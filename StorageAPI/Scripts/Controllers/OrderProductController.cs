using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

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
    public async Task<OrderProductModel?> Get(int id)
    {
        return await _orderProductService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<OrderProductModel>> GetAll()
    {
        return await _orderProductService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(OrderProductModel? orderProductModel)
    {
        if (orderProductModel == null) return BadRequest();
        var temp = await _orderProductService.Add(orderProductModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(OrderProductModel? orderProductModel)
    {
        if (orderProductModel == null) return BadRequest();
        var temp = await _orderProductService.Update(orderProductModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _orderProductService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}