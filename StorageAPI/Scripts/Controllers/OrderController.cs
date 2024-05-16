using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

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
    public async Task<OrderModel?> Get(int id)
    {
        return await _orderService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<OrderModel>> GetAll()
    {
        return await _orderService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(OrderModel? order)
    {
        if (order == null) return BadRequest();
        var temp = await _orderService.Add(order);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(OrderModel? order)
    {
        if (order == null) return BadRequest();
        var temp = await _orderService.Update(order);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _orderService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }

    [HttpPost("update_status")]
    public async Task<IActionResult> UpdateStatus(int id, int statusId)
    {
        var temp = await _orderService.UpdateStatus(id, statusId);
        if (!temp) return BadRequest();
        return Ok();
    }
}