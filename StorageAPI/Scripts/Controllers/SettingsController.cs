    using Microsoft.AspNetCore.Mvc;
    using StorageAPI.Scripts.Entities;
    using StorageAPI.Scripts.Models;
    using StorageAPI.Scripts.Services;

    namespace StorageAPI.Scripts.Controllers;
    [Route(template: "api/[controller]")]
    [ApiController]
public class SettingsController
{
    private readonly SettingsService _settingsService;
    public SettingsController(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }
    [HttpGet("get")]
    public async Task<bool> SetValues()
    {
        return await _settingsService.SetValues();
    }
}

