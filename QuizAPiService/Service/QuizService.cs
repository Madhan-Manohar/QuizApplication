using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web.Http;
using Ubiety.Dns.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuizAPiService.Service
{
    public class QuizService : IQuizInterfaceService
    {
        private readonly QuizContext _quizContext;
        private ILogger<QuizService> _logger;
        public QuizService(QuizContext quizContext, ILogger<QuizService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }

        #region Level
        public async Task<IEnumerable<Level>> GetLevelsAsync()
        {
            try
            {
                return await _quizContext.Levels.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<IEnumerable<Level>> GetActiveLevelsAsync()
        {
            try
            {
                return await _quizContext.Levels.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<Level> GetActiveLevelByIdAsync(int levelId)
        {
            try
            {
                return await _quizContext.Levels.Where(x => x.LevelId == levelId && x.IsActive==1).FirstAsync<Level>();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<Level> GetLevelByIdAsync(int levelId)
        {
            try
            {
                return await _quizContext.Levels.Where(x => x.LevelId == levelId).FirstAsync<Level>();
            }
            catch(Exception excepption)
            {
                throw new NotFoundException(excepption.Message);
            }

        }

        public async Task<bool> InsertLevelAsync(Level level)
        {
            try
            {
                if (level != null && !_quizContext.Levels.Any(x => x.LevelType.ToLower() == level.LevelType.ToLower()))
                {
                    var addLevel = new Level()
                    {
                        LevelType = level.LevelType,
                        IsActive = level.IsActive,
                        CreatedBy = level.CreatedBy,
                        CreatedOn = level.CreatedOn,
                        ModifiedBy= 0,
                        ModifiedOn = null
                    };

                    var result = _quizContext.Levels.Add(addLevel);
                    await _quizContext.SaveChangesAsync();
                    //      _logger.LogInformation(String.Format("Level {0} is Inserted", level.LevelType));
                    return true;
                }

             //   _logger.LogWarning("Level  does not exist to inserted or Duplicate Level is already exist ");
                return false;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public  async Task<bool> InsertLevelAsync(List<Level> lstLevel)
        {
            try
            {
                var distinctLevels = lstLevel.GroupBy(x => x.LevelType).Select(y => y.First());
                var existDuplicate = _quizContext.Levels.Any(t => !lstLevel.Select(l => l.LevelType.ToLower()).Contains(t.LevelType.ToLower()));

                if (distinctLevels.Count() == lstLevel.Count() && (existDuplicate || _quizContext.Levels.Count()==0))
                {
                    await Task.WhenAll(lstLevel.Select(async level =>
                    {
                       await InsertLevelAsync(level);
                     }));
                    return true;
                 }

             //   _logger.LogWarning("Duplicate Levels is exist ,So cannot be updated");
                return false;

            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }
        public async Task<bool> UpdateLevelAsync(List<Level> lstLevel)
        {
            try
            {
                var distinctLevels = lstLevel.GroupBy(x => x.LevelType).Select(y => y.First());
                var existDuplicate = _quizContext.Levels.Any(t => !lstLevel.Select(l => l.LevelType.ToLower()).Contains(t.LevelType.ToLower()));


                if (distinctLevels.Count() == lstLevel.Count() && (existDuplicate || _quizContext.Levels.Count() == 0))
                {
                    await Task.WhenAll(lstLevel.Select(async level =>
                    {
                        await UpdateLevelAsync(level);
                     }));
                    return true;
                }
          //      _logger.LogWarning("Duplicate Levels is exist ,So cannot be updated");
                return false;
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }


        }

        public async Task<bool> UpdateLevelAsync(Level level)
        {
            try
            {
                if (level != null && !_quizContext.Levels.Any(x => x.LevelType.ToLower() == level.LevelType.ToLower()))
                {
                    Level updateLevel = await _quizContext.Levels.FirstAsync<Level>(s => s.LevelId == level.LevelId);
                    updateLevel.LevelType = level.LevelType;
                    updateLevel.IsActive = level.IsActive;
                    updateLevel.ModifiedBy = level.ModifiedBy;
                    updateLevel.ModifiedOn = level.ModifiedOn;
                    var result = _quizContext.Levels.Update(updateLevel);
                    await _quizContext.SaveChangesAsync();
           //         _logger.LogInformation(String.Format("Level {0} is updated", level.LevelType));
                    return true;
                }

           //     _logger.LogWarning("Level  does not exist to update or Duplicate Level is already exist ");
                return false;

            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }
        public async Task<bool> DeleteLevelAsync(Level level)
        {
            try
            {
                var levelIdExist = _quizContext.Levels.Where(x => x.LevelId== level.LevelId);

                if (levelIdExist.Count()==1)
                {
                    _quizContext.Levels.Remove(level);
                    await _quizContext.SaveChangesAsync();
                    //    _logger.LogInformation("level are deleted");
                    return true;
                }
                else
                {
                    throw new NotFoundException("Level doesnot exists to delete");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteLevelAsync(List<Level> lstLevelId)
        {
            

            try
            {
                var lstLevelIdExist = _quizContext.Levels.Where(x => lstLevelId.Select(c => c.LevelId).ToList().Contains(x.LevelId)).ToList();

                if (lstLevelId.Count() == lstLevelIdExist.Count())
                {
                    _quizContext.Levels.RemoveRange(lstLevelId);
                    await _quizContext.SaveChangesAsync();
           //         _logger.LogInformation("Levels are deleted");
                    return true;
                }

             //   _logger.LogWarning("Levels are not able to delete due to unavailability ");
                return false;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }
        #endregion

        #region Category
        public async Task<IEnumerable<CategoryQuestion>> GetCategoryQuestionsAsync()
        {
            try
            {
                return await _quizContext.CategoryQuestions.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<IEnumerable<CategoryQuestion>> GetActiveCategoryQuestionsAsync()
        {
            try
            {
                return await _quizContext.CategoryQuestions.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<CategoryQuestion> GetCategoryQuestionByIdAsync(int categoryId)
        {
            try
            {
                return  await _quizContext.CategoryQuestions.Where(x => x.CategoryId == categoryId).FirstAsync<CategoryQuestion>();

            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<CategoryQuestion> GetActiveCategoryQuestionByIdAsync(int categoryId)
        {
            try
            {
                return await _quizContext.CategoryQuestions.Where(x => x.CategoryId == categoryId && x.IsActive == 1).FirstAsync<CategoryQuestion>();

            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<bool> InsertCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                if (categoryQuestion != null && !_quizContext.CategoryQuestions.Any(x => x.CategoryType.ToLower() == categoryQuestion.CategoryType.ToLower()))
                {
                    var addCategoryQuestion = new CategoryQuestion()
                    {
                        CategoryType = categoryQuestion.CategoryType,
                        IsActive = categoryQuestion.IsActive,
                        CreatedBy = categoryQuestion.CreatedBy,
                        CreatedOn = categoryQuestion.CreatedOn,
                        ModifiedBy = 0,
                        ModifiedOn = null
                    };

                    var result = _quizContext.CategoryQuestions.Add(addCategoryQuestion);
                    await _quizContext.SaveChangesAsync();
           //         _logger.LogInformation(String.Format("CategoryQuestion {0} is Inserted", categoryQuestion.CategoryId));
                    return true;
                }

          //      _logger.LogWarning("Category  does not exist to inserted or Duplicate CategoryQuestion is already exist ");
                return false;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<bool> InsertCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion)
        {
            try
            {
                var distinctCategorys = lstCategoryQuestion.GroupBy(x => x.CategoryType).Select(y => y.First());
                var existDuplicate = _quizContext.CategoryQuestions.Any(t => !lstCategoryQuestion.Select(l => l.CategoryType.ToLower()).Contains(t.CategoryType.ToLower()));
                if (distinctCategorys.Count() == lstCategoryQuestion.Count() && (existDuplicate || _quizContext.Levels.Count() == 0))
                {
                    await Task.WhenAll(lstCategoryQuestion.Select(async categoryQuestion =>
                    {
                        await InsertCategoryQuestionAsync(categoryQuestion);
                    }));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }

        public async Task<bool> UpdateCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                if (categoryQuestion != null && !_quizContext.CategoryQuestions.Any(x => x.CategoryType.ToLower() == categoryQuestion.CategoryType.ToLower()))
                {
                    CategoryQuestion updateCategoryQuestion = await _quizContext.CategoryQuestions.FirstAsync<CategoryQuestion>(s => s.CategoryId == categoryQuestion.CategoryId);
                    updateCategoryQuestion.CategoryType = categoryQuestion.CategoryType;
                    updateCategoryQuestion.IsActive = categoryQuestion.IsActive;
                    updateCategoryQuestion.ModifiedBy = categoryQuestion.ModifiedBy;
                    updateCategoryQuestion.ModifiedOn = categoryQuestion.ModifiedOn;
                    var result = _quizContext.CategoryQuestions.Update(updateCategoryQuestion);
                    await _quizContext.SaveChangesAsync();
                 //   _logger.LogInformation(String.Format("CategoryQuestion {0} is updated", categoryQuestion.CategoryId));
                    return true;
                }

         //       _logger.LogWarning("Category  does not exist to update or Duplicate CategoryQuestion is already exist ");
                return false;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }
        public async Task<bool> UpdateCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion)
        {
            try
            {
                var distinctCategorys = lstCategoryQuestion.GroupBy(x => x.CategoryType).Select(y => y.First());
                var existDuplicate = _quizContext.CategoryQuestions.Any(t => !lstCategoryQuestion.Select(l => l.CategoryType.ToLower()).Contains(t.CategoryType.ToLower()));

                if (distinctCategorys.Count() == lstCategoryQuestion.Count() && (existDuplicate || _quizContext.Levels.Count() == 0))
                {
                    await Task.WhenAll(lstCategoryQuestion.Select(async categoryQuestion =>
                    {
                        await UpdateCategoryQuestionAsync(categoryQuestion);
                    }));
                    return true;
                }
                //      _logger.LogWarning("Duplicate CategoryQuestion is exist ,So cannot be updated");
                return false;
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }


        }
        public async Task<bool> DeleteCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                var categoryQuestionExist = _quizContext.CategoryQuestions.Where(x => x.CategoryId == categoryQuestion.CategoryId);

                if (categoryQuestionExist.Count() == 1)
                { 
                    _quizContext.CategoryQuestions.Remove(categoryQuestion);
                    await _quizContext.SaveChangesAsync();
                //    _logger.LogInformation("CategoryQuestions are deleted");
                    return true;

                }
                else
                {
                    throw new NotFoundException("CategortQuestion doesnot exists to delete");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion)
        {
            try
            {
                var lstCategoryIdExist = _quizContext.CategoryQuestions.Where(x => lstCategoryQuestion.Select(c => c.CategoryId).ToList().Contains(x.CategoryId)).ToList();
                if (lstCategoryQuestion.Count() == lstCategoryIdExist.Count())
                {
                    _quizContext.CategoryQuestions.RemoveRange(lstCategoryQuestion);
                    await _quizContext.SaveChangesAsync();
                 //   _logger.LogInformation("CategoryQuestions are deleted");
                    return true;
                
                }
           //     _logger.LogWarning("CategoryQuestions are not able to delete due to unavailability ");
                return false;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        #endregion

    }
}



