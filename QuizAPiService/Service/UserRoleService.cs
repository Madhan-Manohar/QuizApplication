using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web.Http;
using Ubiety.Dns.Core;




namespace QuizAPiService.Service
{
    public class UserRoleService : IUserRoleInterface
    {
        private readonly QuizContext _quizContext;
        private ILogger<UserRoleService> _logger;
        public UserRoleService(QuizContext quizContext, ILogger<UserRoleService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
        }

        public bool DeleteUserroleAsync(int UserRoleId)
        {
            try
            {
                if (UserRoleId > 0)
                {
                    var deleterole = _quizContext.Userroles.Find(UserRoleId);
                    var result = _quizContext.Userroles.Remove(deleterole);
                    _quizContext.SaveChangesAsync();

                    if (deleterole == null)
                    {
                        throw new NotFoundException("Role ID {RoleId} not found to delete.");
                    }
                    return result != null ? true : false;
                }
                else
                {
                    throw new NotFoundException("Role ID {RoleId} Should be greated than 0.");



                }
            
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<Userrole> GetUserroleByIdAsync(int UserRoleId)
        {
            try
            {
                if (UserRoleId > 0)
                {
                    var categoryQuestion = await _quizContext.Userroles.Where(x => x.UserRoleId == UserRoleId && x.IsActive == 1).FirstOrDefaultAsync<Userrole>();
                    if (categoryQuestion == null)
                    {
                        throw new NotFoundException("UserRole ID {UserRoleId} not found.");
                    }
                    return categoryQuestion;
                }
                else
                {
                    throw new NotFoundException("user ID {UserRoleId} Should be greated than 0.");
                }
            }
            catch
            {
                string response = string.Format("No userId found with ID = {0}", UserRoleId);
                throw new NotFoundException(response);
            }

        }

       

        public async Task<IEnumerable<Userrole>> GetUserrolesAsync()
        {
            try
            {
                return await _quizContext.Userroles.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
            
        }

        public async Task<Userrole> InsertUserroleAsync(Userrole userrole)
        {
            try
            {
                if (userrole == null)
                {
                    throw new NotFoundException("Unable to add Userrole}");
                }
                var result = _quizContext.Userroles.Add(userrole);
                await _quizContext.SaveChangesAsync();
                return result.Entity;
            }
            catch
            {
                string response = string.Format("Unable to add user with userrole ID = {0}", userrole.UserRoleId);
                throw new NotFoundException(response);
            }

        }

        public Userrole  UpdateUserroleAsync(Userrole userrole)
        {
            try
            {
                if (userrole == null)
                {
                    throw new NotFoundException("User ID {userrole.UserId} not found.");
                }
                if (userrole.UserRoleId > 0)
                {
                    var result = _quizContext.Userroles.Where(x => x.UserRoleId == userrole.UserRoleId).FirstOrDefault();
                    result.EmployeeId = userrole.EmployeeId;
                    result.CompanyId = userrole.CompanyId;
                    result.IsActive = userrole.IsActive;
                    result.ModifiedBy = userrole.ModifiedBy;
                    result.ModifiedOn = userrole.ModifiedOn;
                    result.CreatedOn = userrole.CreatedOn;
                    result.CreatedBy = userrole.CreatedBy;
                    result.SysStartTime = userrole.SysStartTime;
                    result.SysEndTime = userrole.SysEndTime;
                    result.UserRoleId = userrole.UserRoleId;
                    result.RoleId = userrole.RoleId;

                    var updateUserRoles = _quizContext.Userroles.Update(result);
                    _quizContext.SaveChangesAsync();
                    return updateUserRoles.Entity;
                }
                else
                {
                    throw new NotFoundException("Category ID {categoryQuestion.CategoryId} Should be greater than 0 to update.");
                }

            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }
    }
}
