using DTO_s;
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
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            _logger.LogInformation("Get categories request");
            return await _categoryService.GetCategories();
        }
    }
}
