
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Repositories;
using Entities;

namespace Repositories.Tests;

    public class CategoryRepositoryTests
{
    private CategoryRepository _categoryRepository;
    private readonly DbContextOptions<ShopContext> _options;

    public CategoryRepositoryTests()
    {
        // Set up in-memory database options
        _options = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Create a new ShopContext using the in-memory database
        using (var context = new ShopContext(_options))
        {
            // Seed data for testing
            context.Categories.AddRange(new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Category1" },
                new Category { CategoryId = 2, CategoryName = "Category2" }
            });
            context.SaveChanges();
        }

        // Initialize the repository with the in-memory context
        _categoryRepository = new CategoryRepository(new ShopContext(_options));
    }

    [Fact]
    public async Task GetCategories_ReturnsAllCategories()
    {
        // Act
        var result = await _categoryRepository.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Ensure two categories are returned
    }

    [Fact]
    public async Task GetCategories_ReturnsEmpty_WhenNoCategoriesExist()
    {
        // Arrange: Create a new in-memory database without seeding any categories
        using (var context = new ShopContext(_options))
        {
            context.Database.EnsureDeleted(); // Clean database
        }

        // Initialize the repository with the empty context
        _categoryRepository = new CategoryRepository(new ShopContext(_options));

        // Act
        var result = await _categoryRepository.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // Ensure the result is empty
    }
}