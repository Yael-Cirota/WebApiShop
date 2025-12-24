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

        public UsersController(IUserServices userServices, IPasswordServices passwordServices)
        {
            _userServices = userServices;
            _passwordServices = passwordServices;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            IEnumerable<UserDTO> users = await _userServices.GetUsers();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
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
            int strength = _passwordServices.GetStrength(password);
            Password password1 = {password, strength};
            if (password.Strength < 2)
                return BadRequest($"Password too weak (score: {password.Strength}/4). Minimum required: 2");
            UserDTO userResult = await _userServices.AddUser(user);
            if (userResult == null)
            {
                return BadRequest("The Password is not Strength Enough");
            }
            return CreatedAtAction(nameof(Get), new { id = userResult.Id }, userResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO user)
        {
            UserDTO userResult = await _userServices.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            return Ok(userResult);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDTO user)
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