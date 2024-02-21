using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class StorageParametersController : ControllerBase
{
    private readonly StorageParametersService _storageParametersService;
    public StorageParametersController(StorageParametersService storageParametersService)
    {
        _storageParametersService = storageParametersService;
    }
    [HttpGet("get")]
    public async Task<StorageParameters?> Get(string id)
    {
        return await _storageParametersService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<StorageParameters>> GetAll()
    {
        return await _storageParametersService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(StorageParameters? storageParameters)
    {
        if (storageParameters == null) return Ok();
        var temp = await _storageParametersService.Add(storageParameters);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(StorageParameters? storageParameters)
    {
        if (storageParameters == null) return Ok();
        var temp = await _storageParametersService.Update(storageParameters);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _storageParametersService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}