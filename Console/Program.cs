using Microsoft.EntityFrameworkCore;
using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;

namespace ConsoleTest
{
    public class Program
    {
        private static SQLServerInit SqlServer = new SQLServerInit("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;");
        static void Main()
        {
            List<Score> data = GetAllScores(SqlServer);
            foreach (var item in data)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void JwtTest()
        {
            Dictionary<string, object> claims = new Dictionary<string, object>();
            claims.Add("UserId", "123456");
            claims.Add("UserName", "张三");
            var token = JwtUtil.CreateJwtToken(claims, DateTime.Now.AddSeconds(5));
            Console.WriteLine(token);

            // 3秒后解析 Token
            Thread.Sleep(3000);
            {
                Dictionary<string, object> dic = JwtUtil.ValidateJwtToken(token);
                // 输出解析结果
                if (dic.TryGetValue("claim", out object? value))
                {
                    Console.WriteLine("claim" + value);
                }
                else
                {
                    Console.WriteLine("UserId: " + dic["UserId"]);
                }
            }

            Thread.Sleep(3000);
            {
                Dictionary<string, object> dic = JwtUtil.ValidateJwtToken(token);
                // 输出解析结果
                if (dic.TryGetValue("claim", out object? value))
                {
                    Console.WriteLine("claim" + value);
                }
                else
                {
                    Console.WriteLine("UserId: " + dic["UserId"]);
                }
            }

        }

        public static void Redistest()
        {
            Console.WriteLine("Redis写入缓存：aaa");

            RedisUtil.Set("aaa", "AAAAA", DateTime.Now.AddSeconds(10));

            Console.WriteLine("Redis获取缓存：aaa");

            string str = RedisUtil.Get<string>("aaa");

            Console.WriteLine(str);
        }

        public static List<Score> GetAllScores(SQLServerInit serverInit)
        {
            string sql = $"SELECT * FROM [work].[score] WHERE del=0";
            List<Score> scores = serverInit.Scores.FromSqlRaw(sql)
                .Select(s => new Score
                {
                    ScoreId = s.ScoreId,
                    StudentId = s.StudentId,
                    DisciplineId = s.DisciplineId,
                    Scores = s.Scores,
                    Del = s.Del
                })
                .ToList();
            return scores;
        }
    }
}