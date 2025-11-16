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
        IUserServices _iUserServices;
        IPasswordServices _iPasswordServices;

        public UsersController(IUserServices userServices, IPasswordServices passwordServices)
        {
            _iUserServices = userServices;
            _iPasswordServices = passwordServices;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _iUserServices.GetUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string getById(int id)
        {
            return _iUserServices.GetById();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> newUser([FromBody] User user)
        {
            User userResult = _iUserServices.AddUser(user);
            Password password = _iPasswordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest("Password is too weak");
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User userResult = _iUserServices.FindUser(user);
            if (userResult == null)
                return NoContent();
            return user;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User user)
        {
            Password password = _iUserServices.UpdateUser(id, user);
            if (password.Strength < 2)
                return BadRequest();
            return Ok(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}