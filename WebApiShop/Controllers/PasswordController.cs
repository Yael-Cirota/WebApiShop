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
        private readonly IPasswordServices _passwordServices;
        
        public PasswordController(IPasswordServices passwordServices)
        {
            _passwordServices = passwordServices;
        }

        // POST api/<PasswordController>
        [HttpPost]
        public ActionResult<Password> Post([FromBody] string password)
        {
            Password result = _passwordServices.GetStrength(password);
            if (result == null)
            {
                return BadRequest("Invalid password");
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Password> Post([FromBody] Password password)
        {
            throw new NotImplementedException();
        }
    }
}
