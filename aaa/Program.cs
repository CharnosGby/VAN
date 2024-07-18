using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.WebCore.WebVO;

namespace aaa
{
    public class Program
    {
        private static readonly SQLServerInit SqlServer = new("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;");
        static void Main()
        {
            Task.Run(async () => await RunAsync()).GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {

            List<TeacherVO> Teachers = await GetTeachers(1,100);
            foreach (var teacher in Teachers)
            {
                Console.WriteLine(teacher.ToString());
            }
        }

        public static async Task<List<TeacherVO>> GetTeachers(int page,int pageSize)
        {
            string sql = $@"
            SELECT
	            teacher.tname AS Tname, 
	            CASE 
		            WHEN teacher.sex = 0 THEN '女'
		            WHEN teacher.sex = 1 THEN '男'
		            ELSE '其他'
	            END AS Sex, 
	            teacher.tno AS Tno, 
	            teacher.tphone AS Tphone, 
	            college.cname AS Cname, 
	            college.ccode AS Ccode, 
	            school.sname AS Sname, 
	            school.scode AS Scode, 
	            CASE 
		            WHEN school.slevel = 1 THEN '专科'
		            WHEN school.slevel = 2 THEN '二本'
		            WHEN school.slevel = 3 THEN '一本'
		            WHEN school.slevel = 4 THEN '211'
		            WHEN school.slevel = 5 THEN '985'
		            ELSE '其他'
	            END AS Slevel
            FROM
	            [work].teacher
	            LEFT JOIN
	            [work].college
	            ON 
		            teacher.becollegeid = college.cid
	            LEFT JOIN
	            [work].school
	            ON 
		            college.beschoolcode = school.sid
            ORDER BY
	            teacher.tno ASC
            OFFSET {(page-1)*pageSize} ROWS
            FETCH NEXT {pageSize} ROWS ONLY;
";

            var rs = await SqlServer.QueryAsync<TeacherVO>(sql);
            return (List<TeacherVO>)rs;
        }

        public static async Task<List<ScoreVO>> GetStudentScores(string sno)
        {
            string sql = $@"
            SELECT
                stu.sname AS Sname, 
                stu.sno AS Sno,
                MAX(CASE WHEN di.dname = '语文' THEN sc.scores ELSE NULL END) AS Chinese,
                MAX(CASE WHEN di.dname = '数学' THEN sc.scores ELSE NULL END) AS Math,
                MAX(CASE WHEN di.dname = '英语' THEN sc.scores ELSE NULL END) AS English
            FROM
                [work].score AS sc
                LEFT JOIN [work].student AS stu ON sc.studentid = stu.sid
                LEFT JOIN [work].discipline AS di ON sc.disciplineid = di.did
            WHERE
                stu.sno = {sno}
            GROUP BY
                stu.sname, stu.sno;";

            var rs = await SqlServer.QueryAsync<ScoreVO>(sql);
            return (List<ScoreVO>)rs;
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
    }
}