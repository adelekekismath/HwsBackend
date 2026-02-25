namespace HwsBackend.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using HwsBackend.Domain.Entities;
public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminEmail = "admin@hwstrip.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser 
            { 
                UserName = adminEmail, 
                Email = adminEmail,
                EmailConfirmed = true 
            };
            await userManager.CreateAsync(admin, "HwsAdmin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}