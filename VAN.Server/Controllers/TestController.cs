
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;
using VAN.WebCore.Init;
using VAN.WebCore.WebModels;
using VAN.WebCore.WebService;

namespace VAN.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(DocVersion.V1))]
    public class TestController(SQLServerInit serverInit, ITestService testService) : Controller
    {
        private readonly SQLServerInit _SQLServerInit = serverInit;
        private readonly ITestService _testService = testService;

        [HttpGet("TestGet")]
        [SwaggerOperation(Summary = "TestGet", Description = "测试数据")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<Dictionary<string, int>>), description: "结果")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<Result<object>> TestGet()
        {
            var data = SpawnTestObject;
            data.Add(await _SQLServerInit.Users.ToListAsync());
            return new Result<object>((int)Result<object>.CM.SUCCESS_200, "Success", data);
        }

        [HttpGet("TestDoSql")]
        [SwaggerOperation(Summary = "TestDoSql", Description = "TestDoSql")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<object>), description: "结果")]
        [SwaggerResponse(statusCode: 404, type: typeof(Result<object>), description: "Data not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(Result<object>), description: "Internal server error.")]
        public async Task<Result<object>> TestDoSql(long id)
        {
            List<User> data = await _testService.GetAllUsers(_SQLServerInit, id); // 传递 id 参数给 GetAllUsers 方法
            if (data.IsNullOrEmpty())
            {
                return new Result<object>((int)Result<object>.CM.ERROR_404, "Data not found", "");
            }
            else { 
                return new Result<object>((int)Result<object>.CM.SUCCESS_200, "Success",data); // 返回查询结果
            }
        }

        private static List<Object> SpawnTestObject
        {
            get
            {
                List<Object> RsList = new();

                Dictionary<string, int> Rs = new()
                {
                    ["Alice"] = 92,
                    ["Bob"] = 85,
                    ["Charlie"] = 78
                };
                Rs.Add("David", 90);

                foreach (var item in Rs)
                {
                    Dictionary<string, string> t = new();
                    t["key"] = item.Key;
                    t["value"] = item.Value.ToString();
                    RsList.Add(t);
                }
                return RsList;
            }
        }
    }
}
