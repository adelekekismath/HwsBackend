namespace HwsBackend.Infrastructure.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using HwsBackend.Domain.Entities;

public class AppDbContext : IdentityDbContext<ApplicationUser> 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Guide> Guides => Set<Guide>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<ReferenceOption> ReferenceOptions => Set<ReferenceOption>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var intListConverter = new ValueConverter<List<int>, string>(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
        );

        builder.Entity<Guide>(entity => 
        {
            entity.Property(e => e.SeasonIds).HasConversion(intListConverter);
            entity.Property(e => e.TargetAudienceIds).HasConversion(intListConverter);
            entity.Property(e => e.MobilityIds).HasConversion(intListConverter);

            entity.HasMany(g => g.InvitedUsers)
                  .WithMany(u => u.InvitedGuides)
                  .UsingEntity<Dictionary<string, object>>(
                      "GuideInvitations", 
                      j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                      j => j.HasOne<Guide>().WithMany().HasForeignKey("GuideId")
                  );
        });

        builder.Entity<Activity>(entity => 
        {
            entity.HasOne(a => a.Guide)
                  .WithMany(g => g.Activities)
                  .HasForeignKey(a => a.GuideId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        SeedReferenceOptions(builder);
    }

    private static void SeedReferenceOptions(ModelBuilder builder)
    {
        builder.Entity<ReferenceOption>().HasData(
            // Seasons (1-5)
            new ReferenceOption { Id = 1, Type = "Season", Label = "Printemps", Value = "Spring" },
            new ReferenceOption { Id = 2, Type = "Season", Label = "Été", Value = "Summer" },
            new ReferenceOption { Id = 3, Type = "Season", Label = "Automne", Value = "Autumn" },
            new ReferenceOption { Id = 4, Type = "Season", Label = "Hiver", Value = "Winter" },
            new ReferenceOption { Id = 5, Type = "Season", Label = "Toute l'année", Value = "AllYear" },
            
            // Mobilities (6-9)
            new ReferenceOption { Id = 6, Type = "Mobility", Label = "Voiture", Value = "Car" },
            new ReferenceOption { Id = 7, Type = "Mobility", Label = "Vélo", Value = "Bike" },
            new ReferenceOption { Id = 8, Type = "Mobility", Label = "Moto", Value = "Motorcycle" },
            new ReferenceOption { Id = 9, Type = "Mobility", Label = "À pied", Value = "Walking" },
            
            // Targets (10-13) - 
            new ReferenceOption { Id = 10, Type = "Target", Label = "Famille", Value = "Family" },
            new ReferenceOption { Id = 11, Type = "Target", Label = "Couple", Value = "Couple" },
            new ReferenceOption { Id = 12, Type = "Target", Label = "Groupe", Value = "Group" },
            new ReferenceOption { Id = 13, Type = "Target", Label = "Seul", Value = "Solo" },
            
            // Categories (14-18)
            new ReferenceOption { Id = 14, Type = "ActivityCategory", Label = "Musée", Value = "museum" },
            new ReferenceOption { Id = 15, Type = "ActivityCategory", Label = "Château", Value = "castle" },
            new ReferenceOption { Id = 16, Type = "ActivityCategory", Label = "Activité", Value = "activity" },
            new ReferenceOption { Id = 17, Type = "ActivityCategory", Label = "Parc", Value = "park" },
            new ReferenceOption { Id = 18, Type = "ActivityCategory", Label = "Grotte", Value = "cave" }
        );
    }
}