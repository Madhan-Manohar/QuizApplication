using QuizAPiService.Entities;



namespace QuizAPiService.Service.Abstract
{
    public interface IUserRoleInterface
    {


        Task<IEnumerable<Userrole>> GetUserrolesAsync();
        Task<Userrole> GetUserroleByIdAsync(int userId);

        Task<Userrole> InsertUserroleAsync(Userrole userrole);
        Userrole UpdateUserroleAsync(Userrole userrole);
        bool DeleteUserroleAsync(int UserRoleId);

       

    }
}
