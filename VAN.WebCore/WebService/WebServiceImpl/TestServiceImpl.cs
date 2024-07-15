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

        public async Task<List<User>> GetAllUsers(SQLServerInit serverInit, long id)
        {
            string sql = $"SELECT * FROM [work].[user] WHERE id={id}"; // 使用参数化查询，防止 SQL 注入
            return await serverInit.Users.FromSqlRaw(sql).ToListAsync();
        }
    }
}
