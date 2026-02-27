namespace HwsBackend.Api.Controllers;
using HwsBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Infrastructure.Data;


[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly AppDbContext _context;
    public ConfigController(AppDbContext context) => _context = context;

    [HttpGet("options")]
    public async Task<IActionResult> GetOptions()
    {
        var allOptions = await _context.ReferenceOptions.ToListAsync();

        var response = new
        {
            Seasons = allOptions.Where(o => o.Type == "Season").Select(o => new { o.Id, o.Label }),
            Mobilities = allOptions.Where(o => o.Type == "Mobility").Select(o => new { o.Id, o.Label }),
            Targets = allOptions.Where(o => o.Type == "Target").Select(o => new { o.Id, o.Label })
        };

        return Ok(response);
    }
}