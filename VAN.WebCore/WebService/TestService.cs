using AOPInit;
using Autofac.Extras.DynamicProxy;
using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;
using VAN.WebCore.WebVO;

namespace VAN.WebCore.WebService
{
    [Intercept(typeof(CustomLogInterceptor))]
    public interface ITestService
    {
        Task<List<User>> GetAllUsers(SQLServerInit serverInit, long id); // 修正方法签名，添加缺少的参数类型和名称
        Task<List<Score>> GetAllScores(SQLServerInit serverInit);

        Task<List<ScoreVO>> GetScoreBySno(MyUtils.SQLUtil SqlServer, string sno);
        Task<List<TeacherVO>> GetTeachers(MyUtils.SQLUtil SqlServer, int page,int pageSize);

    }
}
