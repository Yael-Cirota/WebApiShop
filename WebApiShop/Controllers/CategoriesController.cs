using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _iCategoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _iCategoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _iCategoryService.GetCategories();
        }
    }
}
