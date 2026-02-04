using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_s;
using AutoMapper;


namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _iCategoryRepository;
        private readonly IMapper _iMapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _iCategoryRepository = categoryRepository;
            _iMapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<Category> c = await _iCategoryRepository.GetCategories();
            IEnumerable<CategoryDTO> categoryDTOs = _iMapper.Map< IEnumerable<Category>, IEnumerable<CategoryDTO>>(c);
            return categoryDTOs;
        }
    }
}
