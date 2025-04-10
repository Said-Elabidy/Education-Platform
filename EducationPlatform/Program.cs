
using Education.Application.Extentions;
using Education.Infrastructure.Database;
using Education.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //Inject DataBase Connection string
            builder.Services.AddDbContext<EducationPlatformDBContext>(optionBulder =>
            {
                optionBulder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Invoke the static methods of Application Registeration and Infrastructure Registeration

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
             

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
