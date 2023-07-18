using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGTS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ExcerciseInSessionController : ControllerBase
    {
        // GET: api/<ExcerciseInSessionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExcerciseInSessionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExcerciseInSessionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExcerciseInSessionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExcerciseInSessionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
