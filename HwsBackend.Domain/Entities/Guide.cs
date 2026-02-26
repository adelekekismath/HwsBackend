namespace HwsBackend.Domain.Entities;


using HwsBackend.Domain.Enums;
public class Guide {
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public int DaysCount { get; set; } 
    public MobilityType Mobility { get; set; } 
    public Season Season { get; set; } 
    public TargetAudience Target { get; set; } 
    
    public List<Activity> Activities { get; set; } = new(); 
    public List<string> InvitedUserIds { get; set; } = new();
    public List<ApplicationUser> InvitedUsers { get; set; } = new();
}