using Aggregator;
using ProductManagement.DTO.Filter;

namespace Repository.Filter;

public class ProductFilter : IFilter<ProductAggregatorRoot>
{
    public string? Search { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public IQueryable<ProductAggregatorRoot> Apply(
        IQueryable<ProductAggregatorRoot> query)
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(x =>
                x.Name.Contains(Search) ||
                x.Description.Contains(Search));
        }

        if (MinPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= MinPrice.Value);
        }

        if (MaxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= MaxPrice.Value);
        }

        return query;
    }
}