namespace HwsBackend.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Infrastructure.Data;
using HwsBackend.Domain.Entities;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly AppDbContext _context;
    public ConfigController(AppDbContext context) => _context = context;

    [HttpGet("guide-options")]
    public async Task<IActionResult> GetOptions()
    {
        // On récupère tout en une fois pour éviter de multiplier les appels DB
        var allOptions = await _context.ReferenceOptions
            .Where(o => o.Type != "ActivityCategory")
            .Select(o => new { o.Id, o.Label, o.Type })
            .ToListAsync();

        var response = new
        {
            seasons = allOptions.Where(o => o.Type == "Season").Select(o => new { o.Id, o.Label }),
            mobilities = allOptions.Where(o => o.Type == "Mobility").Select(o => new { o.Id, o.Label }),
            targets = allOptions.Where(o => o.Type == "Target").Select(o => new { o.Id, o.Label })
        };

        return Ok(response);
    }

    [HttpGet("activity-categories")]
    public async Task<IActionResult> GetActivityCategories()
    {
        var categories = await _context.ReferenceOptions
            .Where(o => o.Type == "ActivityCategory")
            .Select(o => new { o.Id, o.Label }) 
            .ToListAsync();
            
        return Ok(categories);
    }
}