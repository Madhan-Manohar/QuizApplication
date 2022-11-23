using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetail _QuizService;

        public UserDetailController(IUserDetail QuizService)
        {
            _QuizService = QuizService;
        }


        // GET: api/<UserDetailController>
        [HttpGet]
        public async Task<IEnumerable<Userdetail>> GetUsers()
        {
            return await _QuizService.GetUserdetails();
        }

        [HttpGet]
        public async Task<IEnumerable<Userdetail>> GetActiveUsers()
        {
            return await _QuizService.GetActiveUsers();
        }
        // GET api/<UserDetailController>/5
        [HttpGet("{id}")]
        public async Task<Userdetail> GetUserByUserId(int userId)
        {
            return await _QuizService.GetUserDetailByUserId(userId);
        }

        [HttpGet("{id}")]
        public async Task<Userdetail> GetUserByUserEmployeeId(int userId)
        {
            return await _QuizService.GetUserDetailByEmployeeeId(userId);
        }

        // POST: LevelController/Create
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] Userdetail  userdetail)
        {
            await _QuizService.InsertUserDetail(userdetail);
            return Ok();
        }

        // PUT api/<UserDetailController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] Userdetail userdetail)
        {
            await _QuizService.UpdateUserDetail(userdetail);
            return Ok();
        }
        // DELETE api/<UserDetailController>/5
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteLevel( Userdetail userId)
        {
            var status = await _QuizService.DeleteUserDetail(userId);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
