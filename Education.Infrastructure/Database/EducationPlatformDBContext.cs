using Education.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Database
{
    public class EducationPlatformDBContext: IdentityDbContext
    {
        public EducationPlatformDBContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly
                (
                    typeof(EducationPlatformDBContext).Assembly
                );
            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Courses> courses { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<FeedBack> feedBacks { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<StudentCourses> studentCourses { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<VideoProgress> videoProgresses { get; set; }
        public DbSet<Section> sections { get; set; }

        public DbSet<Question> questions {  get; set; }

        public DbSet<Quiz> quizzes { get; set; }

        public DbSet<UserQuiz> userQuizzes { get; set; }
    }
}
