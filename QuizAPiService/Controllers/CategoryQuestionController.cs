using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Net;

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryQuestionController : ControllerBase
    {
        // GET: api/<CategoryQuestionController>
        private readonly ICategoryQuestionService _QuizService;

        public CategoryQuestionController(ICategoryQuestionService QuizService)
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
        public async Task<CategoryQuestion> GetCategoryType([FromQuery] string categoryQuestionType)
        {
            return await _QuizService.GetCategoryQuestionByTypeAsync(categoryQuestionType);
        }


        // POST: CategoryQuestionController/Create
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryQuestion categoryQuestion)
        {
            var result = await _QuizService.InsertCategoryQuestionAsync(categoryQuestion);
            return Ok(result);
        }

        //// POST: CategoryQuestionController/Create
        [HttpPost]
        public async Task<IActionResult> InsertCategorys([FromBody] List<CategoryQuestion> lstCategoryQuestion)
        {
            var result = await _QuizService.InsertCategoryQuestionAsync(lstCategoryQuestion);
            return Ok();
        }

        // Put: CategoryQuestionController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryQuestion categoryQuestion)
        {
            var result = await _QuizService.UpdateCategoryQuestionAsync(categoryQuestion);
            return Ok(result);
        }

        //// Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategorys([FromBody] List<CategoryQuestion> lstCategoryQuestion)
        {
            var result = await _QuizService.UpdateCategoryQuestionAsync(lstCategoryQuestion);
            return Ok();
        }

        [HttpDelete]
        public async Task<bool> DeleteCategoryQuestion([FromBody] CategoryQuestion categoryQuestion)
        {
            return await _QuizService.DeleteCategoryQuestionAsync(categoryQuestion);
        }

        //// HttpDelete: CategoryQuestionController/delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryQuestions([FromBody] List<CategoryQuestion> lstCategoryQuestion)
        {
            var result = await _QuizService.DeleteCategoryQuestionAsync(lstCategoryQuestion);
            return Ok();

        }
    }
}
