namespace HwsBackend.Application.DTOs;

public class ActivityCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? OpeningHours { get; set; }
    public string? Website { get; set; }

    public int DayNumber { get; set; } 
    public int ExecutionOrder { get; set; } 

    public int GuideId { get; set; }
}