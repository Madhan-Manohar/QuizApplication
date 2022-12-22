using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        // GET: api/<UserRoleController>
        private readonly IUserRoleInterface _UserRoleService;

        public UserRoleController(IUserRoleInterface QuizService)
        {
            _UserRoleService = QuizService;
        }

        [HttpGet]
        public async Task<IEnumerable<Userrole>> Get()
        {
            return await _UserRoleService.GetUserrolesAsync();
        }

        [HttpGet("{UserRoleId}")]
        public async Task<Userrole> Get( int UserRoleId)
        {
            return await _UserRoleService.GetUserroleByIdAsync(UserRoleId);
        }

        // POST: UserRoleController/Create
        [HttpPost]

        public async Task<IActionResult> post([FromBody] Userrole userrole)
        {
            await _UserRoleService.InsertUserroleAsync(userrole);
            return Ok();
        }

        // Put: UserRoleController/Edit/5
        [HttpPut]
        public async Task<IActionResult> put([FromBody] Userrole userrole)
        {
            var result = _UserRoleService.UpdateUserroleAsync(userrole);
            return StatusCode(200,result);
        }


        [ServiceFilter(typeof(CustomExceptionFilter))]
        // HttpDelete: LevelController/delete/5
        [HttpDelete("{UserRoleId}")]
        public async Task<IActionResult> delete([FromRoute] int UserRoleId)
        {
            var result = _UserRoleService.DeleteUserroleAsync(UserRoleId);
            return StatusCode(200, result);
        }
    }
    
}
