using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<(List<Product> Items, int TotalCount)> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int position, int skip, string? orderBy, string? description);
    }
}