using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using System.Collections.Generic;

namespace QuizAPiService.Service.Abstract
{
    public interface ITenantCompaniesInterfaceService
    {
        Task<IEnumerable<Tenantcompany>> GetTenantCompanyAsync();
        Task<IEnumerable<Tenantcompany>> GetActiveTenantCompanysAsync();
        Task<Tenantcompany> GetTenantCompanyByNameAsync(string tenantCompanyName);
        Task<bool> InsertTenantCompanyAsync(Tenantcompany tenantCompany);
        Task<bool> UpdateTenantCompanyAsync(Tenantcompany tenantCompany);
        Task<bool> DeleteTenantCompanyAsync(Tenantcompany tenantCompanyId);
    }
}
