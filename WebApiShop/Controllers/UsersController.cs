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
    public class UsersController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            IEnumerable<User> users = await _userServices.GetUsers();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            User user = await _userServices.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> NewUser([FromBody] User user)
        {
            Password password = _passwordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest($"Password too weak (score: {password.Strength}/4). Minimum required: 2");
            User userResult = await _userServices.AddUser(user);
            if (userResult == null)
            {
                return BadRequest("The Password is not Strength Enough");
            }
            return CreatedAtAction(nameof(Get), new { id = userResult.Id }, userResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User userResult = await _userServices.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            return Ok(userResult);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            Password password = _passwordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest($"Password too weak (score: {password.Strength}/4). Minimum required: 2");
            _userServices.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}