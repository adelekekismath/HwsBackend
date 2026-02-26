namespace HwsBackend.Application.Interfaces;

using HwsBackend.Application.DTOs;
public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto);
}