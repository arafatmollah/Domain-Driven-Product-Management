using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTO.Command;
using OrderManagement.DTO.Query;
using OrderManagement.DTO.Response;
using ServiceBus.Handlers;

namespace OrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(IServiceBus serviceBus) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> Create(
        CreateOrderCommandDto request)
    {
        await serviceBus.SendCommandAsync(request);

        var order = await serviceBus.SendQueryAsync<GetOrderQuery, OrderResponseDto>(
            new GetOrderQuery { Id = request.Id });

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAll(
        [FromQuery] string? customerId,
        [FromQuery] OrderManagement.DTO.OrderStatus? status)
    {
        var orders = await serviceBus.SendQueryAsync<GetOrdersQuery, IEnumerable<OrderResponseDto>>(
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
        var order = await serviceBus.SendQueryAsync<GetOrderQuery, OrderResponseDto>(
            new GetOrderQuery { Id = id });

        return Ok(order);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrderResponseDto>> Update(
        int id,
        UpdateOrderCommandDto request)
    {
        request.Id = id;

        await serviceBus.SendCommandAsync(request);

        var order = await serviceBus.SendQueryAsync<GetOrderQuery, OrderResponseDto>(
            new GetOrderQuery { Id = request.Id });

        return Ok(order);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await serviceBus.SendCommandAsync(
            new DeleteOrderCommandDto { Id = id });

        return NoContent();
    }
}
