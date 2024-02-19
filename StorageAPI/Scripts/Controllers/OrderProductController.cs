using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]

public class OrderProductController : ControllerBase
{
    private readonly OrderProductService _orderProductService;
    public OrderProductController(OrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }
    [HttpGet("get")]
    public async Task<OrderProduct?> Get(string id)
    {
        return await _orderProductService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<OrderProduct>> GetAll()
    {
        return await _orderProductService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(OrderProduct? orderProduct)
    {
        if (orderProduct == null) return Ok();
        var temp = await _orderProductService.Add(orderProduct);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(OrderProduct? orderProduct)
    {
        if (orderProduct == null) return Ok();
        var temp = await _orderProductService.Update(orderProduct);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _orderProductService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}