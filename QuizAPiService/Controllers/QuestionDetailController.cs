using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionDetailController : ControllerBase
    {
        private readonly IQuestionDetails _QuizService;

        public QuestionDetailController(IQuestionDetails QuizService)
        {
            _QuizService = QuizService;
        }
        // GET: api/<QuestionDetailController>
        [HttpGet]
        public async Task<IEnumerable<Questiondetail>> GetQuestionDetails()
        {
            return await _QuizService.GetQuestionDetail();
        }

        [HttpGet]
        public async Task<IEnumerable<Questiondetail>> GetActiveQuestionDetails()
        {
            return await _QuizService.GetActiveQuestionDetail();
        }

        [HttpGet("{id}")]
        public async Task<Questiondetail> GetQuestionDetailByQuestionId( int questionId)
        {
            return await _QuizService.GetQuestionDetailByQuestionId(questionId);
        }


        // GET api/<QuestionDetailController>/5
        [HttpGet("{id}")]
        public async Task<Questiondetail> GetQuestionDetailByCategoryId( int categoryId)
        {
            return await _QuizService.GetQuestionDetailByCategoryId(categoryId);
        }

        [HttpGet("{id}")]
        public async Task<Questiondetail> GetQuestionDetailByLevelId([FromRoute] int levelId)
        {
            return await _QuizService.GetQuestionDetailByLevelId(levelId);
        }


        // POST api/<QuestionDetailController>
        //[HttpPost]
        //public async Task<IActionResult> InsertQuestionDetail([FromBody] List<Questiondetail> questiondetail)
        //{
        //    await _QuizService.InsertQuestionDetail(questiondetail);
        //    return Ok();
        //}


        [HttpPost]
        public async Task<IActionResult> InsertQuestionDetails([FromBody] Questiondetail lstquestion)
        {
            await _QuizService.InsertQuestionDetails(lstquestion);
            return Ok();
        }


        // PUT api/<QuestionDetailController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionDetails([FromBody] Questiondetail questiondetail)
        {
            await _QuizService.UpdateQuestionDetails(questiondetail);
            return Ok();
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestionDetail([FromBody] List<Questiondetail> lstquestion)
        //{
        //    await _QuizService.UpdateQuestionDetail(lstquestion);
        //    return Ok();
        //}



        // DELETE api/<QuestionDetailController>/5
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteQuestionDetail( Questiondetail questionId)
        {
            var status = await _QuizService.DeleteQuestionDetails(questionId);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }

        //[HttpDelete("{id}")]
        //public async Task<HttpStatusCode> DeleteQuestionDetails([FromBody] int lstquestionId)
        //{
        //    var status = await _QuizService.DeleteQuestionDetail(lstquestionId);
        //    if (!status)
        //    {
        //        return HttpStatusCode.NotFound;
        //    }
        //    return HttpStatusCode.OK;
        //}

    }
}
