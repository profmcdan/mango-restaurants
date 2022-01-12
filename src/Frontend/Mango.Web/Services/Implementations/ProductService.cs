using Mango.Web.Models;
using Mango.Web.Services.Contracts;

namespace Mango.Web.Services.Implementations;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    // public ResponseDto ResponseModel { get; set; }

    public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        // this.ResponseModel = new ResponseDto();
    }


    public async Task<T> GetAllProductsAsync<T>()
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = Sd.ApiType.GET,
            Url = $"{Sd.ProductApiBase}/products",
            AccessToken = ""
        });
    }

    public async Task<T> GetProductByIdAsync<T>(Guid id)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = Sd.ApiType.GET,
            Url = $"{Sd.ProductApiBase}/products/{id}",
            AccessToken = ""
        });
    }

    public async Task<T> CreateProductAsync<T>(CreateProductDto productDto)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = Sd.ApiType.POST,
            Data = productDto,
            Url = $"{Sd.ProductApiBase}/products",
            AccessToken = ""
        });
    }

    public async Task<T> UpdateProductAsync<T>(Guid id, CreateProductDto productDto)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = Sd.ApiType.PUT,
            Data = productDto,
            Url = $"{Sd.ProductApiBase}/products/{id}",
            AccessToken = ""
        });
    }

    public async Task<T> DeleteProductAsync<T>(Guid id)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = Sd.ApiType.DELETE,
            Url = $"{Sd.ProductApiBase}/products/{id}",
            AccessToken = ""
        });
    }
}