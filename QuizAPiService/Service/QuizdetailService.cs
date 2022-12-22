using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System.ComponentModel.Design;
using System.Web.Http.Results;

namespace QuizAPiService.Service
{
    public class QuizdetailService : IQuizdetailService
    {
        private readonly QuizContext _quizContext;
        private ILogger<QuizdetailService> _logger;
        public QuizdetailService(QuizContext quizContext, ILogger<QuizdetailService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }
        
        private void Save()
        {
            _quizContext.SaveChanges();
        }

        public async Task<Quizdetail> GetQuizdetailByIdAsync(int quizId)
        {
            try
            {
                if (quizId > 0)
                {
                    var quizdetail = await _quizContext.Quizdetails.Where(x => x.QuizId == quizId && x.IsActive == 1).FirstOrDefaultAsync<Quizdetail>();
                    if (quizdetail == null)
                    {
                        throw new NotFoundException("Quiz ID {quizId} not found.");
                    }
                    return quizdetail;
                }
                else
                {
                    throw new NotFoundException("Quiz ID {quizId} Should be greated than 0.");
                }
            }
            catch
            {
                string response = string.Format("No QuizId found with ID = {0}", quizId);
                throw new NotFoundException(response);
            }
        }

        public async Task<IEnumerable<Quizdetail>> GetQuizdetailsAsync()
        {
            try
            {
                var quiz = await _quizContext.Quizdetails.Where(x => x.IsActive == 1).ToListAsync();
                if (quiz == null)
                {
                    throw new NotFoundException("QuizDetails doesnot exist.");
                }
                return quiz;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public bool DeleteQDsAsync(int quizId)
        {
            try
            {
                if (quizId > 0)
                {
                    var deleterole = _quizContext.Quizdetails.Find(quizId);

                    if (deleterole == null)
                    {
                        throw new NotFoundException("Quiz ID {QuizId} not found to delete.");
                    }
                    else
                    {
                        var result = _quizContext.Quizdetails.Remove(deleterole);
                        Save();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<bool> InsertQuizdetailAsync(Quizdetail quizdetail)
        {
            try
            {
                if (quizdetail != null && !_quizContext.Quizdetails.Any(x => x.QuizId == quizdetail.QuizId))
                {
                   
                    var result = _quizContext.Quizdetails.Add(quizdetail);
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

        public async Task<bool> UpdateQuizdetailAsync(Quizdetail quizdetail)
        {
            try
            {
                if (quizdetail != null)
                {
                    var result = _quizContext.Quizdetails.Where(s => s.QuizId == quizdetail.QuizId).FirstOrDefault();

                    result.CategoryId = quizdetail.CategoryId;
                    result.LevelId = quizdetail.LevelId;
                    result.Userid = quizdetail.Userid;
                    result.CompanyId = quizdetail.CompanyId;
                    result.ExpiresOn = quizdetail.ExpiresOn;
                    result.Status = quizdetail.Status;
                    result.TotalScore = quizdetail.TotalScore;
                    result.SecureScore = quizdetail.SecureScore;
                    result.IsActive = quizdetail.IsActive;
                    result.ModifiedBy = quizdetail.ModifiedBy;
                    result.ModifiedOn = quizdetail.ModifiedOn;
                    Save();

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