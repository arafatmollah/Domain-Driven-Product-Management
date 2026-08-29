namespace OrderManagement.DTO.Filter;

public interface IFilter<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}
