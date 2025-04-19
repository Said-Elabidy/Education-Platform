
using Education.Application.DTO_s.CourseDto_s;
using Education.Application.DTO_s.FeedBackDTO_s;
using Education.Application.DTO_s.SectionDTO_s;
using Education.Application.DTO_s.StudentCourse;
using Education.Application.DTO_s.VideoProgressDto_s;
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
      
        services.AddScoped<ISectionRepository<SectionDto>, SectionRepository>();
      
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IQuizRepository,QuizRepository>();
      
        services.AddScoped<ICourseRepository<GetCourseDataDTO>, CourseRepository>();
        services.AddScoped<IFeedBackRepo<FeedBackDTO>, FeedBackRepository>();
        services.AddScoped<IStudentCourseRepository<StudentCourseDTO>, StudentCourseRepository>();
        services.AddScoped<IVideoProgressRepository<VideoProgressDtos>, VideoProgressRepository>();

        services.AddScoped<IUserQuizReository, UserQuizReository>();
        services.AddScoped<IVideoRepository, VideoRepository>();


    }
}
