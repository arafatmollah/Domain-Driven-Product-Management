using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
    CreateProductHandler createProductHandler,
    GetProductsHandler getProductsHandler,
    GetProductByIdHandler getProductByIdHandler,
    UpdateProductHandler updateProductHandler,
    DeleteProductHandler deleteProductHandler) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        [FromBody] CreateProductCommandDto request)
    {
        var result = await createProductHandler.HandleAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var result = await getProductsHandler.HandleAsync(new GetProductsQuery());
        return Ok(result);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id)
    {
        var result = await getProductByIdHandler.HandleAsync(new GetProductQuery { Id = id });
        return Ok(result);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        int id, [FromBody] UpdateProductCommandDto request)
    {
        request.Id = id;
        var result = await updateProductHandler.HandleAsync(request);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await deleteProductHandler.HandleAsync(new DeleteProductCommandDto { Id = id });
        return NoContent();
    }
}