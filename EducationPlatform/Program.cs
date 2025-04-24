
using Education.Application.Extentions;
using Education.Domain.Entities;
using Education.Infrastructure.Extentions;
using EducationPlatform.DataSeeds;
using EducationPlatform.Extentions;
using Microsoft.AspNetCore.Identity;

namespace EducationPlatform
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         

            // Add services to the container.

            //builder.Services.AddControllers();
            //Inject DataBase Connection string
            //builder.Services.AddDbContext<EducationPlatformDBContext>(optionBulder =>
            //{
            //    optionBulder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            // Invoke the static methods of Application Registeration and Infrastructure Registeration

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddPresentation(builder.Configuration);
             

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

               await AdminSeeder.SeedAdminIfNoneExistAsync(userManager, roleManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowCustom");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
