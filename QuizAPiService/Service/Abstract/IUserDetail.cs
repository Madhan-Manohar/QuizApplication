using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface IUserDetail
    {
        Task<IEnumerable<Userdetail>> GetUserdetails();

        Task<IEnumerable<Userdetail>> GetActiveUsers();
        Task<Userdetail> GetUserDetailByUserId(int userId);

        Task<Userdetail> GetUserDetailByEmployeeeId(int employeeId);

        Task<bool> InsertUserDetail(Userdetail userdetail);
        Task<bool> UpdateUserDetail(Userdetail userdetail);
   
        Task<bool> DeleteUserDetail(Userdetail userdetail);
       
    }
}