using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        PasswordServices passwordServices = new PasswordServices();

        // GET: api/<PasswordController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PasswordController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<PasswordController>
        [HttpPost]
        public ActionResult<Password> Post([FromBody]Password password)
        {
            Password password1 = passwordServices.GetStrength(password);
            if (password1 == null)
            {
                return NoContent();
            }
            return Ok(password1);
        }

        // PUT api/<PasswordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<PasswordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
