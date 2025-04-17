using Education.Domain.Entities;
using Education.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace EducationPlatform.DataSeeds
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminIfNoneExistAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Check if any admin exists
            var admins = await userManager.GetUsersInRoleAsync(MyRoles.Admin);
            if (admins.Any())
            {
                Console.WriteLine("Admin already exists.");
                return;
            }

            // Ensure the Admin role exists
            if (!await roleManager.RoleExistsAsync(MyRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(MyRoles.Admin));
            }

            var adminEmail = "admin@admin.com";
            var adminPassword = "Admin@123";

            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, MyRoles.Admin);
                var user = await userManager.FindByNameAsync(adminEmail);
                if (user is not null)
                {
                    var roleAdded = await userManager.AddToRoleAsync(user, MyRoles.Admin);
                    if (roleAdded.Succeeded)
                    {
                        Console.WriteLine("Admin seeded.");
                    }
                }
               
            }
        }
    }

}
