using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.Controllers;

[Route(template: "api/[controller]")]
[ApiController]


public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;
    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    [HttpGet("get")]
    public async Task<Client?> Get(string id)
    {
        return await _clientService.Get(id);
    }
    [HttpGet("get-all")]
    public async Task<List<Client>> GetAll()
    {
        return await _clientService.GetAll();
    }
    [HttpPost("add")]
    public async Task<IActionResult> Post(Client? client)
    {
        if (client == null) return Ok();
        var temp = await _clientService.Add(client);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Put(Client? client)
    {
        if (client == null) return Ok();
        var temp = await _clientService.Update(client);
        if (temp == null) return BadRequest();
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var temp = await _clientService.Delete(id);
        if (!temp) return BadRequest();
        return Ok();
    }
}