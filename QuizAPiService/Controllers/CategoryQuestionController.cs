using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryQuestionController : ControllerBase
    {
        // GET: api/<CategoryQuestionController>
        private readonly IQuizInterfaceService _QuizService;

        public CategoryQuestionController(IQuizInterfaceService QuizService)
        {
            _QuizService = QuizService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryQuestion>> GetCategorys()
        {
            return await _QuizService.GetCategoryQuestionsAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryQuestion>> GetActiveCategorys()
        {
            return await _QuizService.GetActiveCategoryQuestionsAsync();
        }

        [HttpGet]
        public async Task<CategoryQuestion> GetCategoryById([FromBody] CategoryQuestion categoryQuestion)
        {
            return await _QuizService.GetCategoryQuestionByIdAsync(categoryQuestion.CategoryId);
        }


        // POST: CategoryQuestionController/Create
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryQuestion categoryQuestion)
        {
            await _QuizService.InsertCategoryQuestionAsync(categoryQuestion);
            return Ok();
        }

        // POST: CategoryQuestionController/Create
        [HttpPost]
        public async Task<IActionResult> InsertCategorys([FromBody] List<CategoryQuestion> lstCategoryQuestion)
        {
            await _QuizService.InsertCategoryQuestionAsync(lstCategoryQuestion);
            return Ok();
        }

        // Put: CategoryQuestionController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryQuestion categoryQuestion)
        {
            await _QuizService.UpdateCategoryQuestionAsync(categoryQuestion);
            return Ok();
        }

        // Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategorys([FromBody] List<CategoryQuestion> lstCategoryQuestion)
        {
            await _QuizService.UpdateCategoryQuestionAsync(lstCategoryQuestion);
            return Ok();
        }

        [HttpDelete]
        public async Task<HttpStatusCode> DeleteCategoryQuestion([FromBody] List<CategoryQuestion> lstLevel)
        {
            var status = await _QuizService.DeleteCategoryQuestionAsync(lstLevel);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }

        //  [ServiceFilter(typeof(CustomExceptionFilter))]
        // HttpDelete: CategoryQuestionController/delete/5
        [HttpDelete]
        public async Task<HttpStatusCode> DeleteCategoryQuestions([FromBody] List<CategoryQuestion> lstLevel)
        {
            var status=await _QuizService.DeleteCategoryQuestionAsync(lstLevel);
            if(!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
