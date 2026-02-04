using Entities;

namespace Repository

{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}