using AutoMapper;
using Mango.Services.ProductApi.DbContexts;
using Mango.Services.ProductApi.Dtos;
using Mango.Services.ProductApi.Helpers;
using Mango.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductApiDbContext _db;
    private readonly IMapper _mapper;
    private readonly IAWSS3Service _awsS3;

    public ProductRepository(ProductApiDbContext db, IMapper mapper, IAWSS3Service awsS3)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _awsS3 = awsS3;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await _db.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (product is null)
            throw new NullReferenceException($"Product with id {id} not found.");
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto productDto)
    {
        var productImage = productDto.Image;
        if (productImage is not null)
        {
            var imageUrl = await _awsS3.UploadFileAsync(productImage);
            productDto.ImageUrl = imageUrl;
        }
        var product = _mapper.Map<Product>(productDto);
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateProduct(Guid id, CreateProductDto productDto)
    {
        var productImage = productDto.Image;
        if (productImage is not null)
        {
            var imageUrl = await _awsS3.UploadFileAsync(productImage);
            productDto.ImageUrl = imageUrl;
        }
        
        var product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (product is null)
            throw new NullReferenceException($"Product with id {id} not found.");
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.CategoryName = productDto.CategoryName;
        product.ImageUrl = productDto.ImageUrl;
        product.UpdatedAt = DateTime.Now;
        _db.Products.Update(_mapper.Map<Product>(product));
        await _db.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (product is null)
            throw new NullReferenceException($"Product with id {id} not found.");
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }
}