using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly CreateProductHandler _createHandler;
    private readonly GetProductsHandler _getAllHandler;
    private readonly GetProductByIdHandler _getByIdHandler;
    private readonly UpdateProductHandler _updateHandler;
    private readonly DeleteProductHandler _deleteHandler;

    public ProductController(
        CreateProductHandler createHandler,
        GetProductsHandler getAllHandler,
        GetProductByIdHandler getByIdHandler,
        UpdateProductHandler updateHandler,
        DeleteProductHandler deleteHandler)
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
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _getAllHandler.HandleAsync(
            new GetProductsQuery());

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