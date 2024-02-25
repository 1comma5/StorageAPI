using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet("get")]
    public async Task<Employee?> Get(int id)
    {
        return await _employeeService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Employee>> GetAll()
    {
        return await _employeeService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Employee? employee)
    {
        if (employee == null) return Ok();
        var temp = await _employeeService.Add(employee);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Employee? employee)
    {
        if (employee == null) return Ok();
        var temp = await _employeeService.Update(employee);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _employeeService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}