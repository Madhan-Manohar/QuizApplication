using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Service
{
    public class QuestionDetailRepository : IQuestionDetails
    {

        private readonly QuizContext _quizContext;
        private ILogger<QuestionDetailRepository> _logger;
        public QuestionDetailRepository(QuizContext quizContext, ILogger<QuestionDetailRepository> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }




        public async Task<bool> DeleteQuestionDetails(Questiondetail questiondetail)
        {
            try
            {
                
                var questionIdExist = _quizContext.Questiondetails.Where(x => x.QuestionId == questiondetail.QuestionId);

                if (questionIdExist != null)
                {
                    _quizContext.Questiondetails.Remove(questiondetail);
                    await _quizContext.SaveChangesAsync();
                   
                    return true;
                }
                else
                {
                    throw new NotFoundException("Question doesnot exists to delete");
                }
            }
            catch(Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }



        public async Task<IEnumerable<Questiondetail>> GetActiveQuestionDetail()
        {
            try
            {
                return await _quizContext.Questiondetails.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<IEnumerable<Questiondetail>> GetQuestionDetail()
        {
            try
            {
                return await _quizContext.Questiondetails.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<IEnumerable<Questiondetail>> GetQuestionDetailByCategoryId(int categoryId)
        {
            try
            {
                return await _quizContext.Questiondetails.Where(x => x.CategoryId == categoryId).ToListAsync<Questiondetail>();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<IEnumerable<Questiondetail>> GetQuestionDetailByLevelId(int levelId)
        {
            try
            {
                return await _quizContext.Questiondetails.Where(x => x.LevelId == levelId).ToListAsync<Questiondetail>();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<Questiondetail> GetQuestionDetailByQuestionId(int questionId)
        {
            try
            {
                return await _quizContext.Questiondetails.Where(x => x.QuestionId == questionId).FirstAsync<Questiondetail>();
            }
            catch (Exception excepption)
            {
                throw new NotFoundException(excepption.Message);
            }

        }

      

        public async Task<bool> InsertQuestionDetails(Questiondetail questiondetail)
        {
            try
            {
                if (questiondetail != null && !_quizContext.Questiondetails.Any(x => x.QuestionDescription.ToLower() == questiondetail.QuestionDescription.ToLower()))
                {
                    var addquestion = new Questiondetail()
                    {
                    QuestionDescription = questiondetail.QuestionDescription,
                    IsActive = questiondetail.IsActive,
                    CategoryId = questiondetail.CategoryId,
                    LevelId = questiondetail.LevelId,
                    ImageUrl = questiondetail.ImageUrl,
                    Type = questiondetail.Type,
                    CorrectOption = questiondetail.CorrectOption,
                    OptionA = questiondetail.OptionA,
                    OptionB = questiondetail.OptionB,
                    OptionC = questiondetail.OptionC,
                    OptionD = questiondetail.OptionD,
                    CreatedBy = questiondetail.CreatedBy,
                    CreatedOn = questiondetail.CreatedOn,
                    ModifiedBy = questiondetail.ModifiedBy,
                    ModifiedOn = questiondetail.ModifiedOn,
                    IsRandomSelected = questiondetail.IsRandomSelected
                };

                    var result = _quizContext.Questiondetails.Add(addquestion);
                    await _quizContext.SaveChangesAsync();
                   
                    return true;
                }

                return false;
                
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<bool> UpdateQuestionDetails(Questiondetail questiondetail)
        {
            try
            {
                if (questiondetail != null && !_quizContext.Questiondetails.Any(x => x.QuestionDescription.ToLower() == questiondetail.QuestionDescription.ToLower()))
                {
                    Questiondetail updatequestiondetail = await _quizContext.Questiondetails.FirstAsync<Questiondetail>(s => s.QuestionId == questiondetail.QuestionId);
                    updatequestiondetail.QuestionDescription = questiondetail.QuestionDescription;
                    updatequestiondetail.IsActive = questiondetail.IsActive;
                    updatequestiondetail.CategoryId = questiondetail.CategoryId;
                    updatequestiondetail.LevelId = questiondetail.LevelId;
                    updatequestiondetail.ImageUrl = questiondetail.ImageUrl;
                    updatequestiondetail.Type = questiondetail.Type;
                    updatequestiondetail.CorrectOption = questiondetail.CorrectOption;
                    updatequestiondetail.OptionA = questiondetail.OptionA;
                    updatequestiondetail.OptionB  = questiondetail.OptionB;
                    updatequestiondetail.OptionC = questiondetail.OptionC;
                    updatequestiondetail.OptionD = questiondetail.OptionD;
                    updatequestiondetail.CreatedBy = questiondetail.CreatedBy;
                    updatequestiondetail.CreatedOn = questiondetail.CreatedOn;
                    updatequestiondetail.ModifiedBy = questiondetail.ModifiedBy;
                    updatequestiondetail.ModifiedOn = questiondetail.ModifiedOn;
                    updatequestiondetail.IsRandomSelected = questiondetail.IsRandomSelected;

                    var result = _quizContext.Questiondetails.Update(updatequestiondetail);
                    await _quizContext.SaveChangesAsync();
                   
                    return true;
                }


                return false;

            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

       
        }
    }

