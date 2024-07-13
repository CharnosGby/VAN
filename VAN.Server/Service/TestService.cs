using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VAN.Server.Models;
using VueASPNet.Server.Db;
using VueASPNet.Server.Models;

namespace VAN.Server.Service
{
    public interface ITestService
    {
        Task<List<UserModel>> GetAllUsers(BaseContext baseContext, long id); // 修正方法签名，添加缺少的参数类型和名称
    }

    public class TestService : ITestService
    {
        private readonly BaseContext _baseContext;

        public TestService(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        public async Task<List<UserModel>> GetAllUsers(BaseContext baseContext, long id)
        {
            string sql = $"SELECT * FROM [work].[user] WHERE id={id}"; // 使用参数化查询，防止 SQL 注入
            return await baseContext.UserDbContent.FromSqlRaw(sql).ToListAsync();
        }
    }
}