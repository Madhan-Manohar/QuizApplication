using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface ITenantmasterService
    {
        Task<IEnumerable<Tenantmaster>> GetTMsAsync();
        Task<Tenantmaster> GetTMByIdAsync(int tmid);
        Task<bool> InsertTMAsync(Tenantmaster tm);
        Task<bool> UpdateTMAsync(Tenantmaster tm);
        Task<bool> DeleteTMsAsync(Tenantmaster tm);
    }
}
