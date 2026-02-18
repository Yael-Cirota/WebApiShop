using AutoMapper;
using DTO_s;
using Entities;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Repository;
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

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _iProductRepository = productRepository;
            _iMapper = mapper;
        }

        public async Task<PageResponse<ProductDTO>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description)
        {
            skip = skip ?? 10;
            position = position ?? 1;
            List<Product> products;
            PageResponse<ProductDTO> pageResponse = new PageResponse<ProductDTO>();
            (products, pageResponse.TotalItems) = await _iProductRepository.GetProducts(name, categories, minPrice, maxPrice, (int)position, (int)skip, orderBy, description);
            pageResponse.Data = _iMapper.Map<List<Product>, List<ProductDTO>>(products);
            pageResponse.CurrentPage = (int)position;
            pageResponse.HasPreviousPage = pageResponse.CurrentPage > 1;
            pageResponse.HasNextPage = (pageResponse.TotalItems / skip) > (pageResponse.CurrentPage - 1);
            pageResponse.PageSize = (int)skip;
            return pageResponse;
        }
    }
}
