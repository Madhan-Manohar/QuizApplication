using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAPiService.Entities;
using QuizAPiService.Middleware;
using QuizAPiService.Service.Abstract;
using System.Net;

namespace QuizAPiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantmasterController : ControllerBase
    {
        private readonly ITenantmasterService _QuizService;
        public TenantmasterController(ITenantmasterService QuizService)
        {
            _QuizService = QuizService;
        }
        [HttpGet]
        public async Task<IEnumerable<Tenantmaster>> Get()
        {
            return await _QuizService.GetTMsAsync();
        }
        [HttpGet("{id}")]
        public async Task<Tenantmaster> Get([FromRoute] int id)
        {
            return await _QuizService.GetTMByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> post([FromBody] Tenantmaster value)
        {
            await _QuizService.InsertTMAsync(value);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> put([FromBody] Tenantmaster value)
        {
            await _QuizService.UpdateTMAsync(value);
            return Ok();
        }
        [HttpDelete]
        public async Task<HttpStatusCode> Delete([FromBody] Tenantmaster value)
        {
            var status = await _QuizService.DeleteTMsAsync(value);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
