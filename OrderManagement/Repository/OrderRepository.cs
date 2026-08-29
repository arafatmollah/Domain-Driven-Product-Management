using OrderManagement.Aggregator;
using OrderManagement.DTO.Filter;
using OrderManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Repository;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    private readonly OrderDbContext _context = context;

    public async Task<OrderAggregatorRoot?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<OrderAggregatorRoot>> GetAllAsync(
        IFilter<OrderAggregatorRoot>? filter = null)
    {
        IQueryable<OrderAggregatorRoot> query = _context.Orders;

        if (filter != null)
            query = filter.Apply(query);

        return await query.ToListAsync();
    }

    public async Task<OrderAggregatorRoot> AddAsync(OrderAggregatorRoot entity)
    {
        await _context.Orders.AddAsync(entity);

        return entity;
    }

    public Task UpdateAsync(OrderAggregatorRoot entity)
    {
        _context.Orders.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(OrderAggregatorRoot entity)
    {
        _context.Orders.Remove(entity);
        return Task.CompletedTask;
    }
}
