using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IUsersController
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string getById(int id);
        ActionResult<User> Login([FromBody] User user);
        ActionResult<User> newUser([FromBody] User user);
        ActionResult UpdateUser(int id, [FromBody] User user);
    }
}