namespace HwsBackend.Domain.Entities;


using HwsBackend.Domain.Enums;
public class Guide {
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public int DaysCount { get; set; } 
    public List<int> MobilityIds { get; set; } = new(); 
    public List<int> SeasonIds { get; set; } = new(); 
    public List<int> TargetAudienceIds { get; set; } = new();
    
    public List<Activity> Activities { get; set; } = new(); 
    public List<string> InvitedUserIds { get; set; } = new();
    public List<ApplicationUser> InvitedUsers { get; set; } = new();
}