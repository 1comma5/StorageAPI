using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers; 
[Route(template: "api/[controller]")]
[ApiController]

public class EmployeeWarehouseController : ControllerBase
{
    private readonly EmployeeWarehouseService _employeeWarehouseService;
    public EmployeeWarehouseController(EmployeeWarehouseService employeeWarehouseService)
    {
        _employeeWarehouseService = employeeWarehouseService;
    }
    [HttpGet("get")]
    public async Task<EmployeeWarehouse?> Get(string id)
    {
        return await _employeeWarehouseService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<EmployeeWarehouse>> GetAll()
    {
        return await _employeeWarehouseService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(EmployeeWarehouse? employeeWarehouse)
    {
        if (employeeWarehouse == null) return Ok();
        var temp = await _employeeWarehouseService.Add(employeeWarehouse);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(EmployeeWarehouse? employeeWarehouse)
    {
        if (employeeWarehouse == null) return Ok();
        var temp = await _employeeWarehouseService.Update(employeeWarehouse);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _employeeWarehouseService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
    }