using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IPasswordController
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string Get(string id);
        ActionResult<Password> Post([FromBody] Password password);
        void Put(int id, [FromBody] string value);
    }
}