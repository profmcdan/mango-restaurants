using Mango.Services.ProductApi.Dtos;

namespace Mango.Services.ProductApi.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(Guid id);
    Task<ProductDto> CreateProduct(CreateProductDto productDto);
    Task<ProductDto> UpdateProduct(Guid id, CreateProductDto productDto);
    Task<bool> DeleteProduct(Guid id);
}