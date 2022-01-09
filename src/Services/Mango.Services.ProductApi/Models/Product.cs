using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductApi.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
    [Range(1,1000)]
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}