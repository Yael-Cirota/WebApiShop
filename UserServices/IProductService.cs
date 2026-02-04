using DTO_s;

namespace Service
{
    public interface IProductService
    {
        Task<PageResponse<ProductDTO>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description);
    }
}