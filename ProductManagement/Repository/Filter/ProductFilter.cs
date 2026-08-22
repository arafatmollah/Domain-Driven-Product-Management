using Aggregator.Entities;
using ProductManagement.DTO.Filter;

namespace Repository.Filter;


public class ProductFilter : IFilter<Product>
{
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public IQueryable<Product> Apply(IQueryable<Product> query)
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(x =>
                x.Name.Contains(Search) ||
                x.Description.Contains(Search));
        }

        if (MinPrice.HasValue)
            query = query.Where(x => x.Price >= MinPrice.Value);

        if (MaxPrice.HasValue)
            query = query.Where(x => x.Price <= MaxPrice.Value);

        return query;
    }
}
