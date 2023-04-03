using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace DAL.DatabaseInitializers
{
    public class IdentityInitializer
    {
        public const string AdminRoleName = "Admin";
        public const string LecturerRoleName = "Lecturer";
        public const string MethodistRoleName = "Methodist";

        public static async Task InitializeAsync(UserManager<Person> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Qwerty123#";
            var firstName = "Oleg";
            var lastName = "Chygryn";

            if (await roleManager.FindByNameAsync(AdminRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRoleName));
            }

            if (await roleManager.FindByNameAsync(LecturerRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(LecturerRoleName));
            }

            if (await roleManager.FindByNameAsync(MethodistRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(MethodistRoleName));
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
                    await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                    await userManager.AddToRoleAsync(adminUser, LecturerRoleName);
                    await userManager.AddToRoleAsync(adminUser, MethodistRoleName);
                }
            }
        }
    }
}