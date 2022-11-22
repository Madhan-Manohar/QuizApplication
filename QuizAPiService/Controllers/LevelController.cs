using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Ocsp;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Security.Claims;

namespace QuizAPiService.Controllers
{
  
    [Route("api/[controller]/[action]")]
    [ApiController]
   public class LevelController : ControllerBase
    {
        // GET: api/<LevelController>
        private readonly ILevelInterfaceService _QuizService;

        public LevelController(ILevelInterfaceService QuizService)
        {
            _QuizService = QuizService;
        }

        [HttpGet]
        public async Task<IEnumerable<Level>> GetLevels()
        {
            return await _QuizService.GetLevelsAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Level>> GetActiveLevels()
        {
            return await _QuizService.GetActiveLevelsAsync();
        }

        [HttpGet]
        public async Task<Level> GetlevelType([FromQuery] String levelType)
        {
            return await _QuizService.GetLevelByTypeAsync(levelType);
        }

        //POST: LevelController/Create
       [HttpPost]
        public async Task<IActionResult> InsertLevel([FromBody] Level level)
        {
            var result = await _QuizService.InsertLevelAsync(level);
            return Ok(result);
        }

        //   POST: LevelController/Create
        [HttpPost]
        public async Task<IActionResult> InsertLevels([FromBody] List<Level> level)
        {
            var result = await _QuizService.InsertLevelAsync(level);
            return Ok();
        }

        // Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateLevel([FromBody] Level level)
        {
            var result = await _QuizService.UpdateLevelAsync(level);
            return Ok(result);
        }

        //// Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateLevels([FromBody] List<Level> level)
        {
            var result = await _QuizService.UpdateLevelAsync(level);
            return Ok();
        }

        //// HttpDelete: LevelController/delete/5
        [HttpDelete]
        public async Task<bool> DeleteLevel([FromBody] Level levelId)
        {
            return await _QuizService.DeleteLevelAsync(levelId);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLevels([FromBody] List<Level> lstLevelId)
        {
            var result = await _QuizService.DeleteLevelAsync(lstLevelId);
            return Ok();
        }   

    }
}
