using OrderManagement.Aggregator;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Repository.Context;

public class OrderDbContext(DbContextOptions<OrderDbContext> options)
    : DbContext(options)
{
    public DbSet<OrderAggregatorRoot> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderAggregatorRoot>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.CustomerId)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Quantity)
                  .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Status)
                  .HasConversion<int>();
        });
    }
}
