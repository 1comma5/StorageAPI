using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class ProductCostController : ControllerBase
{ 
    private readonly ProductCostService _productCostService;
    public ProductCostController(ProductCostService productCostService)
    {
        _productCostService = productCostService;
    }
    [HttpGet("get")]
    public async Task<ProductCost?> Get(string id)
    {
        return await _productCostService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ProductCost>> GetAll()
    {
        return await _productCostService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ProductCost? productCost)
    {
        if (productCost == null) return Ok();
        var temp = await _productCostService.Add(productCost);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ProductCost? productCost)
    {
        if (productCost == null) return Ok();
        var temp = await _productCostService.Update(productCost);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _productCostService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}