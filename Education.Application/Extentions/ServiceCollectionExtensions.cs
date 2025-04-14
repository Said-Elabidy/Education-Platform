using Education.Application.Services.QuestionServices;
using Education.Application.Services.SectionServices;
using Education.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ISectionServices, SectionServices>();

    }
}
