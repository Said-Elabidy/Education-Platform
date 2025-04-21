using Education.Application.Services.CategoryServices;
using Education.Application.Services.QuestionServices;
using Education.Application.Services.QuizServices;
using Education.Application.Services.SectionServices;
﻿using Education.Application.CourseServices;
using Education.Application.Implementations;
using Education.Application.Implementations.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Education.Application.helpers;
using Microsoft.Extensions.Configuration;
using Education.Application.Services.JwtServices;

namespace Education.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services , IConfiguration configuration)
    {

        services.Configure<JWT>(configuration.GetSection("JWT"));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ICategoryServices, CategoryService>();

        services.AddScoped<ISectionServices, SectionServices>();

        services.AddScoped<IQuizService, QuizService>();
      
        services.AddScoped<IImageService, ImageService>();
		    services.AddHttpContextAccessor();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUriService, UriService>();
    }
}
