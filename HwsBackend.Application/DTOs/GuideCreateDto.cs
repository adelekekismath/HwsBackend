namespace HwsBackend.Application.DTOs;

using HwsBackend.Domain.Enums;

public class GuideCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DaysCount { get; set; }
    public int MobilityType { get; set; } 
    public int Season { get; set; }
    public int TargetAudience { get; set; }
}