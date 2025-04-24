using Education.Application.Services.CategoryServices;
using Education.Application.Services.QuestionServices;
using Education.Application.Services.QuizServices;
using Education.Application.Services.SectionServices;
using Education.Application.Implementations;
using Education.Application.Implementations.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Education.Application.Services.FeedBackServices;
using Education.Application.Services.StudentCourseServices;

using Education.Application.Services.UserQuizServices;
using Education.Application.Services.VideoServices;
using Education.Application.Services.Storage_Services;
using Education.Application.Services.FeedBackServices;
using Education.Application.Services.StudentCourseServices;

using Education.Application.Services.UserQuizServices;
using Education.Application.Services.VideoProgressServices;
using Education.Application.Services.CourseServices;
using Microsoft.Extensions.Configuration;
using Education.Application.helpers;
using Education.Application.Services.JwtServices;
using Education.Application.Services.PaymentServices;


namespace Education.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
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


        services.AddScoped<IVideoService, VideoService>();

        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IFeedBackServices, FeedBackService>();
        services.AddScoped<IStudetCourseServices, StudentCourseServices>();
        services.AddScoped<IVideoProgressServices, VideoProgressServices>();

        services.AddScoped<IUserQuizServices, UserQuizServices>();
        services.AddScoped<IPaymentServices, PaymentServices>();


    }
}
