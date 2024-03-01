using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]

public class StorageLocationController : ControllerBase
{
    private readonly StorageLocationService _storageLocationService;
    public StorageLocationController(StorageLocationService storageLocationService)
    {
        _storageLocationService = storageLocationService;
    }
    [HttpGet("get")]
    public async Task<StorageLocationModel?> Get(int id)
    {
        return await _storageLocationService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<StorageLocationModel>> GetAll()
    {
        return await _storageLocationService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(StorageLocationModel? storageLocationModel)
    {
        if (storageLocationModel == null) return BadRequest();
        var temp = await _storageLocationService.Add(storageLocationModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(StorageLocationModel? storageLocationModel)
    {
        if (storageLocationModel == null) return BadRequest();
        var temp = await _storageLocationService.Update(storageLocationModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _storageLocationService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}