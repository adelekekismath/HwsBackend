using Microsoft.AspNetCore.Identity;
using HwsBackend.Application.DTOs;
using HwsBackend.Application.Interfaces;
using HwsBackend.Domain.Entities;

namespace HwsBackend.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return null;

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid) return null;

        var roles = await _userManager.GetRolesAsync(user);
        var mainRole = roles.FirstOrDefault() ?? "User";

        var token = _tokenGenerator.GenerateToken(user, mainRole);

        return new AuthResponseDto(token, user.Email!, mainRole);
    }

    public async Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto)
{
    var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
    if (userExists != null) return (false, "Cet email est déjà utilisé.");

    var user = new ApplicationUser 
    { 
        UserName = registerDto.Email, 
        Email = registerDto.Email,
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName
    };

    var result = await _userManager.CreateAsync(user, registerDto.Password);
    
    if (result.Succeeded)
    {
        await _userManager.AddToRoleAsync(user, registerDto.Role);
        return (true, string.Empty);
    }

    return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
}
}