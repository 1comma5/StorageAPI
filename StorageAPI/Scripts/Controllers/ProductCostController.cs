using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;
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
    public async Task<ProductCostModel?> Get(int id)
    {
        return await _productCostService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ProductCostModel>> GetAll()
    {
        return await _productCostService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ProductCostModel? productCostModel)
    {
        if (productCostModel == null) return Ok();
        var temp = await _productCostService.Add(productCostModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ProductCostModel? productCostModel)
    {
        if (productCostModel == null) return Ok();
        var temp = await _productCostService.Update(productCostModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _productCostService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}