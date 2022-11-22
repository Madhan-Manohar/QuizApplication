using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI.Common;
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
    public class RoleDetailsService : IRoleDetailsInterfaceService
    {
        

        private readonly QuizContext _quizContext;
        private ILogger<RoleDetailsService> _logger;
        

        public RoleDetailsService(QuizContext quizContext, ILogger<RoleDetailsService> logger)
        {
            _quizContext = quizContext;
            _logger = logger;
            
        }

        public bool DeleteRoleDetailsAsync(int RoleId)
        {
           
            //var query1 = from gan in _quizContext.Roledetails
            //          where gan.RoleId == RoleId
            //          && gan.IsActive == 1
            //          select gan;
            var query2 = from gan in _quizContext.Userroles
                       where gan.RoleId == RoleId
                       select gan;

            //bool one = query1.Any();
            bool query2_return = query2.Any();


            try
            {
                    if (RoleId > 0)
                    {
                        var deleterole = _quizContext.Roledetails.Find(RoleId);
                        
                        if (deleterole == null)
                        {
                            throw new NotFoundException("Role ID {RoleId} not found to delete.");
                        }

                        else if (query2_return == true)
                        {
                            throw new NotFoundException("Role ID Cannot be deleted Since the user in Still active");

                        }
                        else
                        {
                            var result = _quizContext.Roledetails.Remove(deleterole);
                            Save();
                            return result != null ? true : false;
                        }
                        return false;

                        //return result != null ? true : false;
                    }
                else
                {
                    return false;
                    throw new NotFoundException("Role ID Cannot be deleted Since the user in Still active");
                }
                //    else
                //    {
                //        return false;
                //        Console.WriteLine("Role ID {RoleId} Should be greated than 0.");
                //        //throw new NotFoundException("Role ID {RoleId} Should be greated than 0.");

                //    }



                //if (RoleId > 0)
                //{
                //    var deleterole = _quizContext.Roledetails.Find(RoleId);
                //    var result = _quizContext.Roledetails.Remove(deleterole);
                //    Save();
                //    //return true;
                //    //await _quizContext.SaveChangesAsync();

                //    if (deleterole == null)
                //    {
                //        throw new NotFoundException("Role ID {RoleId} not found to delete.");
                //    }
                //    return result != null ? true : false;
                //}
                //else
                //{
                //    return false;
                //    Console.WriteLine("Role ID {RoleId} Should be greated than 0.");
                //    //throw new NotFoundException("Role ID {RoleId} Should be greated than 0.");

                //}
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        
        public async Task<IEnumerable<Roledetail>> GetRoleDetailsAsync()
        {
            try
            {
                var level = await _quizContext.Roledetails.Where(x => x.IsActive == 1).Include(p=>p.Userroles).ToListAsync();
                if (level == null)
                {
                    throw new NotFoundException("Role Detail doesnot exist.");
                }
                return level;
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public async Task<Roledetail> GetRoleDetailByIdAsync(int RoleId)
        {
            try
            {
                if (RoleId > 0)
                {
                    var level = await _quizContext.Roledetails.Where(x => x.RoleId == RoleId && x.IsActive == 1).FirstOrDefaultAsync<Roledetail>();
                    if (level == null)
                    {
                        throw new NotFoundException("Role ID {RoleId} not found.");
                    }
                    return level;
                }
                else
                {
                    throw new NotFoundException("Role ID {RoleId} Should be greated than 0.");
                }
            }
            catch
            {
                string response = string.Format("No RoleId found with ID = {0}", RoleId);
                throw new NotFoundException(response);
            }
        }

        public async Task<Roledetail> GetRoleDetailByTypeAsync(string RoleDescription)
        {
            try
            {
                if (!string.IsNullOrEmpty(RoleDescription))
                {
                    var level = await _quizContext.Roledetails.Where(x => x.RoleDescription == RoleDescription && x.IsActive == 1).FirstOrDefaultAsync<Roledetail>();
                    if (level == null)
                    {
                        throw new NotFoundException("Role Type {RoleType} doesnot exist.");
                    }
                    return level;
                }
                else
                {
                    throw new NotFoundException("Role Type {RoleType} Should not be null or empty.");
                }
            }
            catch
            {
                string response = string.Format("No LevelId found with LevelType = {0}", RoleDescription);
                throw new NotFoundException(response);
            }
        }

      

        public async Task<Roledetail> InsertRoleDetailAsync(Roledetail roledetail)
        {
            try
            {

                
                if (roledetail == null)
                {
                    throw new NotFoundException("Unable to add Role Detail}");
                }
                //if (condition == null)
                //{
                //    throw new NotFoundException("Unable to add Role Detail}");
                //}
                //else if ()
                //{
                //    throw new NotFoundException("Role Description is already available}");
                //}

                //    var result = _quizContext.Roledetails.Where(x => x.RoleId == roledetail.RoleId).FirstOrDefault();
                //    result.RoleDescription = roledetail.RoleDescription;
                //    //result.Userroles = roledetail.Userroles;
                //    result.Status = roledetail.Status;
                //    result.CreatedOn = roledetail.CreatedOn;
                //    result.CreatedBy = roledetail.CreatedBy;
                //    result.IsActive = roledetail.IsActive;
                //    result.ModifiedBy = roledetail.ModifiedBy;
                //    result.ModifiedOn = roledetail.ModifiedOn;

                var result = _quizContext.Roledetails.Add(roledetail);
                await _quizContext.SaveChangesAsync();
                return result.Entity;


            }
            catch
            {
                string response = string.Format("Unable to add Role Detail with ID = {0}", roledetail);
                throw new NotFoundException(response);
            }

        }

        //public async Task<Roledetail> UpdateRoleDetailAsync(Roledetail roledetail)
        public Roledetail UpdateRoleDetailAsync(Roledetail roledetail)
        {

            try
            {
                
                if (roledetail == null)
                {
                    throw new NotFoundException("Role ID {RoleId} not found.");
                }
                if (roledetail.RoleId > 0)
                {
                    var result = _quizContext.Roledetails.Where(x => x.RoleId == roledetail.RoleId).FirstOrDefault(); 
                    result.RoleDescription = roledetail.RoleDescription;
                    //result.Userroles = roledetail.Userroles;
                    result.Status = roledetail.Status;
                    //result.CreatedOn = roledetail.CreatedOn;
                    //result.CreatedBy = roledetail.CreatedBy;
                    result.IsActive = roledetail.IsActive;
                    result.ModifiedBy = roledetail.ModifiedBy;
                    result.ModifiedOn = roledetail.ModifiedOn;

                    //_quizContext.Roledetails.Update(roledetail);
                    _quizContext.SaveChangesAsync();
                    
                    return roledetail;
                }
                else
                {
                    throw new NotFoundException("Role ID {RoleId} Should be greated than 0 to update.");
                }

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
