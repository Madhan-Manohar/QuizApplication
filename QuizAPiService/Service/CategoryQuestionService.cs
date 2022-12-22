using K4os.Compression.LZ4;
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
    public class CategoryQuestionService : ICategoryQuestionService
    {
        private readonly QuizContext _quizContext;
        public CategoryQuestionService(QuizContext quizContext)
        {
            _quizContext = quizContext;
        }

       #region Category
        
        /// <summary>
        /// Get all the CategoryQuestions
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
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

        /// <summary>
        /// Get all the Active CategoryQuestions
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
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

        /// <summary>
        /// Get Category Question By Name
        /// </summary>
        /// <param name="categoryType"></param>
        /// <returns></returns>
        public async Task<CategoryQuestion> GetCategoryQuestionByTypeAsync(string categoryType)
        {
            try
            {
                if (!string.IsNullOrEmpty(categoryType))
                {
                    return await _quizContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == categoryType.ToLower()).FirstAsync<CategoryQuestion>();

                }
                else
                {
                    throw new NotFoundException("CategoryQuestion cannot fetched");
                }
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Insert Category Question 
        /// </summary>
        /// <param name="categoryQuestion"></param>
        /// <returns></returns>
        public async Task<bool> InsertCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                //Validating the whether same category question exista
                if (categoryQuestion != null && !_quizContext.CategoryQuestions.Any(x => x.CategoryType.ToLower() == categoryQuestion.CategoryType.ToLower()))
                {
                    var addCategoryQuestion = new CategoryQuestion()
                    {
                        CategoryType = categoryQuestion.CategoryType,
                        IsActive = categoryQuestion.IsActive,
                        CreatedBy = categoryQuestion.CreatedBy,
                        CreatedOn = categoryQuestion.CreatedOn,
                        ModifiedBy = categoryQuestion.ModifiedBy,
                        ModifiedOn = categoryQuestion.ModifiedOn
                    };

                    var result = _quizContext.CategoryQuestions.Add(addCategoryQuestion);
                    await _quizContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NotFoundException("CategoryQuestion Type is not Inserted becuase CategoryQuestion Type not exist or Inserting with existing ");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert Category Questions by Passing list of CategoryQuestion Object
        /// </summary>
        /// <param name="lstCategoryQuestion"></param>
        /// <returns></returns>
        public async Task<bool> InsertCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion)
        {
            try
            {
                //find in list has same categorytypes
                var distinctCategorys = lstCategoryQuestion.GroupBy(x => x.CategoryType).Select(y => y.First());
                
                //find list has same categorytype has in database
                var existDuplicate = _quizContext.CategoryQuestions.Any(t => !lstCategoryQuestion.Select(l => l.CategoryType.ToLower()).Contains(t.CategoryType.ToLower()));
              
                if (distinctCategorys.Count() == lstCategoryQuestion.Count() && (existDuplicate || _quizContext.Levels.Count() == 0))
                {
                    await Task.WhenAll(lstCategoryQuestion.Select(async categoryQuestion =>
                    {
                        await InsertCategoryQuestionAsync(categoryQuestion);
                    }));

                    return true;
                }
                else
                {
                    throw new BadRequestException("CategoryQuestion Type(s) are not Inserted because CategoryQuestion Type not exist or Inserting with duplicate");
                }
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Update the Category Question By passsing category Object
        /// </summary>
        /// <param name="categoryQuestion"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                //find category name should not exist in database
                var categoryQuestionType = _quizContext.CategoryQuestions.Any(x => x.CategoryType.ToLower() == categoryQuestion.CategoryType.ToLower());
                
                if (categoryQuestion != null && !categoryQuestionType)
                {
                    CategoryQuestion updateCategoryQuestion = await _quizContext.CategoryQuestions.FirstAsync<CategoryQuestion>(s => s.CategoryId == categoryQuestion.CategoryId);
                    if (updateCategoryQuestion != null)
                    {
                        var quizDetailsExist = _quizContext.Quizdetails.Where(x => x.CategoryId == categoryQuestion.CategoryId && x.IsActive == 1);
                        var questiondetailsExist = _quizContext.Questiondetails.Where(x => x.CategoryId == categoryQuestion.CategoryId && x.IsActive == 1);
                        if (categoryQuestion.IsActive == 0 && questiondetailsExist.Count() > 0)
                        {
                            throw new BadRequestException("CategoryQuestion cannot be update as inactive because Question details have active records related to CategoryQuestion");
                        }
                        if (categoryQuestion.IsActive == 0 && quizDetailsExist.Count() > 0)
                        {
                            throw new BadRequestException("CategoryQuestion cannot be update as inactive because Quiz details have active records related to CategoryQuestion");
                        }
                        updateCategoryQuestion.CategoryType = categoryQuestion.CategoryType;
                        updateCategoryQuestion.IsActive = categoryQuestion.IsActive;
                        updateCategoryQuestion.ModifiedBy = categoryQuestion.ModifiedBy;
                        updateCategoryQuestion.ModifiedOn = categoryQuestion.ModifiedOn;
                        var result = _quizContext.CategoryQuestions.Update(updateCategoryQuestion);
                        await _quizContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new NotFoundException("CategoryQuestion Type is not Updated becuase CategoryQuestion Type not exist");
                    }
                }
                else
                {
                    throw new NotFoundException("CategoryQuestion Type is not Updated becuase CategoryQuestion Type not exist or updating with existing ");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateCategoryQuestion by passsing list of category object
        /// </summary>
        /// <param name="lstCategoryQuestion"></param>
        /// <returns></returns>
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
                else
                {
                    throw new NotFoundException("CategoryQuestion Type(s) are not Updated becuase CategoryQuestion Type(s) not exist");
                }
            }
            catch
            {
                throw;
            }


        }

        /// <summary>
        /// Delete Category Question By passing CategoryQuestion Object
        /// </summary>
        /// <param name="categoryQuestion"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategoryQuestionAsync(CategoryQuestion categoryQuestion)
        {
            try
            {
                var categoryQuestionExist = _quizContext.CategoryQuestions.Where(x => x.CategoryId == categoryQuestion.CategoryId);

                var quizDetailsExist = _quizContext.Quizdetails.Where(x => x.CategoryId == categoryQuestion.CategoryId);
                var questiondetailsExist = _quizContext.Questiondetails.Where(x => x.CategoryId == categoryQuestion.CategoryId);

                if (questiondetailsExist.Count() > 0)
                {
                    throw new BadRequestException("CategoryQuestion cannot be delete becuase Questions exits related to CategoryQuestion");
                }
                if (quizDetailsExist.Count() > 0)
                {
                    throw new NotFoundException("CategoryQuestion cannot be delete because Quiz exits related to CategoryQuestion");
                }

                if (categoryQuestionExist.Count() == 1)
                { 
                    _quizContext.CategoryQuestions.Remove(categoryQuestion);
                    await _quizContext.SaveChangesAsync();
                    return true;

                }
                else
                {
                    throw new NotFoundException("CategoryQuestion Type is not delete");
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete Category Questions By passing list of CategoryQuestion Object
        /// </summary>
        /// <param name="lstCategoryQuestion"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategoryQuestionAsync(List<CategoryQuestion> lstCategoryQuestion)
        {
            try
            {
        
                var lstCategoryQuestionIdExist = _quizContext.CategoryQuestions.Where(x => lstCategoryQuestion.Select(c => c.CategoryId).ToList().Contains(x.CategoryId)).ToList();
                var lstCategoryQuestionIdExistinQuiz = lstCategoryQuestion.Where(x => _quizContext.Quizdetails.Select(c => c.CategoryId).ToList().Contains(x.CategoryId)).ToList();
                var lstCategoryQuestionIdExistinQuestion = lstCategoryQuestion.Where(x => _quizContext.Questiondetails.Select(c => c.CategoryId).ToList().Contains(x.CategoryId)).ToList();


                if (lstCategoryQuestionIdExistinQuestion.Count() > 0)
                {
                    throw new BadRequestException("CategoryQuestion(s) cannot be delete becuase Questions details exits related to CategoryQuestion");
                }
                if (lstCategoryQuestionIdExistinQuiz.Count() > 0)
                {
                    throw new NotFoundException("CategoryQuestion(s) cannot be delete because Quiz details exits related to CategoryQuestion");
                }

                if (lstCategoryQuestion.Count() == lstCategoryQuestionIdExist.Count())
                {
                    _quizContext.CategoryQuestions.RemoveRange(lstCategoryQuestion);
                    await _quizContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NotFoundException("CategoryQuestion(s) are not able to delete due to unavailability ");
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}



