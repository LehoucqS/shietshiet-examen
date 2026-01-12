using EventPlanner.API.Contracts;
using EventPlanner.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }

        // GET: api/location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetAll()
        {
            try
            {

                return Ok(await _service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/location/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationResponse>> Get(int id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Location
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LocationPostRequest request)
        {
            try
            {
                await _service.AddLocation(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
