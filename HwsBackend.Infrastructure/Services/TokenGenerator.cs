using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HwsBackend.Application.Interfaces;
using HwsBackend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HwsBackend.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _config;

    public TokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(ApplicationUser user, string role)
    {
        // 1. Définition des "Claims" (les informations embarquées dans le jeton)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role) // Crucial pour le [Authorize(Roles = "Admin")]
        };

        // 2. Récupération de la clé secrète depuis appsettings.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Création du token
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1), // Le token expire après 24h
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}