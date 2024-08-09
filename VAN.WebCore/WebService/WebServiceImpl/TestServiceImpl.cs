using VAN.SQLServerCore.SQLServer.Models;
using VAN.SQLServerCore.SQLServer;
using Microsoft.EntityFrameworkCore;
using VAN.WebCore.WebVO;
using Utils;

namespace VAN.WebCore.WebService.WebServiceImpl
{
    public class TestServiceImpl : ITestService
    {

        public async Task<List<Score>> GetAllScores(SQLServerInit SqlServer)
        {
            string sql = $"SELECT * FROM [work].[score] WHERE del=0";
            List<Score> scores = await SqlServer.Scores.FromSqlRaw(sql)
                .Select(s => new Score
                {
                    ScoreId = s.ScoreId,
                    StudentId = s.StudentId,
                    DisciplineId = s.DisciplineId,
                    Scores = s.Scores,
                    Del = s.Del
                })
                .ToListAsync();
            return scores;
        }

        public async Task<List<User>> GetAllUsers(SQLServerInit SqlServer, long id)
        {
            string sql = $"SELECT * FROM [work].[user] WHERE id={id}";
            return await SqlServer.Users.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<List<ScoreVO>> GetScoreBySno(MyUtils.SQLUtil SqlServer, string sno)
        {
            List<ScoreVO> rs = (List<ScoreVO>)await SqlServer.QueryAsync<ScoreVO>(SQLString.GetStudentScore(sno));
            return rs;
        }

        public async Task<List<TeacherVO>> GetTeachers(MyUtils.SQLUtil SqlServer, int page, int pageSize)
        {
            List<TeacherVO> rs = (List<TeacherVO>)await SqlServer.QueryAsync<TeacherVO>(SQLString.GetTeachers(page,pageSize));
            return rs;
        }
    }
}
