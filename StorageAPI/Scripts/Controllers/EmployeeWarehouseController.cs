using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;
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
    public async Task<EmployeeWarehouseModel?> Get(int id)
    {
        return await _employeeWarehouseService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<EmployeeWarehouseModel>> GetAll()
    {
        return await _employeeWarehouseService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(EmployeeWarehouseModel? employeeWarehouseModel)
    {
        if (employeeWarehouseModel == null) return BadRequest();
        var temp = await _employeeWarehouseService.Add(employeeWarehouseModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(EmployeeWarehouseModel? employeeWarehouseModel)
    {
        if (employeeWarehouseModel == null) return Ok();
        var temp = await _employeeWarehouseService.Update(employeeWarehouseModel);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _employeeWarehouseService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
    }