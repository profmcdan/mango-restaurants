using Mango.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.DbContexts;

public class ProductApiDbContext : DbContext
{
    public ProductApiDbContext(DbContextOptions<ProductApiDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
}