using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class StorageLocationProductController : ControllerBase
{
    private readonly StorageLocationProductService _storageLocationProductService;

    public StorageLocationProductController(StorageLocationProductService storageLocationProductControllerService)
    {
        _storageLocationProductService = storageLocationProductControllerService;
    }
    [HttpGet("get")]
    public async Task<StorageLocationProductModel?> Get(int id)
    {
        return await _storageLocationProductService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<StorageLocationProductModel>> GetAll()
    {
        return await _storageLocationProductService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(StorageLocationProductModel? storageLocationProductModel)
    {
        if (storageLocationProductModel == null) return BadRequest();
        var temp = await _storageLocationProductService.Add(storageLocationProductModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(StorageLocationProductModel? storageLocationProductModel)
    {
        if (storageLocationProductModel == null) return BadRequest();
        var temp = await _storageLocationProductService.Update(storageLocationProductModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _storageLocationProductService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}