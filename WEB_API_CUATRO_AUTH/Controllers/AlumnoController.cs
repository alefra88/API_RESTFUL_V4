using Microsoft.AspNetCore.Mvc;
using WEB_API_CUATRO_AUTH.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_CUATRO_AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _service;
        public AlumnoController(IAlumnoService service)
        {
            _service = service;
        }

        // GET: api/<AlumnoController>
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            var listAlumnos = await _service.GetAsync();
            return Ok(listAlumnos);
        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
