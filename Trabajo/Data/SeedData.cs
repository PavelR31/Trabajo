using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear roles
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Crear el rol
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Crear usuarios y asignar roles
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };
            await userManager.CreateAsync(adminUser, "AdminPassword123!");
        }
        await userManager.AddToRoleAsync(adminUser, "Admin");

        var regularUser = await userManager.FindByEmailAsync("user@example.com");
        if (regularUser == null)
        {
            regularUser = new IdentityUser
            {
                UserName = "user@example.com",
                Email = "user@example.com"
            };
            await userManager.CreateAsync(regularUser, "UserPassword123!");
        }
        await userManager.AddToRoleAsync(regularUser, "User");
    }
}
