using AutoMapper;
using DTO_s;
using Entities;
using Microsoft.Azure.Documents;
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
        private readonly IMapper _iMapper;

        public ProductService(IProductRepository productRepository)
        {
            _iProductRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? limit, string? orderBy, int? offset)
        {
            IEnumerable<Product> products = await _iProductRepository.GetProducts(name, categories, minPrice, maxPrice, limit, orderBy, offset);
            IEnumerable<ProductDTO> productsDTO = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return productsDTO;
        }
    }
}
