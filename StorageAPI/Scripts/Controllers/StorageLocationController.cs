using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

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
    public async Task<StorageLocation?> Get(string id)
    {
        return await _storageLocationService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<StorageLocation>> GetAll()
    {
        return await _storageLocationService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(StorageLocation? storageLocation)
    {
        if (storageLocation == null) return Ok();
        var temp = await _storageLocationService.Add(storageLocation);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(StorageLocation? storageLocation)
    {
        if (storageLocation == null) return Ok();
        var temp = await _storageLocationService.Update(storageLocation);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _storageLocationService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}