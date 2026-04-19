using DTO_s;

namespace Service
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductById(int id);
        Task<PageResponse<ProductDTO>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description);
    }
}