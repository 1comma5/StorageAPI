using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]
public class ProductHistoryController : ControllerBase
{
    private readonly ProductHistoryService _productHistoryService;
    public ProductHistoryController(ProductHistoryService productHistoryService)
    {
        _productHistoryService = productHistoryService;
    }
    [HttpGet("get")]
    public async Task<ProductHistory?> Get(int id)
    {
        return await _productHistoryService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ProductHistory>> GetAll()
    {
        return await _productHistoryService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ProductHistory? productHistory)
    {
        if (productHistory == null) return Ok();
        var temp = await _productHistoryService.Add(productHistory);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ProductHistory? productHistory)
    {
        if (productHistory == null) return Ok();
        var temp = await _productHistoryService.Update(productHistory);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _productHistoryService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}