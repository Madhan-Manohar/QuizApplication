using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using System.Collections.Generic;

namespace QuizAPiService.Service.Abstract
{
    public interface IQuizInterfaceService
    {
        Task<IEnumerable<Level>> GetLevelsAsync();
        Task<IEnumerable<Level>> GetActiveLevelsAsync();
        Task<Level> GetLevelByIdAsync(int levelId);
        Task<Level> GetActiveLevelByIdAsync(int levelId);
        Task<bool> InsertLevelAsync(List<Level> level);
        Task<bool> InsertLevelAsync(Level level);
        Task<bool> UpdateLevelAsync(Level level);
        Task<bool> UpdateLevelAsync(List<Level> level);
        Task<bool> DeleteLevelAsync(Level levelId);
        Task<bool> DeleteLevelAsync(List<Level> lstLevelId);

        Task<IEnumerable<CategoryQuestion>> GetCategoryQuestionsAsync();
        Task<IEnumerable<CategoryQuestion>> GetActiveCategoryQuestionsAsync();
        Task<CategoryQuestion> GetCategoryQuestionByIdAsync(int categoryId);
        Task<CategoryQuestion> GetActiveCategoryQuestionByIdAsync(int categoryId);
        Task<bool> InsertCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
        Task<bool> InsertCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> UpdateCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> UpdateCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
        Task<bool> DeleteCategoryQuestionAsync(CategoryQuestion categoryQuestion);
        Task<bool> DeleteCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion);
    }
}
