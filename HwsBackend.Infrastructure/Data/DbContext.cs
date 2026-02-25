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
            .HasForeignKey(a => a.GuideId);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.InvitedGuides)
            .WithMany(g => g.InvitedUsers)
            .UsingEntity(j => j.ToTable("GuideInvitations"));
    }
}