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
        // 1. Recherche de l'utilisateur
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return null;

        // 2. Vérification du mot de passe
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid) return null;

        // 3. Récupération des rôles (Crucial pour le test HWS)
        var roles = await _userManager.GetRolesAsync(user);
        var mainRole = roles.FirstOrDefault() ?? "User";

        // 4. Génération du Token incluant le rôle
        var token = _tokenGenerator.GenerateToken(user, mainRole);

        return new AuthResponseDto(token, user.Email!, mainRole);
    }
}