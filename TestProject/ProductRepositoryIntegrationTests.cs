//using Entities;
//using Repositories;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;
//using TestProject;

//namespace Repositories.Tests
//{
//    public class ProductRepositoryIntegrationTests
//        : IClassFixture<DatabaseFixture>, IDisposable
//    {
//        private readonly DatabaseFixture _fixture;
//        private readonly ProductRepository _repository;

//        public ProductRepositoryIntegrationTests(DatabaseFixture fixture)
//        {
//            _fixture = fixture;
//            _repository = new ProductRepository(_fixture.Context);

//            SeedDatabase();
//        }

//        private void SeedDatabase()
//        {
//            _fixture.Context.Products.RemoveRange(_fixture.Context.Products);
//            _fixture.Context.Categories.RemoveRange(_fixture.Context.Categories);
//            _fixture.Context.SaveChanges();

//            var category1 = new Category { CategoryId = 1, CategoryName = "Cat1" };
//            var category2 = new Category { CategoryId = 2, CategoryName = "Cat2" };

//            _fixture.Context.Categories.AddRange(category1, category2);

//            _fixture.Context.Products.AddRange(
//                new Product
//                {
//                    ProductName = "Apple",
//                    Price = 10,
//                    CategoryId = 1,
//                    Description = "Red apple"
//                },
//                new Product
//                {
//                    ProductName = "Banana",
//                    Price = 5,
//                    CategoryId = 1,
//                    Description = "Yellow banana"
//                },
//                new Product
//                {
//                    ProductName = "Orange",
//                    Price = 8,
//                    CategoryId = 2,
//                    Description = "Orange fruit"
//                }
//            );

//            _fixture.Context.SaveChanges();
//        }

//        [Fact]
//        public async Task GetProducts_NoFilters_ReturnsAllProducts()
//        {
//            var result = await _repository.GetProducts(
//                null, null, null, null,
//                position: 1,
//                skip: 10,
//                orderBy: null,
//                description: null
//            );

//            Assert.Equal(3, result.TotalCount);
//            Assert.Equal(3, result.Items.Count);
//        }

//        [Fact]
//        public async Task GetProducts_FilterByName_ReturnsCorrectProduct()
//        {
//            var result = await _repository.GetProducts(
//                name: "App",
//                categories: null,
//                minPrice: null,
//                maxPrice: null,
//                position: 1,
//                skip: 10,
//                orderBy: null,
//                description: null
//            );

//            Assert.Single(result.Items);
//            Assert.Equal("Apple", result.Items.First().ProductName);
//        }

//        [Fact]
//        public async Task GetProducts_FilterByCategory_ReturnsOnlyCategoryProducts()
//        {
//            var result = await _repository.GetProducts(
//                name: null,
//                categories: new[] { 1 },
//                minPrice: null,
//                maxPrice: null,
//                position: 1,
//                skip: 10,
//                orderBy: null,
//                description: null
//            );

//            Assert.Equal(2, result.TotalCount);
//            Assert.All(result.Items, p => Assert.Equal(1, p.CategoryId));
//        }

//        [Fact]
//        public async Task GetProducts_OrderByPriceDesc_WorksCorrectly()
//        {
//            var result = await _repository.GetProducts(
//                null, null, null, null,
//                position: 1,
//                skip: 10,
//                orderBy: "price_desc",
//                description: null
//            );

//            var prices = result.Items.Select(p => p.Price).ToList();
//            Assert.Equal(prices.OrderByDescending(p => p), prices);
//        }

//        [Fact]
//        public async Task GetProducts_Pagination_ReturnsCorrectPage()
//        {
//            var result = await _repository.GetProducts(
//                null, null, null, null,
//                position: 2,
//                skip: 1,
//                orderBy: "price_asc",
//                description: null
//            );

//            Assert.Single(result.Items);
//            Assert.Equal(3, result.TotalCount);
//        }

//        public void Dispose()
//        {
//            // Tear-down after EACH test
//            _fixture.Context.Products.RemoveRange(_fixture.Context.Products);
//            _fixture.Context.Categories.RemoveRange(_fixture.Context.Categories);
//            _fixture.Context.SaveChanges();
//        }
//    }
//}




using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace Repositories.Tests
{
    public class ProductRepositoryIntegrationTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly ShopContext _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTests()
        {
            _fixture = new DatabaseFixture();
            _dbContext = _fixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }
        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetProducts_WhenProductsExist_ReturnsAllProductsWithCategory()
        {
            // Arrange
            var category = new Category { CategoryName = "Electronics" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var testProducts = new List<Product>
            {
                new Product { ProductName = "Laptop", CategoryId = category.CategoryId, Price = 3500 },
                new Product { ProductName = "Mouse", CategoryId = category.CategoryId, Price = 150 }
            };

            await _dbContext.Products.AddRangeAsync(testProducts);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProducts(
                position: 1,
                skip: 10,
                name: null,
                description: null,
                categories: null,
                minPrice: null,
                maxPrice: null,
                orderBy: null
            );

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count());
            Assert.All(result.Items, p => Assert.NotNull(p.Category));
            Assert.Contains(result.Items, p => p.ProductName == "Laptop" && p.Category.CategoryName == "Electronics");
        }

        [Fact]
        public async Task GetProducts_WhenNoProductsExist_ReturnsEmptyList()
        {
            // Act
            var result = await _productRepository.GetProducts(
                position: 1,
                skip: 10,
                name: null,
                description: null,
                categories: null,
                minPrice: null,
                maxPrice: null,
                orderBy: null
            );

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
        }
    }
}