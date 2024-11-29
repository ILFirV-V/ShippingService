using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShippingService.Email.Domain.Entities.Identity;

namespace ShippingService.Email.Persistence.Configs;

public static class DataInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var adminEmail = "admin@example.com";
        var adminPassword = "AdminPassword123!";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true 
            };

            await userManager.CreateAsync(adminUser, adminPassword);
        }
    }
}