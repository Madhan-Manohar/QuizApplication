using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;
using System.Collections.Generic;

namespace QuizAPiService.Service.Abstract
{
    public interface ILevelInterfaceService
    {
        Task<IEnumerable<Level>> GetLevelsAsync();
        Task<IEnumerable<Level>> GetActiveLevelsAsync();
        Task<Level> GetLevelByTypeAsync(string levelType);
        Task<bool> InsertLevelAsync(List<Level> level);
        Task<bool> InsertLevelAsync(Level level);
        Task<bool> UpdateLevelAsync(Level level);
        Task<bool> UpdateLevelAsync(List<Level> level);
        Task<bool> DeleteLevelAsync(Level levelId);
        Task<bool> DeleteLevelAsync(List<Level> lstLevelId);
    }
}
