using Aggregator.Entities;
using ProductManagement.DTO.Command;

namespace Aggregator.Services;

public class ProductAggregator
{
    public Product Create(CreateProductCommandDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Product name is required.");

        if (request.Price <= 0)
            throw new ArgumentException(
                "Product price must be greater than zero.");

        if (request.Quantity <= 0)
            throw new ArgumentException(
                "Product quantity must be greater than zero.");

        if (request.ExpirationDate <= DateTime.UtcNow)
            throw new ArgumentException(
                "Product expiration date must be in the future.");

        return new Product
        {
            Name = request.Name,
            Description = request.Description,
            Quantity = request.Quantity,
            ExpirationDate = request.ExpirationDate,
            Price = request.Price
        };
    }
}