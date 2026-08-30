using ProductManagement.DTO.Command;
using Repository;
using Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler;

public class DeleteProductHandler(
    IProductRepository productRepository,
    ProductDbContext dbContext)
    : SharedSubsystem.Abstraction.Handlers.ICommandHandler<DeleteProductCommandDto>
{
    public async Task HandleAsync(
        DeleteProductCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {command.Id} was not found.");

        await productRepository.DeleteAsync(product);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
