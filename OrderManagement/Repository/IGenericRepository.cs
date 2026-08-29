using OrderManagement.DTO.Filter;

namespace OrderManagement.Repository;

public interface IGenericRepository<T>
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync(IFilter<T>? filter = null);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
