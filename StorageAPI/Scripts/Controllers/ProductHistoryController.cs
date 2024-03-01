using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;
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
    public async Task<ProductHistoryModel?> Get(int id)
    {
        return await _productHistoryService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ProductHistoryModel>> GetAll()
    {
        return await _productHistoryService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ProductHistoryModel? productHistoryModel)
    {
        if (productHistoryModel == null) return BadRequest();
        var temp = await _productHistoryService.Add(productHistoryModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ProductHistoryModel? productHistoryModel)
    {
        if (productHistoryModel == null) return BadRequest();
        var temp = await _productHistoryService.Update(productHistoryModel);
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