using Education.Infrastructure.Database;
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



    }
}
