using EventPlanner.Service;
using EventPlanner.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly AuditService _service;
        public AuditController(AuditService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuditTrail>>> Get()
        {
            var audits = await _service.GetAll();
            return Ok(audits);
        }
    }
}
