using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Service
{
    public class TenantcompanyService : ITenantcompanyService
    {
        private readonly QuizContext _quizContext;
        private ILogger<TenantcompanyService> _logger;
        public TenantcompanyService(QuizContext quizContext, ILogger<TenantcompanyService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }

        public async Task<bool> DeleteTCsAsync(Tenantcompany tc)
        {
            try
            {
                var tcexist = _quizContext.Tenantcompanies.Where(x => x.CompanyId == tc.CompanyId);

                if (tcexist.Count() == 1)
                {
                    _quizContext.Tenantcompanies.Remove(tc);
                    await _quizContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new NotFoundException("Tenantcompany doesnot exists to delete");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tenantcompany> GetTCByIdAsync(int tcid)
        {
            try
            {
                if (tcid > 0)
                {
                    var tenantcompany = await _quizContext.Tenantcompanies.Where(x => x.CompanyId == tcid && x.IsActive == 1).FirstOrDefaultAsync<Tenantcompany>();
                    if (tenantcompany == null)
                    {
                        throw new NotFoundException(" ID {tcid} not found.");
                    }
                    return tenantcompany;
                }
                else
                {
                    throw new NotFoundException(" ID {tcid} Should be greated than 0.");
                }
            }
            catch
            {
                string response = string.Format("No tenantcompany found with ID = {0}", tcid);
                throw new NotFoundException(response);
            }
        }

        public async Task<IEnumerable<Tenantcompany>> GetTCsAsync()
        {
            try
            {
                var tenantcompany = await _quizContext.Tenantcompanies.Where(x => x.IsActive == 1).ToListAsync();
                if (tenantcompany == null)
                {
                    throw new NotFoundException("tenantcompany doesnot exist.");
                }
                return tenantcompany;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<bool> InsertTCAsync(Tenantcompany tc)
        {
            try
            {
                if (tc != null && !_quizContext.Tenantcompanies.Any(x => x.CompanyId == tc.CompanyId))
                {
                    var result = _quizContext.Tenantcompanies.Add(tc);
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

        public async Task<bool> UpdateTCAsync(Tenantcompany tc)
        {
            try
            {
                if (tc != null)
                {
                    var update = _quizContext.Tenantcompanies.Where(s => s.CompanyId == tc.CompanyId).FirstOrDefault();

                    update.CompanyName = tc.CompanyName;
                    update.CompanyCode = tc.CompanyCode;
                    update.Pannumber = tc.Pannumber;
                    update.Tannumber = tc.Tannumber;
                    update.Pfreg = tc.Pfreg;
                    update.Esireg = tc.Esireg;
                    update.State = tc.State;
                    update.MobileNumber = tc.MobileNumber;
                    update.TenantId = tc.TenantId;
                    update.IsActive = tc.IsActive;
                    update.Address1 = tc.Address1;
                    update.Address2 = tc.Address2;
                    update.Address3 = tc.Address3;
                    update.Pincode = tc.Pincode;
                    update.Remarks = tc.Remarks;
                    update.CreatedBy = tc.CreatedBy;
                    update.CreatedOn = tc.CreatedOn;
                    update.ModifiedBy = tc.ModifiedBy;
                    update.ModifiedOn = tc.ModifiedOn;

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
