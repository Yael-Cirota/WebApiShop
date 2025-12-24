using DTO_s;

namespace Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset);
    }
}