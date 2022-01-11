using Mango.Services.ProductApi.Dtos;
using Mango.Services.ProductApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductApi.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    protected ResponseDto _response;


    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        this._response = new ResponseDto();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productRepository.GetProducts();
            _response.Data = products;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return Ok(_response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var product = await _productRepository.GetProductById(id);
            _response.Data = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return Ok(_response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateProduct(productDto);
            _response.Data = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return Ok(_response);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductDto productDto)
    {
        try
        {
            var product = await _productRepository.UpdateProduct(id, productDto);
            _response.Data = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return Ok(_response);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var deleted = await _productRepository.DeleteProduct(id);
            _response.Data = deleted;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return Ok(_response);
    }
}