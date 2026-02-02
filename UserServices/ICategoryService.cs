using DTO_s;
using Entities;

namespace Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
    }
}