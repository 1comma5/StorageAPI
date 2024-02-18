using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template:"api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet ("get")] 
    public async Task<Category> GetCategory([FromQuery] string id)
    {
        return await _categoryService.GetCategory(id);
    }
}