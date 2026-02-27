namespace HwsBackend.Domain.Entities;

public class ReferenceOption
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; 
    public string Label { get; set; } = string.Empty; 
    public string Value { get; set; } = string.Empty;
}