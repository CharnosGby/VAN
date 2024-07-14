using Microsoft.EntityFrameworkCore;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.WebCore.WebService
{
    public interface ITestService
    {
        Task<List<UserModel>> GetAllUsers(SQLServerInit serverInit, long id); // 修正方法签名，添加缺少的参数类型和名称
    }

    public class TestService : ITestService
    {
        private readonly SQLServerInit _serverInit;

        public TestService(SQLServerInit serverInit)
        {
            _serverInit = serverInit;
        }

        public async Task<List<UserModel>> GetAllUsers(SQLServerInit serverInit, long id)
        {
            string sql = $"SELECT * FROM [work].[user] WHERE id={id}"; // 使用参数化查询，防止 SQL 注入
            return await serverInit.UserDbContent.FromSqlRaw(sql).ToListAsync();
        }
    }
}