using Aggregator.Entities;

using Microsoft.EntityFrameworkCore;
using ProductManagement.DTO.Filter;
using Repository.Context;

namespace Repository;

public class ProductRepository(
    ProductDbContext context) : IProductRepository
{
    private readonly ProductDbContext _context = context;

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(IFilter<Product>? filter = null)
    {
        IQueryable<Product> query = _context.Products;

        if (filter != null)
            query = filter.Apply(query);

        return await query.ToListAsync();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);

        return entity;
    }

    public Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
        return Task.CompletedTask;
    }
}