using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.WebCore.WebVO;

namespace ConsoleTest
{
    public class Program
    {
        private static readonly MyUtils.SQLUtil SqlServer = new("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;");
        static void Main()
        {
            Task.Run(async () => await RunAsync()).GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {

            List<TeacherVO> Teachers = await GetTeachers(1, 10);
            foreach (var teacher in Teachers)
            {
                Console.WriteLine(teacher.ToString());
            }
            //List<ScoreVO> scores = await GetStudentScores("10001");
            //foreach (var score in scores)
            //{
            //    Console.WriteLine(score.ToString());
            //}
        }

        public static async Task<List<TeacherVO>> GetTeachers(int page, int pageSize)
        {
            List<TeacherVO> rs = (List<TeacherVO>)await SqlServer.QueryAsync<TeacherVO>(SQLString.GetTeachers(page, pageSize));
            return rs;
        }

        public static async Task<List<ScoreVO>> GetStudentScores(string sno)
        {
            List<ScoreVO> rs = (List<ScoreVO>)await SqlServer.QueryAsync<ScoreVO>(SQLString.GetStudentScore(sno));
            return rs;
        }

        public static void JwtTest()
        {
            Dictionary<string, object> claims = new Dictionary<string, object>();
            claims.Add("UserId", "123456");
            claims.Add("UserName", "张三");
            var token = MyUtils.JwtUtil.CreateJwtToken(claims, DateTime.Now.AddSeconds(5));
            Console.WriteLine(token);

            // 3秒后解析 Token
            Thread.Sleep(3000);
            {
                Dictionary<string, object> dic = MyUtils.JwtUtil.ValidateJwtToken(token);
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
                Dictionary<string, object> dic = MyUtils.JwtUtil.ValidateJwtToken(token);
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

            MyUtils.RedisUtil.Set("aaa", "AAAAA", DateTime.Now.AddSeconds(10));

            Console.WriteLine("Redis获取缓存：aaa");

            string str = MyUtils.RedisUtil.Get<string>("aaa");
            Console.WriteLine(str);
        }
    }
}