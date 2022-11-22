using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface IQuestionDetails
    {
        Task<IEnumerable<Questiondetail>> GetQuestionDetail();
        Task<IEnumerable<Questiondetail>> GetActiveQuestionDetail();
        Task<Questiondetail> GetQuestionDetailByQuestionId(int questionId);

        Task<Questiondetail> GetQuestionDetailByCategoryId(int categoryId);

        Task<Questiondetail> GetQuestionDetailByLevelId(int levelId);

       
        //Task<bool> InsertQuestionDetail(List<Questiondetail> lstquestion);
        Task<bool> InsertQuestionDetails(Questiondetail questiondetail);
        Task<bool> UpdateQuestionDetails(Questiondetail questiondetail);
        //Task<bool> UpdateQuestionDetail(List<Questiondetail> lstquestion);
        //Task<bool> DeleteQuestionDetail(int questionId);
        Task<bool> DeleteQuestionDetails(Questiondetail questiondetail);
    }
}
