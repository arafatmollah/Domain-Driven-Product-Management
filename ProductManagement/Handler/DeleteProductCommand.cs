using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class DeleteProductHandler(IProductRepository productRepository)
    : ICommandHandler<DeleteProductCommandDto, bool>
{
    public async Task<bool> HandleAsync(DeleteProductCommandDto command)
    {
        var product = await productRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Product with id {command.Id} was not found.");

        await productRepository.DeleteAsync(product);

        return true;
    }
}
