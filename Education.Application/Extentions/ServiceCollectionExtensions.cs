using Education.Application.CourseServices;
using Education.Application.Implementations;
using Education.Application.Implementations.Abstracts;
using Education.Application.QuestionServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IImageService, ImageService>();
		services.AddHttpContextAccessor();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUriService, UriService>();
    }
}
