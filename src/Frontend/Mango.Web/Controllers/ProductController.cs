using Mango.Web.Models;
using Mango.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    // GET
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        List<ProductDto> products = new();
        var response = await _productService.GetAllProductsAsync<ResponseDto>();
        if (response.IsSuccess)
        {
            products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data)!)!;
        }

        return View(products);
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync<ResponseDto>(productDto);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(model: productDto);
    }
}