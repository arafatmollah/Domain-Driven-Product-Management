using Aggregator.Services;
using AutoMapper;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;

public class UpdateProductHandler(
    IUnitOfWork unitofwork,
    ProductAggregator productAggregator,
    IMapper mapper)
    : ICommandHandler<UpdateProductCommandDto, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(
        UpdateProductCommandDto command)
    {
        var product = await unitofwork.Products.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {command.Id} was not found.");

        await productAggregator.Update(product, command);

        await unitofwork.Products.UpdateAsync(product);

        await unitofwork.SaveChangesAsync();

        return mapper.Map<ProductResponseDto>(product);
    }
}