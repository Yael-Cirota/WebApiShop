using DTO_s;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<PageResponse<ProductDTO>> Get(string? name, [FromQuery] int[]? categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description)
        {
            _logger.LogInformation("Get products request");
            return await _productService.GetProducts(name, categories, minPrice, maxPrice, position, skip, orderBy, description);
        }
    }
}
