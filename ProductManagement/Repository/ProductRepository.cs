using Aggregator;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DTO.Filter;
using Repository.Context;

namespace Repository;

public class ProductRepository(
    ProductDbContext context) : IProductRepository
{
    private readonly ProductDbContext _context = context;

    public async Task<ProductAggregatorRoot?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ProductAggregatorRoot>> GetAllAsync(IFilter<ProductAggregatorRoot>? filter = null)
    {
        IQueryable<ProductAggregatorRoot> query = _context.Products;

        if (filter != null)
            query = filter.Apply(query);

        return await query.ToListAsync();
    }

    public async Task<ProductAggregatorRoot> AddAsync(ProductAggregatorRoot entity)
    {
        await _context.Products.AddAsync(entity);

        return entity;
    }

    public Task UpdateAsync(ProductAggregatorRoot entity)
    {
        _context.Products.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProductAggregatorRoot entity)
    {
        _context.Products.Remove(entity);
        return Task.CompletedTask;
    }
}