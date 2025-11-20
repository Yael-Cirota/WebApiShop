using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase, IUsersController
    {
        private readonly IUserServices _userServices;
        private readonly IPasswordServices _passwordServices;

        public UsersController(IUserServices userServices, IPasswordServices passwordServices)
        {
            _userServices = userServices;
            _passwordServices = passwordServices;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            IEnumerable<string> users = _userServices.GetUsers();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            string user = _userServices.GetById();
            if (string.IsNullOrEmpty(user))
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> NewUser([FromBody] User user)
        {
            Password password = _passwordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest("Password is not strong enough");
            User userResult = _userServices.AddUser(user);
            return CreatedAtAction(nameof(Get), new { id = userResult.Id }, userResult);
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User userResult = _userServices.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            return Ok(userResult);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User user)
        {
            Password password = _userServices.UpdateUser(id, user);
            if (password.Strength < 2)
                return BadRequest("Password is not strong enough");
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}