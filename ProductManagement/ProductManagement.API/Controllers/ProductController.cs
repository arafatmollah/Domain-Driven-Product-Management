using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler;
using ProductManagement.Handler.Abstraction;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ICommandHandler<CreateProductCommandDto, ProductResponseDto> _createHandler;
    private readonly IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>> _getAllHandler;
    private readonly IQueryHandler<GetProductQuery, ProductResponseDto> _getByIdHandler;
    private readonly ICommandHandler<UpdateProductCommandDto, ProductResponseDto> _updateHandler;
    private readonly ICommandHandler<DeleteProductCommandDto, bool> _deleteHandler;

    public ProductController(
        ICommandHandler<CreateProductCommandDto, ProductResponseDto> createHandler,
        IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>> getAllHandler,
        IQueryHandler<GetProductQuery, ProductResponseDto> getByIdHandler,
        ICommandHandler<UpdateProductCommandDto, ProductResponseDto> updateHandler,
        ICommandHandler<DeleteProductCommandDto, bool> deleteHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        CreateProductCommandDto request)
    {
        var product = await _createHandler.HandleAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var products = await _getAllHandler.HandleAsync(new GetProductsQuery
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
        var product = await _getByIdHandler.HandleAsync(
            new GetProductQuery { Id = id });

        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        int id,
        UpdateProductCommandDto request)
    {
        request.Id = id;

        var product = await _updateHandler.HandleAsync(request);

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteHandler.HandleAsync(
            new DeleteProductCommandDto { Id = id });

        return NoContent();
    }
}