using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Education.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Infrastructure.Extentions;

public static class ServiceCollectionExtensions 
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EducationPlatformDBContext>(optionBulder =>
        {
            optionBulder.UseSqlServer(configuration.GetConnectionString("cs"));
        });


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        services.AddScoped<IQuestionRepository, QuestionRepository>();
      
       services.AddScoped<ISectionRepository, SectionRepository>();
      
       services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IQuizRepository,QuizRepository>();

    }
}
