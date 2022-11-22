using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Service
{
    public class TenantMasterService : ITenantMasterInterfaceService
    {
        private readonly QuizContext _quizContext;
      //  private ILogger<QuizService> _logger;
        public TenantMasterService(QuizContext quizContext)
        {
            _quizContext = quizContext;
           // _logger = logger;
        }
        #region TenantMaster
        public async Task<IEnumerable<Tenantmaster>> GetTenantMasterAsync()
        {
            try
            {
                return await _quizContext.Tenantmasters.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<IEnumerable<Tenantmaster>> GetActiveTenantMastersAsync()
        {
            try
            {
                return await _quizContext.Tenantmasters.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<Tenantmaster> GetTenantMasterByNameAsync(string tenamtMasterName)
        {
            try
            {
                return await _quizContext.Tenantmasters.Where(x => x.TenantName.ToLower() == tenamtMasterName.ToLower() && x.IsActive == 1).FirstAsync<Tenantmaster>();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<bool> InsertTenantMasterAsync(Tenantmaster tenantMaster)
        {
            try
            {
                if (tenantMaster != null && !_quizContext.Tenantmasters.Any(x => x.TenantName.ToLower() == tenantMaster.TenantName.ToLower()))
                {
                    var addTenantMaster = new Tenantmaster()
                    {
                        TenantName = tenantMaster.TenantName,
                        Pannumber = tenantMaster.Pannumber,
                        Tannumber = tenantMaster.Tannumber,
                        Pfreg = tenantMaster.Pfreg,
                        Esireg = tenantMaster.Esireg,
                        IsActive = tenantMaster.IsActive,
                        CompanyLo = tenantMaster.CompanyLo,
                        PasswordExpiry = tenantMaster.PasswordExpiry,
                        Address1 = tenantMaster.Address1,
                        Address2 = tenantMaster.Address2,
                        Address3 = tenantMaster.Address3,
                        Pincode = tenantMaster.Pincode,
                        Remarks = tenantMaster.Remarks,
                        CreatedBy = tenantMaster.CreatedBy,
                        CreatedOn = tenantMaster.CreatedOn,
                        ModifiedBy = 0,
                        ModifiedOn = null
                    };
                    var result = _quizContext.Tenantmasters.Add(addTenantMaster);
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
        public async Task<bool> UpdateTenantMasterAsync(Tenantmaster tenantMaster)
        {
            try
            {
                if (tenantMaster != null && !_quizContext.Tenantmasters.Any(x => x.TenantName.ToLower() == tenantMaster.TenantName.ToLower()))
                {
                    Tenantmaster updateTenantMaster = await _quizContext.Tenantmasters.FirstAsync<Tenantmaster>(s => s.TenantId == tenantMaster.TenantId);
                    updateTenantMaster.TenantName = tenantMaster.TenantName;
                    updateTenantMaster.Pannumber = tenantMaster.Pannumber;
                    updateTenantMaster.Tannumber = tenantMaster.Tannumber;
                    updateTenantMaster.Pfreg = tenantMaster.Pfreg;
                    updateTenantMaster.Esireg = tenantMaster.Esireg;
                    updateTenantMaster.CompanyLo = tenantMaster.CompanyLo;
                    updateTenantMaster.PasswordExpiry = tenantMaster.PasswordExpiry;
                    updateTenantMaster.Address1 = tenantMaster.Address1;
                    updateTenantMaster.Address2 = tenantMaster.Address2;
                    updateTenantMaster.Address3 = tenantMaster.Address3;
                    updateTenantMaster.Pincode = tenantMaster.Pincode;
                    updateTenantMaster.Remarks = tenantMaster.Remarks;
                    updateTenantMaster.IsActive = tenantMaster.IsActive;
                    updateTenantMaster.ModifiedBy = tenantMaster.ModifiedBy;
                    updateTenantMaster.ModifiedOn = tenantMaster.ModifiedOn;
                    var result = _quizContext.Tenantmasters.Update(updateTenantMaster);
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
        public async Task<bool> DeleteTenantMasterAsync(Tenantmaster tenantMaster)
        {
            try
            {
                var levelIdExist = _quizContext.Tenantmasters.Where(x => x.TenantId == tenantMaster.TenantId);

                if (levelIdExist.Count() == 1)
                {
                    _quizContext.Tenantmasters.Remove(tenantMaster);
                    await _quizContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NotFoundException("TenantMaster doesnot exists to delete");
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
