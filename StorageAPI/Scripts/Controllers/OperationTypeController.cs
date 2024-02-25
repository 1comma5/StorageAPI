using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Services;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;
[Route(template: "api/[controller]")]
[ApiController]


public class OperationTypeController : ControllerBase
{
    private readonly OperationTypeService _operationTypeService;

    public OperationTypeController(OperationTypeService operationTypeService)
    {
        _operationTypeService = operationTypeService;
    }

    [HttpGet("get")]
    public async Task<OperationType?> Get(int id)
    {
        return await _operationTypeService.Get(id);
    }

    [HttpGet("get-all")]
    public async Task<List<OperationType>> GetAll()
    {
        return await _operationTypeService.GetAll();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post(OperationType? operationType)
    {
        if (operationType == null) return Ok();
        var temp = await _operationTypeService.Add(operationType);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(OperationType? operationType)
    {
        if (operationType == null) return Ok();
        var temp = await _operationTypeService.Update(operationType);
        if (temp == null) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _operationTypeService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}
    
    