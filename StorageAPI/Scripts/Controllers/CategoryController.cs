using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("get")]
    public async Task<Category?> Get(string id)
    {
        return await _categoryService.Get(id);
    }

    [HttpGet("get-all")]
    public async Task<List<Category>> GetAll()
    {
        return await _categoryService.GetAll();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post(Category? category)
    {
        if (category == null) return Ok();
        var temp = await _categoryService.Add(category);
        if (temp == null) return BadRequest();

        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(Category? category)
    {
        if (category == null) return Ok();
        var temp = await _categoryService.Update(category);
        if (temp == null) return BadRequest();

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _categoryService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}