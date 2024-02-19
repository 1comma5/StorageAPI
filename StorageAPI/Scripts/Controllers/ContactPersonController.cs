using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]


public class ContactPersonController : ControllerBase
{
    private readonly ContactPersonService _contactPersonService;
    public ContactPersonController(ContactPersonService contactPersonService)
    {
        _contactPersonService = contactPersonService;
    }
    [HttpGet("get")]
    public async Task<ContactPerson?> Get(string id)
    {
        return await _contactPersonService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ContactPerson>> GetAll()
    {
        return await _contactPersonService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ContactPerson? contactPerson)
    {
        if (contactPerson == null) return Ok();
        var temp = await _contactPersonService.Add(contactPerson);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ContactPerson? contactPerson)
    {
        if (contactPerson == null) return Ok();
        var temp = await _contactPersonService.Update(contactPerson);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _contactPersonService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}