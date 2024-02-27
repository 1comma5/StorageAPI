using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

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
    public async Task<ProductModel?> Get(int id)
    {
        return await _productService.Get(id);
    }

    [HttpGet("get-all")]
    public async Task<List<ProductModel>> GetAll()
    {
        return await _productService.GetAll();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post(ProductModel? productModel)
    {
        if (productModel == null) return Ok();
        var temp = await _productService.Add(productModel);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(ProductModel? product)
    {
        if (product == null) return Ok();
        var temp = await _productService.Update(product);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _productService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}