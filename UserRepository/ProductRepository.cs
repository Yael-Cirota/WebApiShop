using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ShopContext _dbContext;

        public ProductRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset)
        {
            return await _dbContext.Products.ToListAsync();
        }

    }
}
