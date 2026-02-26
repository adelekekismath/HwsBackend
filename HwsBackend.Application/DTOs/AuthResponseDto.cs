namespace HwsBackend.Application.DTOs;

public record AuthResponseDto(string Token, string Email, string Role);