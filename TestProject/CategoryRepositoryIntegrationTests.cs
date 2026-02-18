
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Repositories;
using Entities;
using TestProject;

namespace Repositories.Tests;

    public class CategoryRepositoryIntegrationTests 
{
    private readonly DatabaseFixture _fixture;
    private readonly ShopContext _dbContext;
    private readonly CategoryRepository _categoryRepository;

    public CategoryRepositoryIntegrationTests()
    {
        _fixture = new DatabaseFixture();
        _dbContext = _fixture.Context;
        _categoryRepository = new CategoryRepository(_dbContext);
    }
    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public async Task GetCategories_ReturnsAllCategories()
    {
        // Arrange
        var category1 = new Category { /*CategoryId = 1,*/ CategoryName = "Category1" };
        var category2 = new Category { /*CategoryId = 2,*/ CategoryName = "Category2" };

        await _dbContext.Categories.AddAsync(category1);
        await _dbContext.Categories.AddAsync(category2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _categoryRepository.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCategories_ReturnsEmpty_WhenNoCategoriesExist()
    {
        // Act
        var result = await _categoryRepository.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // Ensure the result is empty
    }
}

//public class DatabaseFixture : IDisposable
//{
//    private readonly DbContextOptions<ShopContext> _options;

//    public DatabaseFixture()
//    {
//        // Set up the in-memory database
//        _options = new DbContextOptionsBuilder<ShopContext>()
//            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test run
//            .Options;
//    }

//    public ShopContext CreateDbContext()
//    {
//        return new ShopContext(_options);
//    }

//    public void Dispose()
//    {
//        // Cleanup the in-memory database
//        var context = new ShopContext(_options);
//        context.Database.EnsureDeleted();
//        context.Dispose();
//    }
//}