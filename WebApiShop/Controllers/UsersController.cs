using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Service;
using DTO_s;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IPasswordServices _passwordServices;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserServices userServices, IPasswordServices passwordServices, ILogger<UsersController> logger)
        {
            _userServices = userServices;
            _passwordServices = passwordServices;
            _logger = logger;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            UserDTO user = await _userServices.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> NewUser([FromBody] UserDTO user, string password)
        {
            Password password1 = _passwordServices.GetStrength(password);
            if (password1.Strength < 2)
                return BadRequest($"Password too weak (score: {password1.Strength}/4). Minimum required: 2");
            UserDTO userResult = await _userServices.AddUser(user, password);
            if (userResult == null)
            {
                return BadRequest("The Password is not Strength Enough");
            }
            return CreatedAtAction(nameof(GetById), new { id = userResult.Id }, userResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginUser user)
        {
<<<<<<< HEAD
            User userResult = await _userService.FindUser(user);
            if (userResult == null)
                return Unauthorized("Invalid email or password");
=======
            UserDTO userResult = await _userServices.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            _logger.LogInformation($"Login attempted with Email {user.Email} and password {user.Password}");
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938
            return Ok(userResult);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDTO user, string password)
        {
<<<<<<< HEAD
            Password password = _passwordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest($"Password too weak (score: {password.Strength}/4). Minimum required: 2");
            await _userServices.UpdateUser(id, user);
=======
            bool res = await _userServices.UpdateUser(id, user, password);
            if(!res)
                return BadRequest("Password too weak");
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}