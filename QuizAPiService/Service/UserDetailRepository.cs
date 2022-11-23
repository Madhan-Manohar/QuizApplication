using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;

namespace QuizAPiService.Service
{
    
    
        
          public class UserDetailRepository : IUserDetail
         {

            private readonly QuizContext _quizContext; 
            private ILogger<UserDetailRepository> _logger; 
        public UserDetailRepository(QuizContext quizContext, ILogger<UserDetailRepository> logger)

        { _quizContext = quizContext;
            _logger = logger; }




        public async Task<bool> DeleteUserDetail(Userdetail userdetail)
        {
            try
            {
                
                var UserIdExist = _quizContext.Userdetails.Where(x => x.UserId == userdetail.UserId);

                if (UserIdExist != null)
                {
                    _quizContext.Userdetails.Remove(userdetail);
                    await _quizContext.SaveChangesAsync();
                    
                    return true;
                }
                else
                {
                    throw new NotFoundException("UserId doesnot exists to delete");
                }
            }
            catch
            {
                throw;
            }
        }



        public async Task<IEnumerable<Userdetail>> GetActiveUsers()
        {
            try
            {
                return await _quizContext.Userdetails.Where(x => x.IsActive  == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }


        public async Task<Userdetail> GetUserDetailByUserId(int userId)
        {
            try
            {
                return await _quizContext.Userdetails.Where(x => x.UserId == userId).FirstAsync<Userdetail>();
            }
            catch (Exception excepption)
            {
                throw new NotFoundException(excepption.Message);
            }

        }

        public async Task<Userdetail> GetUserDetailByEmployeeeId(int employeeId)
        {
            try
            {
                return await _quizContext.Userdetails.Where(x => x.EmployeeId == employeeId).FirstAsync<Userdetail>();
            }
            catch (Exception excepption)
            {
                throw new NotFoundException(excepption.Message);
            }

        }


        public async Task<IEnumerable<Userdetail>> GetUserdetails()
        {
            try
            {
                return await _quizContext.Userdetails.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }

        public async Task<bool> InsertUserDetail(Userdetail userdetail)
        {
            try
            {
                if (userdetail != null && !_quizContext.Userdetails.Any(x => x.UserId == userdetail.UserId))
                {
                    var addUser = new Userdetail()
                    {
                        UserId = userdetail.UserId,
                        IsActive =userdetail.IsActive,
                        EmployeeId = userdetail.EmployeeId,
                        EmployeeName = userdetail.EmployeeName,
                        Email  = userdetail.Email,
                        Gender = userdetail.Gender,
                        CompanyId = userdetail.CompanyId,
                        
                        CreatedBy = userdetail.CreatedBy,
                        CreatedOn = userdetail.CreatedOn,
                        ModifiedBy = userdetail.ModifiedBy,
                       

                        ModifiedOn = userdetail.ModifiedOn 
                    };

                    var result = _quizContext.Userdetails.Add(addUser);
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

        public async Task<bool> UpdateUserDetail(Userdetail userdetail)
        {
            try
            {
                if (userdetail != null && !_quizContext.Userdetails.Any(x => x.EmployeeName.ToLower()   == userdetail.EmployeeName.ToLower()))
                {
                    Userdetail updateuser = await _quizContext.Userdetails.FirstAsync<Userdetail>(s => s.UserId == userdetail.UserId);
                    updateuser.UserId = userdetail.UserId;
                    updateuser.Email  = userdetail.Email;
                    updateuser.IsActive = userdetail.IsActive;
                    updateuser.ModifiedBy = userdetail.ModifiedBy;
                    updateuser.ModifiedOn  = userdetail.ModifiedOn;
                    updateuser.CreatedOn  = userdetail.CreatedOn;
                    updateuser.CreatedBy = userdetail.CreatedBy;
                    updateuser.CompanyId = userdetail.CompanyId;
                    updateuser.EmployeeName  = userdetail.EmployeeName;
                    updateuser.EmployeeId = userdetail.EmployeeId;
                    updateuser.CompanyId  = userdetail.CompanyId;
                    
                    var result = _quizContext.Userdetails.Update(updateuser);
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
    }
}
