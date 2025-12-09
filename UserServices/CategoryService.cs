using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRepository;


namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _icategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _icategoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _icategoryRepository.GetCategories();
        }
    }
}
