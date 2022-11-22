using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using System.Collections.Generic;

namespace QuizAPiService.Service.Abstract
{
    public interface ICategoryQuestionService
    {
        Task<IEnumerable<CategoryQuestion>> GetCategoryQuestionsAsync();
        Task<IEnumerable<CategoryQuestion>> GetActiveCategoryQuestionsAsync();
        Task<CategoryQuestion> GetCategoryQuestionByTypeAsync(string categoryType);
        Task<bool> InsertCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
        Task<bool> InsertCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> UpdateCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> UpdateCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
        Task<bool> DeleteCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> DeleteCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
    }
}
