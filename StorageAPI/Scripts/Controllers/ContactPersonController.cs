using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]

public class ContactPersonController

{
    private readonly ContactPersonController _contactPersonController;
    public ContactPersonController(ContactPersonController contactPersonController)
    {
        _contactPersonController = contactPersonController;
    }
    [HttpGet("get")]
    public async Task<ContactPerson?> Get(string id)
    {
        return await _contactPersonController.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<ContactPerson>> GetAll()
    {
        return await _contactPersonController.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(ContactPerson? contactPerson)
    {
        if (contactPerson == null) return Ok();
        var temp = await _contactPersonController.Add(contactPerson);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(ContactPerson? contactPerson)
    {
        if (contactPerson == null) return Ok();
        var temp = await _contactPersonController.Update(contactPerson);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _contactPersonController.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}