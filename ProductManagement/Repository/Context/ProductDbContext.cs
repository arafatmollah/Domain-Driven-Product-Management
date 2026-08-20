using Microsoft.EntityFrameworkCore;
//using Repository.Models;
using Aggregator.Entities;

namespace Repository.Context;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}