using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ShopContext _dbContext;

        public CategoryRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
