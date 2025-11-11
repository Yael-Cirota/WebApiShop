using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserServices userServices = new UserServices();
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return userServices.GetUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string getById(int id)
        {
            return userServices.GetById();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User userResult = userServices.AddUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User userResult = userServices.FindUser(user);
            if (userResult == null)
                return NoContent();
            return user;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] User user)
        {
            userServices.UpdateUser(id, user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}