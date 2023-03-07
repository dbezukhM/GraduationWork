using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace DAL.DatabaseInitializers
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<Person> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Qwerty123#";
            var firstName = "Oleg";
            var lastName = "Chygryn";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("admin"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("user"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var adminUser = new Person
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    IsAdmin = true,
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admin");
                    await userManager.AddToRoleAsync(adminUser, "user");
                }
            }
        }
    }
}