using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class UnitOfMeasureController : ControllerBase
{
    private readonly UnitOfMeasureService _unitOfMeasureService;
    public UnitOfMeasureController(UnitOfMeasureService unitOfMeasureService)
    {
        _unitOfMeasureService = unitOfMeasureService;
    }
    
    [HttpGet("get")]
    public async Task<UnitOfMeasure?> Get(int id)
    {
        return await _unitOfMeasureService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<UnitOfMeasure>> GetAll()
    {
        return await _unitOfMeasureService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(UnitOfMeasure? unitOfMeasure)
    {
        if (unitOfMeasure == null) return Ok();
        var temp = await _unitOfMeasureService.Add(unitOfMeasure);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(UnitOfMeasure? unitOfMeasure)
    {
        if (unitOfMeasure == null) return Ok();
        var temp = await _unitOfMeasureService.Update(unitOfMeasure);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _unitOfMeasureService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
    
}