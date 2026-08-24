using Aggregator;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context;

public class ProductDbContext(DbContextOptions<ProductDbContext> options)
    : DbContext(options)
{
    public DbSet<ProductAggregatorRoot> Products { get; set; }
}