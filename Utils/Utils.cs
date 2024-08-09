using Dapper;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using JWT;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using ServiceStack.Redis;

namespace Utils
{
    public class MyUtils
    {
        // 需要先在外界初始化SQLUtil实例
        public class SQLUtil(string connectionString)
        {
            public SqlConnection Connection = new (connectionString);

            public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
            {
                try
                {
                    return await Connection.QueryAsync<T>(sql, parameters);
                }
                finally
                {
                    Connection.Dispose();
                }
                
            }

            public async Task<bool> ExecuteAsync(string sql, object? parameters = null)
            {
                try
                {
                    return await Connection.ExecuteAsync(sql, parameters) > 0;
                }
                finally
                {
                    Connection.Dispose();
                }
            }
        }

        // 直接用
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

        // 直接用
        public class Md5Util
        {
            public static string Encrypt(string input)
            {
                try
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = MD5.HashData(inputBytes);

                    StringBuilder sb = new();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }

                    return sb.ToString();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

        // 直接用
        public class RedisUtil
        {
            private static readonly string Addr = "127.0.0.1";
            private static readonly int Port = 6379;
            private static readonly string Password = "123456";
            private static readonly long DbIndex = 0L;
            private static RedisClient redis;

            static RedisUtil()
            {
                redis = GetRedisClient(Addr, Port, Password, DbIndex);
            }

            private static RedisClient GetRedisClient(string addr, int port, string password, long dbIndex)
            {
                return new RedisClient(addr, port, password, dbIndex);
            }

            public static void DisposeRedisClient(RedisClient redisClient)
            {
                redisClient.Dispose();
            }

            public static void Set<T>(string key, T value, DateTime expireSeconds)
            {
                redis ??= GetRedisClient(Addr, Port, Password, DbIndex);
                redis.Set(key, value, expireSeconds);
            }

            public static T Get<T>(string key)
            {
                if (redis == null)
                {
                    redis = GetRedisClient(Addr, Port, Password, DbIndex);
                }
                return redis.Get<T>(key);
            }
        }

        // 直接用
        public class ThreadLocalUtil
        {
            private static readonly ThreadLocal<object> THREAD_LOCAL = new ThreadLocal<object>();

            public static T Get<T>()
            {
                if (THREAD_LOCAL.Value == null)
                {
                    throw new InvalidOperationException("Value not set in ThreadLocal.");
                }

                return (T)THREAD_LOCAL.Value;
            }

            public static void Set(object value)
            {
                THREAD_LOCAL.Value = value;
            }

            public static void Remove()
            {
                THREAD_LOCAL.Dispose(); // 清理 ThreadLocal 对象
            }
        }
    }
}
