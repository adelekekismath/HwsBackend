using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Infrastructure.Data;
using HwsBackend.Domain.Entities;
using HwsBackend.Domain.Constants;
using HwsBackend.Application.DTOs;

namespace HwsBackend.Api.Controllers;

[Authorize(Roles = Roles.Admin)] 
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context) => _context = context;

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create([FromBody] ActivityCreateDto dto)
    {

        var guideExists = await _context.Guides.AnyAsync(g => g.Id == dto.GuideId);
        if (!guideExists) return NotFound(new { message = "Le guide spécifié n'existe pas." });

        var activity = new Activity
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            OpeningHours = dto.OpeningHours,
            Website = dto.Website,
            DayNumber = dto.DayNumber,
            ExecutionOrder = dto.ExecutionOrder,
            GuideId = dto.GuideId
        };

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