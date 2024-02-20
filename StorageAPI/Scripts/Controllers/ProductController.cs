using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("get")]
    public async Task<Product?> Get(string id)
    {
        return await _productService.Get(id);
    }

    [HttpGet("get-all")]
    public async Task<List<Product>> GetAll()
    {
        return await _productService.GetAll();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post(Product? product)
    {
        if (product == null) return Ok();
        var temp = await _productService.Add(product);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(Product? product)
    {
        if (product == null) return Ok();
        var temp = await _productService.Update(product);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _productService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}