using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System;

namespace QuizAPiService.Service
{
    public class TenantCompanyService: ITenantCompaniesInterfaceService
    {


        private readonly QuizContext _quizContext;
        //  private ILogger<QuizService> _logger;
        public TenantCompanyService(QuizContext quizContext)
        {
            _quizContext = quizContext;
            // _logger = logger;
        }
        #region TenantCompany
        public async Task<IEnumerable<Tenantcompany>> GetTenantCompanyAsync()
        {
            try
            {
                return await _quizContext.Tenantcompanies.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<IEnumerable<Tenantcompany>> GetActiveTenantCompanysAsync()
        {
            try
            {
                return await _quizContext.Tenantcompanies.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<Tenantcompany> GetTenantCompanyByNameAsync(string tenamtCompanyName)
        {
            try
            {
                return await _quizContext.Tenantcompanies.Where(x => x.CompanyName.ToLower() == tenamtCompanyName.ToLower() && x.IsActive == 1).FirstAsync<Tenantcompany>();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        public async Task<bool> InsertTenantCompanyAsync(Tenantcompany tenantCompany)
        {
            try
            {
                if (tenantCompany != null && !_quizContext.Tenantcompanies.Any(x => x.CompanyName.ToLower() == tenantCompany.CompanyName.ToLower()))
                {
                    var addTenantCompany = new Tenantcompany()
                    {
                        CompanyName = tenantCompany.CompanyName,
                        Pannumber = tenantCompany.Pannumber,
                        CompanyCode=tenantCompany.CompanyCode,
                        TenantId=tenantCompany.TenantId,
                        State =tenantCompany.State,
                        MobileNumber=tenantCompany.MobileNumber,
                        Tannumber = tenantCompany.Tannumber,
                        Pfreg = tenantCompany.Pfreg,
                        Esireg = tenantCompany.Esireg,
                        IsActive = tenantCompany.IsActive,
                        Address1 = tenantCompany.Address1,
                        Address2 = tenantCompany.Address2,
                        Address3 = tenantCompany.Address3,
                        Pincode = tenantCompany.Pincode,
                        Remarks = tenantCompany.Remarks,
                        CreatedBy = tenantCompany.CreatedBy,
                        CreatedOn = tenantCompany.CreatedOn,
                        ModifiedBy = 0,
                        ModifiedOn = null
                    };
                    
                    var result = _quizContext.Tenantcompanies.Add(addTenantCompany);
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
        public async Task<bool> UpdateTenantCompanyAsync(Tenantcompany tenantCompany)
        {
            try
            {
                if (tenantCompany != null && !_quizContext.Tenantcompanies.Any(x => x.CompanyName.ToLower() == tenantCompany.CompanyName.ToLower()))
                {
                    Tenantcompany updateTenantCompany = await _quizContext.Tenantcompanies.FirstAsync<Tenantcompany>(s => s.CompanyId == tenantCompany.CompanyId);
                    updateTenantCompany.CompanyName = tenantCompany.CompanyName;
                     updateTenantCompany.CompanyCode = tenantCompany.CompanyCode;
                    updateTenantCompany.TenantId = tenantCompany.TenantId;
                    updateTenantCompany.State = tenantCompany.State;
                    updateTenantCompany.MobileNumber = tenantCompany.MobileNumber;
                    updateTenantCompany.Pannumber = tenantCompany.Pannumber;
                    updateTenantCompany.Tannumber = tenantCompany.Tannumber;
                    updateTenantCompany.Pfreg = tenantCompany.Pfreg;
                    updateTenantCompany.Esireg = tenantCompany.Esireg;
                    updateTenantCompany.Address1 = tenantCompany.Address1;
                    updateTenantCompany.Address2 = tenantCompany.Address2;
                    updateTenantCompany.Address3 = tenantCompany.Address3;
                    updateTenantCompany.Pincode = tenantCompany.Pincode;
                    updateTenantCompany.Remarks = tenantCompany.Remarks;
                    updateTenantCompany.IsActive = tenantCompany.IsActive;
                    updateTenantCompany.ModifiedBy = tenantCompany.ModifiedBy;
                    updateTenantCompany.ModifiedOn = tenantCompany.ModifiedOn;
                    var result = _quizContext.Tenantcompanies.Update(updateTenantCompany);
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
        public async Task<bool> DeleteTenantCompanyAsync(Tenantcompany tenantCompany)
        {
            try
            {
                if (tenantCompany != null)
                {
                    _quizContext.Tenantcompanies.Remove(tenantCompany);
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

        #endregion
    }
}
