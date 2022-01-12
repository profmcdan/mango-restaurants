namespace Mango.Web.Models;

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
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}