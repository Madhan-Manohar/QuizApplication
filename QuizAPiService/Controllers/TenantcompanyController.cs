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
    public class TenantcompanyController : ControllerBase
    {
        private readonly ITenantcompanyService _QuizService;
        public TenantcompanyController(ITenantcompanyService QuizService)
        {
            _QuizService = QuizService;
        }

        [HttpGet]
        public async Task<IEnumerable<Tenantcompany>> Get()
        {
            return await _QuizService.GetTCsAsync();
        }
        [HttpGet("{id}")]
        public async Task<Tenantcompany> Get([FromRoute] int id)
        {
            return await _QuizService.GetTCByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> post([FromBody] Tenantcompany value)
        {
            await _QuizService.InsertTCAsync(value);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> put([FromBody] Tenantcompany value)
        {
            await _QuizService.UpdateTCAsync(value);
            return Ok();
        }
        [HttpDelete]
        public async Task<HttpStatusCode> Delete([FromBody] Tenantcompany value)
        {
            var status = await _QuizService.DeleteTCsAsync(value);
            if (!status)
            {
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.OK;
        }
    }
}
