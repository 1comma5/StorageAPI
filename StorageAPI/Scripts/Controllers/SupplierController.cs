using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class SupplierController : ControllerBase
{
    private readonly SupplierService _supplierService;
    public SupplierController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }
    [HttpGet("get")]
    public async Task<Supplier?> Get(int id)
    {
        return await _supplierService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Supplier>> GetAll()
    {
        return await _supplierService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Supplier? supplier)
    {
        if (supplier == null) return Ok();
        var temp = await _supplierService.Add(supplier);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Supplier? supplier)
    {
        if (supplier == null) return Ok();
        var temp = await _supplierService.Update(supplier);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _supplierService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
    
}