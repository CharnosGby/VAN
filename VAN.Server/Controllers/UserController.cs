using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using VueASPNet.Server.Db;
using VueASPNet.Server.Models;

namespace VueASPNet.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ILogger<UserController> logger, BaseContext baseContext) : Controller
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly BaseContext _baseContext = baseContext;

        [HttpGet("GetUsers")]
        [SwaggerOperation(Summary = "Get Users", Description = "获取全部用户数据")]
        [SwaggerResponse(statusCode: 200, type: typeof(IEnumerable<UserModel>), description: "用户的数据集合")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _baseContext.UserDbContent.ToListAsync());
        }

        [HttpPost("AddUser")]
        [SwaggerOperation(Summary = "Add User", Description = "添加一个用户")]
        [SwaggerResponse(statusCode: 201, type: typeof(UserModel), description: "成功添加用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<ActionResult<UserModel>> Add(UserModel user)
        {
            await _baseContext.AddAsync(user);
            await _baseContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("UpdateUser")]
        [SwaggerOperation(Summary = "Update User", Description = "更新一个用户")]
        [SwaggerResponse(statusCode: 200, type: typeof(UserModel), description: "成功更新用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Update(UserModel user)
        {
            var u = await _baseContext.UserDbContent.FindAsync(user.Id);
            if (u == null)
            {
                return NotFound();
            }
            var entry = _baseContext.Entry(entity: user);
            entry.State = EntityState.Modified;
            await _baseContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("DeleteUser")]
        [SwaggerOperation(Summary = "Delete User", Description = "删除一个用户")]
        [SwaggerResponse(statusCode: 200, type: typeof(UserModel), description: "成功删除用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _baseContext.UserDbContent.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _baseContext.UserDbContent.Remove(user);
            await _baseContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}