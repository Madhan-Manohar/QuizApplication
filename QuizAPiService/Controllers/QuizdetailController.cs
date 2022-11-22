using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizdetailController : ControllerBase
    {
        private readonly IQuizdetailService _QuizService;
        public QuizdetailController(IQuizdetailService QuizService)
        {
            _QuizService = QuizService;
        }

        // GET: api/<QuizdetailController>
        [HttpGet]
        public async Task<IEnumerable<Quizdetail>> Get()
        {
            return await _QuizService.GetQuizdetailsAsync();
        }


        // GET api/<QuizdetailController>/5
        [HttpGet("{id}")]
        public async Task<Quizdetail> Get([FromRoute] int id)
        {
            return await _QuizService.GetQuizdetailByIdAsync(id);
        }

        // POST api/<QuizdetailController>
        [HttpPost]
        public async Task<IActionResult> post([FromBody] Quizdetail value)
        {
            await _QuizService.InsertQuizdetailAsync(value);
            return Ok();
        }

        // PUT api/<QuizdetailController>/5
        [HttpPut]
        public async Task<IActionResult> put([FromBody] Quizdetail value)
        {
            await _QuizService.UpdateQuizdetailAsync(value);
            return Ok();
        }
        [HttpDelete]
        public async Task<HttpStatusCode> Delete([FromBody] Quizdetail value)
        {
            var status = await _QuizService.DeleteQuizDetailsAsync(value);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
