
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;
using VAN.WebCore.Init;
using VAN.WebCore.WebModels;
using VAN.WebCore.WebService;
using VAN.WebCore.WebVO;

namespace VAN.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(DocVersion.V1))]
    public class TestController : Controller
    {
        private readonly SQLServerInit _SQLServerInit;
        private readonly MyUtils MyUtils;
        private readonly MyUtils.SQLUtil SqlServer;
        private readonly ITestService _testService;

        public TestController(SQLServerInit sQLServerInit, MyUtils myUtils, ITestService testService)
        {
            _SQLServerInit = sQLServerInit;
            MyUtils = myUtils;
            SqlServer = new MyUtils.SQLUtil("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;");
            _testService = testService;
        }

        [HttpGet("TestGet")]
        [SwaggerOperation(Summary = "TestGet", Description = "测试数据")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<Dictionary<string, int>>), description: "结果")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> TestGet()
        {
            var data =  await _SQLServerInit.Users.ToListAsync();
            return new JsonResult(new Result<object>()
            {
                Code = (int)Result<object>.CM.SUCCESS_200,
                Message = "Success",
                Data = data
            });
        }

        [HttpGet("TestDoSql")]
        [SwaggerOperation(Summary = "TestDoSql", Description = "TestDoSql")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<List<User>>), description: "结果")]
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
        [SwaggerResponse(statusCode: 200, type: typeof(Result<Score>), description: "结果")]
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

        [HttpGet("GetScoreBySno")]
        [SwaggerOperation(Summary = "GetScoreBySno", Description = "GetScoreBySno")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<ScoreVO>), description: "结果")]
        [SwaggerResponse(statusCode: 404, type: typeof(Result<object>), description: "Data not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(Result<object>), description: "Internal server error.")]
        public async Task<IActionResult> GetScoreBySno(string sno)
        {
            List<ScoreVO> data = await _testService.GetScoreBySno(SqlServer,sno);
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
                return new JsonResult(new Result<List<ScoreVO>>()
                {
                    Code = (int)Result<object>.CM.SUCCESS_200,
                    Message = "Success",
                    Data = data
                });
            }
        }

        [HttpGet("GetTeachers")]
        [SwaggerOperation(Summary = "GetTeachers", Description = "GetTeachers")]
        [SwaggerResponse(statusCode: 200, type: typeof(Result<List<TeacherVO>>), description: "结果")]
        [SwaggerResponse(statusCode: 404, type: typeof(Result<object>), description: "Data not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(Result<object>), description: "Internal server error.")]
        public async Task<IActionResult> GetTeachers(int page,int pageSize)
        {
            List<TeacherVO> data = await _testService.GetTeachers(SqlServer, page, pageSize);
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
                Console.WriteLine(data[0].ToString());
                return new JsonResult(new Result<List<TeacherVO>>()
                {
                    Code = (int)Result<object>.CM.SUCCESS_200,
                    Message = "Success",
                    Data = data
                });
            }
        }

    }
}
