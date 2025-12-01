using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IUsersController
    {
        void Delete(int id);
        Task<ActionResult<IEnumerable<User>>> Get();
        Task<ActionResult<string>> GetById(int id);
        Task<ActionResult<User>> Login([FromBody] User user);
        Task<ActionResult<User>> NewUser([FromBody] User user);
        Task<ActionResult> UpdateUser(int id, [FromBody] User user);
    }
}