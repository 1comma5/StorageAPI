using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;


namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]

public class ManufacturerController : ControllerBase
{
    private readonly ManufacturerService _manufacturerService;
    public ManufacturerController(ManufacturerService manufacturerService)
    {
        _manufacturerService = manufacturerService;
    }
    [HttpGet("get")]
    public async Task<Manufacturer?> Get(string id)
    {
        return await _manufacturerService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Manufacturer>> GetAll()
    {
        return await _manufacturerService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Manufacturer? manufacturer)
    {
        if (manufacturer == null) return Ok();
        var temp = await _manufacturerService.Add(manufacturer);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Manufacturer? manufacturer)
    {
        if (manufacturer == null) return Ok();
        var temp = await _manufacturerService.Update(manufacturer);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _manufacturerService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}