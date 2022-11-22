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
    public class RoleDetailsController : ControllerBase
    {
        private readonly IRoleDetailsInterfaceService _RoleDetailsService;

        public RoleDetailsController(IRoleDetailsInterfaceService RoleDetailsService)
        {
            _RoleDetailsService = RoleDetailsService;
        }


        [HttpGet]
        public async Task<IEnumerable<Roledetail>> Get()
        {
            return await _RoleDetailsService.GetRoleDetailsAsync();
        }

        [HttpGet("{RoleId}")]
        public async Task<Roledetail> Get([FromRoute] int RoleId)
        {
            return await _RoleDetailsService.GetRoleDetailByIdAsync(RoleId);
        }

        // POST: CategoryQuestionController/Create
        [HttpPost]

        public async Task<IActionResult> post([FromBody] Roledetail roledetail)
        {
            await _RoleDetailsService.InsertRoleDetailAsync(roledetail);
            return Ok();
        }

        // Put: CategoryQuestionController/Edit/5
        [HttpPut]
        public async Task<IActionResult> put([FromBody] Roledetail roledetail)
        {
            var result = _RoleDetailsService.UpdateRoleDetailAsync(roledetail);
            return StatusCode(200,result);
        }


        [ServiceFilter(typeof(CustomExceptionFilter))]
        // HttpDelete: CategoryQuestionController/delete/5
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> delete([FromRoute] int RoleId)
        {
            var result = _RoleDetailsService.DeleteRoleDetailsAsync(RoleId);
            return StatusCode(200,result);
        }

    }
}
