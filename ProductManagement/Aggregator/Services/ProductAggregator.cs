using Aggregator.Entities;
using Aggregator.Validators;
using FluentValidation;
using ProductManagement.DTO.Command;

namespace Aggregator.Services;

public class ProductAggregator(
    IValidator<Product> validator)
{
    public async Task<Product> Create(
        CreateProductCommandDto command)
    {
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price
        };

        await validator.ValidateAndThrowAsync(product);

        return product;
    }

    public async Task Update(
        Product product,
        UpdateProductCommandDto command)
    {
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;

        await validator.ValidateAndThrowAsync(product);
    }
}