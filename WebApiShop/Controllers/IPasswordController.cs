using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IPasswordController
    {
        ActionResult<Password> Post([FromBody] Password password);
    }
}