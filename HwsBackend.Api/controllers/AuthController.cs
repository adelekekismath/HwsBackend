namespace HwsBackend.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using HwsBackend.Application.Interfaces;
using HwsBackend.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous] 
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.LoginAsync(loginDto);

        if (result == null)
            return Unauthorized(new { message = "Email ou mot de passe incorrect." });

        return Ok(result);
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (success, message) = await _authService.RegisterAsync(registerDto);

        if (!success)
            return BadRequest(new { message });

        return Ok(new { message = "Utilisateur créé avec succès." });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMe()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);
        
        return Ok(new { Email = email, Role = role });
    }
}