using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class DeleteProductHandler(IUnitOfWork unitofwork)
    : IHandler<DeleteProductCommandDto>
{
    public async Task HandleAsync(DeleteProductCommandDto command)
    {
        var product = await unitofwork.Products.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Product with id {command.Id} was not found.");

        await unitofwork.Products.DeleteAsync(product);

        await unitofwork.SaveChangesAsync();
    }
}
