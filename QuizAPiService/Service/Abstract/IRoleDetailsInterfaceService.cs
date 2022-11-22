using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface IRoleDetailsInterfaceService
    {
        Task<IEnumerable<Roledetail>> GetRoleDetailsAsync();
        Task<Roledetail> GetRoleDetailByIdAsync(int RoleId);
        Task<Roledetail> GetRoleDetailByTypeAsync(string RoleDescription);
        Task<Roledetail> InsertRoleDetailAsync(Roledetail roledetail);
        Roledetail UpdateRoleDetailAsync(Roledetail roledetail);
        bool DeleteRoleDetailsAsync(int RoleId);
       
    }
}
