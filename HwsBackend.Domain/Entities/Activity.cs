namespace HwsBackend.Domain.Entities;
using HwsBackend.Domain.Enums;
public class Activity {
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public int CategoryId { get; set; } 
    public string Address { get; set; } 
    public string PhoneNumber { get; set; } 
    public string OpeningHours { get; set; } 
    public string Website { get; set; } 


    public int DayNumber { get; set; } 
    public int ExecutionOrder { get; set; } 

    public int GuideId { get; set; }
    public Guide Guide { get; set; }
}