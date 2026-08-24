using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;
using Repository.Context;

namespace ProductManagement.Handler;

public class DeleteProductHandler(
    IProductRepository productRepository,
    ProductDbContext dbContext)
    : ICommandHandler<DeleteProductCommandDto>
{
    public async Task HandleAsync(DeleteProductCommandDto command)
    {
        var product = await productRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Product with id {command.Id} was not found.");

        await productRepository.DeleteAsync(product);

        await dbContext.SaveChangesAsync();
    }
}
