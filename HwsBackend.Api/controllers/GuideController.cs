using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HwsBackend.Infrastructure.Data;
using HwsBackend.Domain.Entities;
using HwsBackend.Domain.Constants;

namespace HwsBackend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GuidesController : ControllerBase
{
    private readonly AppDbContext _context;

    public GuidesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        IQueryable<Guide> query = _context.Guides
            .Include(g => g.Activities.OrderBy(a => a.DayNumber).ThenBy(a => a.ExecutionOrder));

        if (!User.IsInRole(Roles.Admin))
        {
            query = query.Where(g => g.InvitedUsers.Any(u => u.Id == userId));
        }

        var guides = await query.ToListAsync();
        return Ok(guides);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create([FromBody] Guide guide)
    {
        _context.Guides.Add(guide);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = guide.Id }, guide);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var guide = await _context.Guides
            .Include(g => g.Activities)
            .Include(g => g.InvitedUsers)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (guide == null) return NotFound();
        if (!User.IsInRole(Roles.Admin) && !guide.InvitedUsers.Any(u => u.Id == userId))
        {
            return Forbid();
        }

        return Ok(guide);
    }

    [HttpPost("{id}/invite")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> InviteUser(int id, [FromBody] string userEmail)
    {
        var guide = await _context.Guides.Include(g => g.InvitedUsers).FirstOrDefaultAsync(g => g.Id == id);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (guide == null || user == null) return NotFound("Guide ou Utilisateur introuvable");

        if (!guide.InvitedUsers.Contains(user))
        {
            guide.InvitedUsers.Add(user);
            await _context.SaveChangesAsync();
        }

        return Ok(new { message = $"L'utilisateur {userEmail} a été invité au guide." });
    }
}