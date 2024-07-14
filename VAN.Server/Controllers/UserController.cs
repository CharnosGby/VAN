using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ILogger<UserController> logger, SQLServerInit serverInit) : Controller
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly SQLServerInit _serverInit = serverInit;

        [HttpGet("GetUsers")]
        [SwaggerOperation(Summary = "Get Users", Description = "获取全部用户数据")]
        [SwaggerResponse(statusCode: 200, type: typeof(IEnumerable<User>), description: "用户的数据集合")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _serverInit.Users.ToListAsync());
        }

        [HttpPost("AddUser")]
        [SwaggerOperation(Summary = "Add User", Description = "添加一个用户")]
        [SwaggerResponse(statusCode: 201, type: typeof(User), description: "成功添加用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<ActionResult<User>> Add(User user)
        {
            await _serverInit.AddAsync(user);
            await _serverInit.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("UpdateUser")]
        [SwaggerOperation(Summary = "Update User", Description = "更新一个用户")]
        [SwaggerResponse(statusCode: 200, type: typeof(User), description: "成功更新用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Update(User user)
        {
            var u = await _serverInit.Users.FindAsync(user.Id);
            if (u == null)
            {
                return NotFound();
            }
            var entry = _serverInit.Entry(entity: user);
            entry.State = EntityState.Modified;
            await _serverInit.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("DeleteUser")]
        [SwaggerOperation(Summary = "Delete User", Description = "删除一个用户")]
        [SwaggerResponse(statusCode: 200, type: typeof(User), description: "成功删除用户")]
        [SwaggerResponse(statusCode: 500, type: typeof(string), description: "Internal server error.")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _serverInit.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _serverInit.Users.Remove(user);
            await _serverInit.SaveChangesAsync();
            return Ok(user);
        }
    }
}