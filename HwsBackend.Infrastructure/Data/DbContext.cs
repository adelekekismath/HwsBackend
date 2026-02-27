namespace HwsBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HwsBackend.Domain.Entities;

public class AppDbContext : IdentityDbContext<ApplicationUser> 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Guide> Guides { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Activity>()
            .HasOne(a => a.Guide)
            .WithMany(g => g.Activities)
            .HasForeignKey(a => a.GuideId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Guide>()
            .HasMany(g => g.InvitedUsers)
            .WithMany(u => u.InvitedGuides)
            .UsingEntity<Dictionary<string, object>>(
                "GuideInvitations", 
                j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Guide>().WithMany().HasForeignKey("GuideId")
            );
    }       
}