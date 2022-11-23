using QuizAPiService.Entities;

namespace QuizAPiService.Service.Abstract
{
    public interface ITenantcompanyService
    {
        Task<IEnumerable<Tenantcompany>> GetTCsAsync();
        Task<Tenantcompany> GetTCByIdAsync(int tcid);
        Task<bool> InsertTCAsync(Tenantcompany tc);
        Task<bool> UpdateTCAsync(Tenantcompany tc);
        Task<bool> DeleteTCsAsync(Tenantcompany tc);
    }
}
