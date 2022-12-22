using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Service
{
    public class TenantmasterService : ITenantmasterService
    {
        private readonly QuizContext _quizContext;
        private ILogger<TenantmasterService> _logger;
        public TenantmasterService(QuizContext quizContext, ILogger<TenantmasterService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }
        public async Task<bool> DeleteTMsAsync(Tenantmaster tm)
        {
            try
            {
                var tmexist = _quizContext.Tenantmasters.Where(x => x.TenantId == tm.TenantId);

                var tenantcompaniessExist = _quizContext.Tenantcompanies.Where(x => x.TenantId == tm.TenantId);

                if (tenantcompaniessExist.Count() > 0)
                {
                    throw new BadRequestException("TenantMaster cannot be delete becuase tenantcompaniess exits related to TenantMaster");
                }
                if (tmexist.Count() == 1)
                {
                    _quizContext.Tenantmasters.Remove(tm);
                    await _quizContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new NotFoundException("TenantMaster is not delete");
                }
            }
            catch
            {
                _logger.LogError("TenantMaster{0} is not delete", tm.TenantId);
                throw;
            }
        }
        public async Task<Tenantmaster> GetTMByIdAsync(int tmid)
        {
            try
            {
                if (tmid > 0)
                {
                    var tenantmaster = await _quizContext.Tenantmasters.Where(x => x.TenantId == tmid && x.IsActive == 1).FirstOrDefaultAsync<Tenantmaster>();
                    if (tenantmaster == null)
                    {
                        throw new NotFoundException(" ID {tmid} not found.");
                    }
                    return tenantmaster;
                }
                else
                {
                    throw new NotFoundException(" ID {tmid} Should be greated than 0.");
                }
            }
            catch
            {
                string response = string.Format("No tenantmaster found with ID = {0}", tmid);
                throw new NotFoundException(response);
            }
        }
        public async Task<IEnumerable<Tenantmaster>> GetTMsAsync()
        {
            try
            {
                var tenantmaster = await _quizContext.Tenantmasters.Where(x => x.IsActive == 1).ToListAsync();
                if (tenantmaster == null)
                {
                    throw new NotFoundException("tenantmaster doesnot exist.");
                }
                return tenantmaster;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }
        public async Task<bool> InsertTMAsync(Tenantmaster tm)
        {
            try
            {
                if (tm != null && !_quizContext.Tenantmasters.Any(x => x.TenantId == tm.TenantId))
                {
                    var result = _quizContext.Tenantmasters.Add(tm);
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
        public async Task<bool> UpdateTMAsync(Tenantmaster tm)
        {
            try
            {
                if (tm != null)
                {
                    var updateTenantMaster = _quizContext.Tenantmasters.Where(s => s.TenantId == tm.TenantId).FirstOrDefault();

                    updateTenantMaster.TenantName = tm.TenantName;
                    updateTenantMaster.Pannumber = tm.Pannumber;
                    updateTenantMaster.Tannumber = tm.Tannumber;
                    updateTenantMaster.Pfreg = tm.Pfreg;
                    updateTenantMaster.Esireg = tm.Esireg;
                    updateTenantMaster.CompanyLocation = tm.CompanyLocation;
                    updateTenantMaster.PasswordExpiry = tm.PasswordExpiry;
                    updateTenantMaster.Address1 = tm.Address1;
                    updateTenantMaster.Address2 = tm.Address2;
                    updateTenantMaster.Address3 = tm.Address3;
                    updateTenantMaster.Pincode = tm.Pincode;
                    updateTenantMaster.Remarks = tm.Remarks;
                    updateTenantMaster.IsActive = tm.IsActive;
                    updateTenantMaster.ModifiedBy = tm.ModifiedBy;
                    updateTenantMaster.ModifiedOn = tm.ModifiedOn;
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
        public void Save()
        {
            _quizContext.SaveChanges();
        }
    }
}
