using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class WarehouseController : ControllerBase
{
    private readonly WarehouseService _warehouseService;
    public WarehouseController(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    [HttpGet("get")]
    public async Task<Warehouse?> Get(int id)
    {
        return await _warehouseService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Warehouse>> GetAll()
    {
        return await _warehouseService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Warehouse? warehouse)
    {
        if (warehouse == null) return BadRequest();
        var temp = await _warehouseService.Add(warehouse);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Warehouse? warehouse)
    {
        if (warehouse == null) return BadRequest();
        var temp = await _warehouseService.Update(warehouse);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _warehouseService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
    
}