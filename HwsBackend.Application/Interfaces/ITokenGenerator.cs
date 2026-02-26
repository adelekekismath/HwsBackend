namespace HwsBackend.Application.Interfaces;

using HwsBackend.Domain.Entities;

public interface ITokenGenerator
{
    string GenerateToken(ApplicationUser user, string role);
}