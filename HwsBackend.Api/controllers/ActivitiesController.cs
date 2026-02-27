using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Infrastructure.Data;
using HwsBackend.Domain.Entities;
using HwsBackend.Domain.Constants;

namespace HwsBackend.Api.Controllers;

[Authorize(Roles = Roles.Admin)] 
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Activity activity)
    {
        var guideExists = await _context.Guides.AnyAsync(g => g.Id == activity.GuideId);
        if (!guideExists) return NotFound("Guide non trouvé");

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        return Ok(activity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Activity activity)
    {
        if (id != activity.Id) return BadRequest();

        _context.Entry(activity).State = EntityState.Modified;
        
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!ActivityExists(id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null) return NotFound();

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ActivityExists(int id) => _context.Activities.Any(e => e.Id == id);
}