using Microsoft.AspNetCore.Identity;

namespace HwsBackend.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Guide> InvitedGuides { get; set; } = new List<Guide>();

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}