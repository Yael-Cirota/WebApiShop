using Entities;

namespace Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset);
    }
}