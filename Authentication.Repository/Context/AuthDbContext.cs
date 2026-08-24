using Authentication.Aggregator;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository.Context;

public class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : DbContext(options)
{
    public DbSet<UserAggregatorRoot> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAggregatorRoot>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.RefreshToken)
                .HasMaxLength(512);
        });
    }
}
