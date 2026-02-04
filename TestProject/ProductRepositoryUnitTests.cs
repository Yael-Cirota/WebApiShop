using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Repositories.Tests;

    public class ProductRepositoryUnitTests
{
    private ShopContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ShopContext(options);

        context.Categories.AddRange(
            new Category { CategoryId = 1, CategoryName = "Cat1" },
            new Category { CategoryId = 2, CategoryName = "Cat2" }
        );

        context.Products.AddRange(
            new Product { ProductId = 1, ProductName = "Apple", Price = 10, CategoryId = 1, Description = "Red" },
            new Product { ProductId = 2, ProductName = "Banana", Price = 5, CategoryId = 1, Description = "Yellow" },
            new Product { ProductId = 3, ProductName = "Orange", Price = 8, CategoryId = 2, Description = "Orange" }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetProducts_NoFilters_ReturnsAll()
    {
        using var context = CreateContext();
        var repo = new ProductRepository(context);

        var result = await repo.GetProducts(
            null, null, null, null,
            position: 1, skip: 10,
            orderBy: null, description: null
        );

        Assert.Equal(3, result.TotalCount);
        Assert.Equal(3, result.Items.Count);
    }

    [Fact]
    public async Task GetProducts_FilterByName_ReturnsMatching()
    {
        using var context = CreateContext();
        var repo = new ProductRepository(context);

        var result = await repo.GetProducts(
            name: "App",
            categories: null,
            minPrice: null,
            maxPrice: null,
            position: 1,
            skip: 10,
            orderBy: null,
            description: null
        );

        Assert.Single(result.Items);
        Assert.Equal("Apple", result.Items.First().ProductName);
    }

    [Fact]
    public async Task GetProducts_FilterByCategory_ReturnsCorrectItems()
    {
        using var context = CreateContext();
        var repo = new ProductRepository(context);

        var result = await repo.GetProducts(
            name: null,
            categories: new[] { 1 },
            minPrice: null,
            maxPrice: null,
            position: 1,
            skip: 10,
            orderBy: null,
            description: null
        );

        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Items, p => Assert.Equal(1, p.CategoryId));
    }

    [Fact]
    public async Task GetProducts_OrderByPriceAsc_Works()
    {
        using var context = CreateContext();
        var repo = new ProductRepository(context);

        var result = await repo.GetProducts(
            null, null, null, null,
            position: 1, skip: 10,
            orderBy: "price_asc",
            description: null
        );

        var prices = result.Items.Select(p => p.Price).ToList();
        Assert.Equal(prices.OrderBy(p => p), prices);
    }

    [Fact]
    public async Task GetProducts_Pagination_Works()
    {
        using var context = CreateContext();
        var repo = new ProductRepository(context);

        var result = await repo.GetProducts(
            null, null, null, null,
            position: 2, skip: 1,
            orderBy: "price_asc",
            description: null
        );

        Assert.Single(result.Items);
        Assert.Equal(3, result.TotalCount);
    }
}
