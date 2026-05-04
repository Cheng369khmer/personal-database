using Microsoft.AspNetCore.Identity;
using MyPortfolio.Models;

namespace MyPortfolio.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Create roles
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Create default Admin
        var adminEmail = "admin@myportfolio.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            // ក្នុង DbSeeder.cs ត្រង់វគ្គ Admin
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                // រកមើលបន្ទាត់នេះក្នុង DbSeeder.cs
                FullName = "Sokhom Thacheng", // 
                // Role = "Admin", <--- លុបបន្ទាត់នេះចេញ ឬ Comment វាចោល
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin@123456");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Create default User
        var userEmail = "user@myportfolio.com";
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                FullName = "Guest User",
                Role = "User",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "User@123456");
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}
