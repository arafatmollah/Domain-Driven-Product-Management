using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Response;
using ProductManagement.Handler;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
    CreateProductHandler createProductHandler) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(
        CreateProductCommandDto request)
    {

        var result = await createProductHandler.HandleAsync(request);

        return Ok(result);
    }
}