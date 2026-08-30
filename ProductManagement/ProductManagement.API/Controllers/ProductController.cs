using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ServiceBus.Handlers;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IServiceBus serviceBus) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        CreateProductCommandDto request)
    {
        await serviceBus.SendCommandAsync(request);

        var product = await serviceBus.SendQueryAsync<GetProductQuery, ProductResponseDto>(
            new GetProductQuery { Id = request.Id });

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var products = await serviceBus.SendQueryAsync<GetProductsQuery, IEnumerable<ProductResponseDto>>(
            new GetProductsQuery
            {
                Search   = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            });

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id)
    {
        var product = await serviceBus.SendQueryAsync<GetProductQuery, ProductResponseDto>(
            new GetProductQuery { Id = id });

        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponseDto>> Update(
        int id,
        UpdateProductCommandDto request)
    {
        request.Id = id;

        await serviceBus.SendCommandAsync(request);

        var product = await serviceBus.SendQueryAsync<GetProductQuery, ProductResponseDto>(
            new GetProductQuery { Id = request.Id });

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await serviceBus.SendCommandAsync(
            new DeleteProductCommandDto { Id = id });

        return NoContent();
    }
}