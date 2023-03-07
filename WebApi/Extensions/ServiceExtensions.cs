using BLL.Contracts;
using BLL.Services;
using DAL;
using DAL.Contracts;
using DAL.DatabaseInitializers;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EducationalProgramsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationalProgramsDb")));
            services.AddDbContext<WorkingProgramsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WorkingProgramsDb")));
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUniversityService, UniversityService>();
        }

        public static void RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Person, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<WorkingProgramsDbContext>();

            services.InitializeIdentity();
        }

        public static async void InitializeIdentity(this IServiceCollection services)
        {
            await using var serviceProvider = services.BuildServiceProvider();

            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<Person>>();
                var rolesManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                await IdentityInitializer.InitializeAsync(userManager, rolesManager);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}