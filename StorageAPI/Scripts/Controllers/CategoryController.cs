using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class CategoryController(CategoryService categoryService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<Category?> Get(string id)
    {
        return await categoryService.Get(id);
    }

    [HttpGet("get-all")]
    public async Task<List<Category>> GetAll()
    {
        return await categoryService.GetAll();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post(Category? category)
    {
        if (category == null) return Ok();
        var temp = await categoryService.Add(category);
        if (temp == null) return BadRequest();

        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(Category? category)
    {
        if (category == null) return Ok();
        var temp = await categoryService.Update(category);
        if (temp == null) return BadRequest();

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await categoryService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}