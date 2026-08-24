namespace Aggregator;

using FluentValidation;
using ProductManagement.DTO.Command;

public class ProductAggregatorRoot
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Quantity { get; private set; }

    public DateTime ExpirationDate { get; private set; }

    public decimal Price { get; private set; }

    private readonly InlineValidator<ProductAggregatorRoot> _validator;

    public ProductAggregatorRoot()
    {
        _validator = new InlineValidator<ProductAggregatorRoot>();

        _validator.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        _validator.RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        _validator.RuleFor(x => x.Price)
            .GreaterThan(0);
    }

    public async Task Create(CreateProductCommandDto command)
    {
        Name = command.Name;
        Description = command.Description;
        Quantity = command.Quantity;
        ExpirationDate = command.ExpirationDate;
        Price = command.Price;

        await _validator.ValidateAndThrowAsync(this);
    }

    public async Task Update(UpdateProductCommandDto command)
    {
        Name = command.Name;
        Description = command.Description;
        Quantity = command.Quantity;
        ExpirationDate = command.ExpirationDate;
        Price = command.Price;

        await _validator.ValidateAndThrowAsync(this);
    }
}