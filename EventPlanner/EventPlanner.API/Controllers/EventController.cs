using EventPlanner.API.Contracts;
using EventPlanner.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _service;

        public EventController(EventService service)
        {
            _service = service;
        }

        // GET: api/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetAll()
        {
            try
            {
                IEnumerable<EventResponse> result = await _service.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/event
        [HttpPost]
        public async Task<ActionResult> AddEvent([FromBody] EventRequest request)
        {
            try
            {
                await _service.AddEvent(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/event/{id}
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                await _service.DeleteEvent(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
