using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductApi.Dtos;

public record ProductDto
{
    public Guid Id { get; set; } 
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
}

public record CreateProductDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
    [Range(1,1000, ErrorMessage = "Price can only be between 1 and 1000")]
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}