using Mango.Web.Models;

namespace Mango.Web.Services.Contracts;

public interface IProductService
{
    Task<T> GetAllProductsAsync<T>();
    Task<T> GetProductByIdAsync<T>(Guid id);
    Task<T> CreateProductAsync<T>(CreateProductDto productDto);
    Task<T> UpdateProductAsync<T>(Guid id, CreateProductDto productDto);
    Task<T> DeleteProductAsync<T>(Guid id);
}