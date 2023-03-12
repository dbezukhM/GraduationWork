using System.Text;
using BLL.Contracts;
using BLL.Services;
using DAL;
using DAL.Contracts;
using DAL.DatabaseInitializers;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddDbContext<EducationalProgramsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationalProgramsDb")));
            services.AddDbContext<WorkingProgramsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WorkingProgramsDb")));*/
            services.AddDbContext<EducationalProgramsDbContext>(options =>
                options.UseInMemoryDatabase("EducationalProgramsDb"));
            services.AddDbContext<WorkingProgramsDbContext>(options =>
                options.UseInMemoryDatabase("WorkingProgramsDb"));
        }

        public static void RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IEpRepositoryAsync<>), typeof(EpRepositoryAsync<>));
            services.AddScoped(typeof(IWpRepositoryAsync<>), typeof(WpRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUniversityService, UniversityService>();
            services.AddScoped<ITokenGenerator>(s => new TokenGenerator(configuration.GetValue<string>("Apps:Api"),
                services.BuildServiceProvider().GetRequiredService<UserManager<Person>>()));
            services.AddScoped<IAccountService, AccountService>();
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

        public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var serverUrl = configuration.GetValue<string>("Apps:Api");
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = serverUrl,
                        ValidAudience = serverUrl,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                });
        }

        public static void RegisterSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                });
            });
        }
    }
}