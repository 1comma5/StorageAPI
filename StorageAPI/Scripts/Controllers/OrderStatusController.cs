using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class OrderStatusController : ControllerBase
{
    private readonly OrderStatusService _orderStatusService;
    public OrderStatusController(OrderStatusService orderStatusService)
    {
        _orderStatusService = orderStatusService;
    }
    [HttpGet("get")]
    public async Task<OrderStatus?> Get(string id)
    {
        return await _orderStatusService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<OrderStatus>> GetAll()
    {
        return await _orderStatusService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(OrderStatus? orderStatus)
    {
        if (orderStatus == null) return Ok();
        var temp = await _orderStatusService.Add(orderStatus);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(OrderStatus? orderStatus)
    {
        if (orderStatus == null) return Ok();
        var temp = await _orderStatusService.Update(orderStatus);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _orderStatusService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}