using VAN.SQLServerCore.SQLServer.Models;
using VAN.SQLServerCore.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace VAN.WebCore.WebService.WebServiceImpl
{
    public class TestServiceImpl : ITestService
    {
        private readonly SQLServerInit _serverInit;

        public TestServiceImpl(SQLServerInit serverInit)
        {
            _serverInit = serverInit;
        }

        public async Task<List<Score>> GetAllScores(SQLServerInit serverInit)
        {
            string sql = $"SELECT * FROM [work].[score] WHERE del=0";
            List<Score> scores = await serverInit.Scores.FromSqlRaw(sql)
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

        public async Task<List<User>> GetAllUsers(SQLServerInit serverInit, long id)
        {
            string sql = $"SELECT * FROM [work].[user] WHERE id={id}";
            return await serverInit.Users.FromSqlRaw(sql).ToListAsync();
        }
    }
}
