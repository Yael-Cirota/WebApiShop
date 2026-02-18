using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        ShopContext _dbContext;

        public ProductRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<(List<Product> Items, int TotalCount)> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int position, int skip, string? orderBy, string? description)
        {
            var query = _dbContext.Products.Where(product =>
                (description == null ? (true) : product.Description.Contains(description))
                && (minPrice == null ? (true) : product.Price >= minPrice)
                && (maxPrice == null ? (true) : product.Price <= maxPrice)
                && (name == null ? (true) : product.ProductName.Contains(name))
                && (categories == null || categories.Length==0 ? (true) : categories.Contains(product.CategoryId)));
            if(orderBy != null)
            {
                switch (orderBy.ToLower())
                {
                    case "price_asc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    default:
                        query = query.OrderBy(p => p.ProductName);
                        break;
                }
            }
            else
                query = query.OrderBy(p => p.ProductName);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip((position - 1) * skip).Take(skip)
                .Include(p => p.Category).ToListAsync();
            var total = await query.CountAsync();
            return (products, total);
        }

    }
}
