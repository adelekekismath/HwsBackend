using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Infrastructure.Data;
using HwsBackend.Domain.Entities;
using HwsBackend.Domain.Constants;
using HwsBackend.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace HwsBackend.Api.Controllers;

[Authorize] 
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var activities = await _context.Activities.Include(a => a.Guide).ToListAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var activity = await _context.Activities.Include(a => a.Guide).FirstOrDefaultAsync(a => a.Id == id);
        if (activity == null) return NotFound();

        if (!User.IsInRole(Roles.Admin))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isInvited = await _context.Guides.AnyAsync(g => g.Id == activity.GuideId && g.InvitedUsers.Any(u => u.Id == userId));
            if (!isInvited) return Forbid();
        }

        return Ok(activity);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)] 
    public async Task<IActionResult> Create([FromBody] ActivityCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var guideExists = await _context.Guides.AnyAsync(g => g.Id == dto.GuideId);
        if (!guideExists) return NotFound(new { message = "Le guide spécifié n'existe pas." });

        var activity = new Activity
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId, 
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

        return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)] 
    public async Task<IActionResult> Update(int id, [FromBody] ActivityUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var activity = await _context.Activities.FindAsync(id);
        if (activity == null) return NotFound();

        activity.Title = dto.Title;
        activity.Description = dto.Description;
        activity.CategoryId = dto.CategoryId;
        activity.Address = dto.Address;
        activity.PhoneNumber = dto.PhoneNumber;
        activity.OpeningHours = dto.OpeningHours;
        activity.Website = dto.Website;
        activity.DayNumber = dto.DayNumber;
        activity.ExecutionOrder = dto.ExecutionOrder;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)] 
    public async Task<IActionResult> Delete(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null) return NotFound();

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}