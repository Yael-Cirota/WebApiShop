using DTO_s;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _iProductService;

        public ProductsController(IProductService productService)
        {
            _iProductService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<PageResponse<ProductDTO>> Get(string? name, [FromQuery] int[]? categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description)
        {
            return await _iProductService.GetProducts(name, categories, minPrice, maxPrice, position, skip, orderBy, description);
        }
    }
}
