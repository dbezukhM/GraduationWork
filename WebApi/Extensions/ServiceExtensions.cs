using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EducationalProgramsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationalProgramsDb")));
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        }
    }
}