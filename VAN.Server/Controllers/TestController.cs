
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
        public async Task<IActionResult> TestGet()
        {
            var data = SpawnTestObject;
            data.Add(await _SQLServerInit.Users.ToListAsync());
            return new JsonResult(new Result<object>()
            {
                Code = (int)Result<object>.CM.SUCCESS_200,
                Message = "Success",
                Data = data
            });
        }

        [HttpGet("TestDoSql")]
        [SwaggerOperation(Summary = "TestDoSql", Description = "TestDoSql")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<object>), description: "结果")]
        [SwaggerResponse(statusCode: 404, type: typeof(Result<object>), description: "Data not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(Result<object>), description: "Internal server error.")]
        public async Task<IActionResult> TestDoSql(long id)
        {
            if (id < 0) {
                return new JsonResult(new Result<object>()
                {
                    Code = (int)Result<object>.CM.ERROR_500,
                    Message = "id must >= 0",
                    Data = null
                });
            }
            List<User> data = await _testService.GetAllUsers(_SQLServerInit, id); 
            if (data.IsNullOrEmpty())
            {
                return new JsonResult(new Result<object>() { 
                    Code = (int)Result<object>.CM.ERROR_404,
                    Message = "Data not found",
                    Data = null
                });
            }
            else {
                return new JsonResult(new Result<object>()
                {
                    Code = (int)Result<object>.CM.SUCCESS_200,
                    Message = "Success",
                    Data = data
                });
            }
        }

        [HttpGet("GetScore")]
        [SwaggerOperation(Summary = "GetScore", Description = "GetScore")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<object>), description: "结果")]
        [SwaggerResponse(statusCode: 404, type: typeof(Result<object>), description: "Data not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(Result<object>), description: "Internal server error.")]
        public async Task<IActionResult> GetScore()
        {
            List<Score> data = await _testService.GetAllScores(_SQLServerInit);
            if (data.IsNullOrEmpty())
            {
                return new JsonResult(new Result<object>()
                {
                    Code = (int)Result<object>.CM.ERROR_404,
                    Message = "Data not found",
                    Data = null
                });
            }
            else
            {
                return new JsonResult(new Result<object>()
                {
                    Code = (int)Result<object>.CM.SUCCESS_200,
                    Message = "Success",
                    Data = data
                });
            }
        }
        private static List<object> SpawnTestObject
        {
            get
            {
                List<object> RsList = new();

                Dictionary<string, int> Rs = new()
                {
                    ["Alice"] = 92,
                    ["Bob"] = 85,
                    ["Charlie"] = 78
                };
                Rs.Add("David", 90);

                foreach (var item in Rs)
                {
                    Dictionary<string, string> t = new()
                    {
                        ["key"] = item.Key,
                        ["value"] = item.Value.ToString()
                    };
                    RsList.Add(t);
                }
                return RsList;
            }
        }
    }
}
