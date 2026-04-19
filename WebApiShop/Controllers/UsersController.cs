using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using DTO_s;

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
        public async Task<ActionResult<UserDTO>> NewUser([FromBody] PostUserDTO user)
        {
            Password password = _passwordServices.GetStrength(user.Password);
            if (password.Strength < 2)
                return BadRequest($"Password too weak (score: {password.Strength}/4). Minimum required: 2");
            UserDTO userResult = await _userServices.AddUser(user);
            if (userResult == null)
            {
                return BadRequest("The Password is not Strength Enough");
            }
            return CreatedAtAction(nameof(GetById), new { id = userResult.Id }, userResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginUser user)
        {
            UserDTO userResult = await _userServices.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            _logger.LogInformation($"Login attempted with Email {user.Email} and password {user.Password}");
            return Ok(userResult);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromBody]PostUserDTO user)
        {
            bool res = await _userServices.UpdateUser(user);
            if(!res)
                return BadRequest("Password too weak");
            return Ok();
        }
    }
}