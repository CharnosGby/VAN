using AOPInit;
using Autofac.Extras.DynamicProxy;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.WebCore.WebService
{
    [Intercept(typeof(CustomLogInterceptor))]
    public interface ITestService
    {
        Task<List<User>> GetAllUsers(SQLServerInit serverInit, long id); // 修正方法签名，添加缺少的参数类型和名称
    }

    
}