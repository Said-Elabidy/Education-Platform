using Education.Application.Services.CategoryServices;
using Education.Application.Services.QuestionServices;
using Education.Application.Services.QuizServices;
using Education.Application.Services.SectionServices;
﻿using Education.Application.CourseServices;
using Education.Application.Implementations;
using Education.Application.Implementations.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
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
