
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Education.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace Education.Infrastructure.Extentions;

public static class ServiceCollectionExtensions 
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var CONNECTION_STRING = configuration.GetConnectionString("cs")
                ?? throw new InvalidOperationException("no connection string found");

        services.AddDbContext<EducationPlatformDBContext>(optionBuilder =>
        {
            optionBuilder.UseSqlServer(CONNECTION_STRING);
        });
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EducationPlatformDBContext>()
                 .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>();


        // no need to inject genric repos there's no dependency for them

        services.AddScoped<IQuestionRepository, QuestionRepository>();
      
       services.AddScoped<ISectionRepository, SectionRepository>();
      
       services.AddScoped<ICategoryRepository, CategoryRepository>();

       services.AddScoped<IQuizRepository,QuizRepository>();
      
       services.AddScoped<ICourseRepository, CourseRepository>();

        using(var scope = services.CreateScope())
{
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await AdminSeeder.SeedAdminIfNoneExistAsync(userManager, roleManager);
        }

    }
}
