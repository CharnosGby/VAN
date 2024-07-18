using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;

namespace Utils
{
    public class JwtUtil
    {
        private const string KEY = "aabbccdd12345678aabbccdd12345678";

        public static string CreateJwtToken(IDictionary<string, object> payload, DateTime expires)
        {
            // 添加过期时间到 payload 中
            payload["exp"] = (long)expires.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            // 初始化 JWT 相关组件
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            // 生成 JWT Token
            var token = encoder.Encode(payload, KEY);
            return token;
        }

        public static Dictionary<string, object> ValidateJwtToken(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm alg = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
                var json = decoder.Decode(token, KEY, true);

                // 解析 JSON 字符串为字典
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                if (dictionary == null)
                { 
                    return new Dictionary<string, object> { { "claim", "无效" } };
                }
                // 如果过期
                if (dictionary.TryGetValue("exp", out object? value))
                {
                    var sec = (long)value - DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    if (sec < 0)
                    {
                        return new Dictionary<string, object> { { "claim", "过期" } }; 
                    }
                }
                // 返回解析后的字典
                return dictionary;
            }
            catch (TokenExpiredException)
            {
                // 表示过期
                return new Dictionary<string, object> { { "claim", "过期" } };
            }
            catch (SignatureVerificationException)
            {
                // 表示验证不通过
                return new Dictionary<string, object> { { "claim", "无效" } };
            }
            catch (Exception e)
            {
                return new Dictionary<string, object> { { "claim", e } };
            }
        }
    }
}