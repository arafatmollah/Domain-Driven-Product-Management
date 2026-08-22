using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class DeleteProductHandler(IUnitOfWork uow)
    : ICommandHandler<DeleteProductCommandDto, bool>
{
    public async Task<bool> HandleAsync(DeleteProductCommandDto command)
    {
        var product = await uow.Products.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Product with id {command.Id} was not found.");

        await uow.Products.DeleteAsync(product);

        await uow.SaveChangesAsync();

        return true;
    }
}
