using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
    ICommandHandler<CreateProductCommandDto> createHandler,
    IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>> getAllHandler,
    IQueryHandler<GetProductQuery, ProductResponseDto> getByIdHandler,
    ICommandHandler<UpdateProductCommandDto> updateHandler,
    ICommandHandler<DeleteProductCommandDto> deleteHandler
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        CreateProductCommandDto request)
    {
        await createHandler.HandleAsync(request);

        var product = await getByIdHandler.HandleAsync(
            new GetProductQuery { Id = request.Id });

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var products = await getAllHandler.HandleAsync(
            new GetProductsQuery
            {
                Search = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            });

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id)
    {
        var product = await getByIdHandler.HandleAsync(
            new GetProductQuery { Id = id });

        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        int id,
        UpdateProductCommandDto request)
    {
        request.Id = id;

        await updateHandler.HandleAsync(request);

        var product = await getByIdHandler.HandleAsync(
            new GetProductQuery { Id = request.Id });

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await deleteHandler.HandleAsync(
            new DeleteProductCommandDto { Id = id });

        return NoContent();
    }
}