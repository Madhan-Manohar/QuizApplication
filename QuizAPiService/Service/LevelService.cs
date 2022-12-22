using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Ubiety.Dns.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuizAPiService.Service
{
    public class LevelService : ILevelInterfaceService
    {
        private readonly QuizContext _quizContext;
        public LevelService(QuizContext quizContext)
        {
            _quizContext = quizContext;
        }

        #region Level
        /// <summary>
        /// Get all the Levels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<Level>> GetLevelsAsync()
        {
            try
            {
                return await _quizContext.Levels.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        /// <summary>
        /// Get all the Active Levels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<Level>> GetActiveLevelsAsync()
        {
            try
            {
                return await _quizContext.Levels.Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new NotFoundException(exception.Message);
            }

        }
        /// <summary>
        /// Get  the Level by passing level Type
        /// </summary>
        /// <param name="levelType"></param>
        /// <returns></returns>
        public async Task<Level> GetLevelByTypeAsync(string levelType)
        {
            try
            {
                if (!string.IsNullOrEmpty(levelType))
                {
                    return await _quizContext.Levels.Where(x => x.LevelType.ToLower() == levelType.ToLower()).FirstAsync<Level>();                
                }
                else
                {
                    throw new NotFoundException("Level cannot fetched");
                }
            }
            catch
            {
                throw ;
            }

        }

        /// <summary>
        /// Insert the Level by passing the Level Object
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> InsertLevelAsync(Level level)
        {
            try
            {
                if (level != null && !_quizContext.Levels.Any(x => x.LevelType.ToLower() == level.LevelType.ToLower()))
                {
                    var addLevel = new Level()
                    {
                        LevelType = level.LevelType,
                        IsActive = level.IsActive,
                        CreatedBy = level.CreatedBy,
                        CreatedOn = level.CreatedOn,
                        ModifiedBy= level.ModifiedBy,
                        ModifiedOn = level.ModifiedOn
                    };

                    var result = _quizContext.Levels.Add(addLevel);
                    await _quizContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NotFoundException("LevelType is not Inserted becuase levelType not exist or Inserting with existing ");
                }
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Insert the Levels by passing the list of Level Object
        /// </summary>
        /// <param name="lstLevel"></param>
        /// <returns></returns>
        public async Task<bool> InsertLevelAsync(List<Level> lstLevel)
        {
            try
            {
                var distinctLevels = lstLevel.GroupBy(x => x.LevelType).Select(y => y.First());
                var existDuplicate = _quizContext.Levels.Any(t => !lstLevel.Select(l => l.LevelType.ToLower()).Contains(t.LevelType.ToLower()));

                if (distinctLevels.Count() == lstLevel.Count() && (existDuplicate || _quizContext.Levels.Count()==0))
                {
                    await Task.WhenAll(lstLevel.Select(async level =>
                    {
                       await InsertLevelAsync(level);
                     }));
                    return true;
                 }
                else
                {  throw new BadRequestException("LevelType(s) are not Inserted because levelType not exist or Inserting with duplicate");
                }
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Update the Level by passing the Level Object
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLevelAsync(Level level)
        {
            try
            {
                var levelType = _quizContext.Levels.Any(x => x.LevelType.ToLower() == level.LevelType.ToLower());
                var levelIsActive = _quizContext.Levels.Any(x => x.IsActive == level.IsActive);
                if (level != null && ((levelType && !levelIsActive) ||(!levelType)))
                {
                    Level updateLevel = await _quizContext.Levels.FirstAsync<Level>(s => s.LevelId == level.LevelId);
                    if (updateLevel != null)
                    {
                        var quizDetailsExist1 = _quizContext.Levels.Include(p => p.Quizdetails.Where(x => x.IsActive == 1));
                        var quizDetailsExist = _quizContext.Quizdetails.Where(x => x.LevelId == level.LevelId && x.IsActive==1);
                        var questiondetailsExist = _quizContext.Questiondetails.Where(x => x.LevelId == level.LevelId && x.IsActive == 1);
                        if (level.IsActive == 0 &&  questiondetailsExist.Count() > 0)
                        {  throw new BadRequestException("Level cannot be update as inactive because Question details have active records related to Level");
                        }
                        if (level.IsActive==0 && quizDetailsExist.Count() > 0)
                        {   throw new BadRequestException("Level cannot be update as inactive because Quiz details have active records related to Level");
                        }
                        updateLevel.LevelType = level.LevelType;
                        updateLevel.IsActive = level.IsActive;
                        updateLevel.ModifiedBy = level.ModifiedBy;
                        updateLevel.ModifiedOn = level.ModifiedOn;
                        var result = _quizContext.Levels.Update(updateLevel);
                        await _quizContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new NotFoundException("LevelType is not Updated becuase levelType not exist");
                    }
                }
                else
                {
                    throw new BadRequestException("LevelType is not Updated becuase levelType not exist or updating with existing ");
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update the Levels by passing the list of Level Object
        /// </summary>
        /// <param name="lstLevel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLevelAsync(List<Level> lstLevel)
        {
            try
            {
                var distinctLevels = lstLevel.GroupBy(x => x.LevelType).Select(y => y.First());
                var existDuplicate = _quizContext.Levels.Any(t => !lstLevel.Select(l => l.LevelType.ToLower()).Contains(t.LevelType.ToLower()));


                if (distinctLevels.Count() == lstLevel.Count() && (existDuplicate || _quizContext.Levels.Count() == 0))
                {
                    await Task.WhenAll(lstLevel.Select(async level =>
                    {
                        await UpdateLevelAsync(level);
                     }));
                    return true;
                }
                else
                {
                    throw new NotFoundException("LevelType(s) are not Updated becuase levelType(s) not exist");
                }
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Delete the Level by passing the Level Object
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLevelAsync(Level level)
        {
            try
            {
                var levelIdExist = _quizContext.Levels.Where(x => x.LevelId== level.LevelId);
                var quizDetailsExist = _quizContext.Quizdetails.Where(x => x.LevelId == level.LevelId);
                var questiondetailsExist = _quizContext.Questiondetails.Where(x => x.LevelId == level.LevelId);
                if(questiondetailsExist.Count() > 0)
                {
                    throw new BadRequestException("Level cannot be delete becuase Questions exits related to Level");
                }
                if (quizDetailsExist.Count() > 0)
                {
                    throw new NotFoundException("Level cannot be delete because Quiz exits related to Level");
                }
                if (levelIdExist.Count()==1)
                {
                    _quizContext.Levels.Remove(level);
                    await _quizContext.SaveChangesAsync();                    
                    return true;
                }
                else
                {
                    throw new NotFoundException("LevelType is not delete");
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update the Levels by passing list of the Level Object
        /// </summary>
        /// <param name="lstLevel"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLevelAsync(List<Level> lstLevel)
        {
            try
            {
              
                var lstLevelIdExist = _quizContext.Levels.Where(x => lstLevel.Select(c => c.LevelId).ToList().Contains(x.LevelId)).ToList();
                var lstLevelIdExistinQuiz = lstLevel.Where(x => _quizContext.Quizdetails.Select(c => c.LevelId).ToList().Contains(x.LevelId)).ToList();
                var lstLevelIdExistinQuestion = lstLevel.Where(x => _quizContext.Questiondetails.Select(c => c.LevelId).ToList().Contains(x.LevelId)).ToList();


                if (lstLevelIdExistinQuestion.Count() > 0)
                {
                    throw new BadRequestException("Level(s) cannot be delete becuase Questions details exits related to Level");
                }
                if (lstLevelIdExistinQuiz.Count() > 0)
                {
                    throw new NotFoundException("Level(s) cannot be delete because Quiz details exits related to Level");
                }

                if (lstLevel.Count() == lstLevelIdExist.Count())
                {
                    _quizContext.Levels.RemoveRange(lstLevel);
                    await _quizContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NotFoundException("Level(s) are not able to delete due to unavailability ");
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



