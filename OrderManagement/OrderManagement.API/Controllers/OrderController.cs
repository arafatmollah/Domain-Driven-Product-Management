using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTO.Command;
using OrderManagement.DTO.Query;
using OrderManagement.DTO.Response;
using OrderManagement.Handler.Abstraction;

namespace OrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(
    ICommandHandler<CreateOrderCommandDto> createHandler,
    IQueryHandler<GetOrdersQuery, IEnumerable<OrderResponseDto>> getAllHandler,
    IQueryHandler<GetOrderQuery, OrderResponseDto> getByIdHandler,
    ICommandHandler<UpdateOrderCommandDto> updateHandler,
    ICommandHandler<DeleteOrderCommandDto> deleteHandler
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> Create(
        CreateOrderCommandDto request)
    {
        await createHandler.HandleAsync(request);

        var order = await getByIdHandler.HandleAsync(
            new GetOrderQuery { Id = request.Id });

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAll(
        [FromQuery] string? customerId,
        [FromQuery] OrderManagement.DTO.OrderStatus? status)
    {
        var orders = await getAllHandler.HandleAsync(
            new GetOrdersQuery
            {
                CustomerId = customerId,
                Status     = status
            });

        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderResponseDto>> GetById(int id)
    {
        var order = await getByIdHandler.HandleAsync(
            new GetOrderQuery { Id = id });

        return Ok(order);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrderResponseDto>> Update(
        int id,
        UpdateOrderCommandDto request)
    {
        request.Id = id;

        await updateHandler.HandleAsync(request);

        var order = await getByIdHandler.HandleAsync(
            new GetOrderQuery { Id = request.Id });

        return Ok(order);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await deleteHandler.HandleAsync(
            new DeleteOrderCommandDto { Id = id });

        return NoContent();
    }
}
