using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        // GET: api/<LevelController>
        private readonly IQuizInterfaceService _QuizService;

        public LevelController(IQuizInterfaceService QuizService)
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
        public async Task<Level> GetLevelById([FromBody] Level level)
        {
            return await _QuizService.GetLevelByIdAsync(level.LevelId);
        }

        // POST: LevelController/Create
        [HttpPost]
        public async Task<IActionResult> InsertLevel([FromBody] Level level)
        {
            await _QuizService.InsertLevelAsync(level);
            return Ok();
        }

        // POST: LevelController/Create
        [HttpPost]
        public async Task<IActionResult> InsertLevels([FromBody] List<Level> level)
        {
            await _QuizService.InsertLevelAsync(level);
            return Ok();
        }

        // Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateLevel([FromBody] Level level)
        {
            await _QuizService.UpdateLevelAsync(level);
            return Ok();
        }

        // Put: LevelController/Edit/5
        [HttpPut]
        public async Task<IActionResult> UpdateLevels([FromBody] List<Level> level)
        {
            await _QuizService.UpdateLevelAsync(level);
            return Ok();
        }

        //// HttpDelete: LevelController/delete/5
        //[ServiceFilter(typeof(CustomExceptionFilter))]
        [HttpDelete]
        public async Task<HttpStatusCode> DeleteLevel([FromBody] Level levelId)
        {
           var status= await _QuizService.DeleteLevelAsync(levelId);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
        [HttpDelete]
        public async Task<HttpStatusCode> DeleteLevels([FromBody] List<Level> lstLevelId)
        {
            var status = await _QuizService.DeleteLevelAsync(lstLevelId);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
