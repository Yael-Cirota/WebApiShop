using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _iProductRepository;

        public ProductService(IProductRepository productRepository)
        {
            _iProductRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset)
        {
            return await _iProductRepository.GetProducts(name, categories, nimPrice, maxPrice, limit, orderBy, offset);
        }
    }
}
