using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]

public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpGet("get")]
    public async Task<Order?> Get(string id)
    {
        return await _orderService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Order>> GetAll()
    {
        return await _orderService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Order? order)
    {
        if (order == null) return Ok();
        var temp = await _orderService.Add(order);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Order? order)
    {
        if (order == null) return Ok();
        var temp = await _orderService.Update(order);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _orderService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}