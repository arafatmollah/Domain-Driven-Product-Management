namespace Aggregator;

using ProductManagement.DTO.Command;

public class ProductAggregatorRoot
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Quantity { get; private set; }

    public DateTime ExpirationDate { get; private set; }

    public decimal Price { get; private set; }


    private static void Validate(
        string name,
        string description,
        decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (name.Length > 100)
            throw new ArgumentException(
                "Product name cannot exceed 100 characters.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(
                "Product description is required.");

        if (description.Length > 500)
            throw new ArgumentException(
                "Product description cannot exceed 500 characters.");

        if (price <= 0)
            throw new ArgumentException(
                "Product price must be greater than zero.");
    }


    public void Create(CreateProductCommandDto command)
    {
        Validate(
            command.Name,
            command.Description,
            command.Price);

        Name = command.Name;
        Description = command.Description;
        Quantity = command.Quantity;
        ExpirationDate = command.ExpirationDate;
        Price = command.Price;
    }

    public void Update(UpdateProductCommandDto command)
    {
        Validate(
            command.Name,
            command.Description,
            command.Price);

        Name = command.Name;
        Description = command.Description;
        Quantity = command.Quantity;
        ExpirationDate = command.ExpirationDate;
        Price = command.Price;
    }
}