using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using System.Collections.Generic;

namespace QuizAPiService.Service.Abstract
{
    public interface ITenantMasterInterfaceService
    {
        Task<IEnumerable<Tenantmaster>> GetTenantMasterAsync();
        Task<IEnumerable<Tenantmaster>> GetActiveTenantMastersAsync();
        Task<Tenantmaster> GetTenantMasterByNameAsync(string tenantMasterName);
        Task<bool> InsertTenantMasterAsync(Tenantmaster tenantMaster);
        Task<bool> UpdateTenantMasterAsync(Tenantmaster tenantMaster);
        Task<bool> DeleteTenantMasterAsync(Tenantmaster tenantMasterId);

    }
}
