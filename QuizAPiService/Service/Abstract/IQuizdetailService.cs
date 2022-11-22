using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface IQuizdetailService
    {
        Task<IEnumerable<Quizdetail>> GetQuizdetailsAsync();

        Task<Quizdetail> GetQuizdetailByIdAsync(int quizId);
        //Task<Quizdetail> InsertQuizdetailAsync(Quizdetail quizdetail);
        Task<bool> InsertQuizdetailAsync(Quizdetail quizdetail);
        //Task<Quizdetail> UpdateQuizdetailAsync(Quizdetail quizdetail);
        Task<bool> UpdateQuizdetailAsync(Quizdetail quizdetail);
        Task<bool> DeleteQuizDetailsAsync(Quizdetail Quiz);
    }
}
