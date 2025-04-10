using System;
using Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourses>
    {
        public void Configure(EntityTypeBuilder<StudentCourses> builder)
        {
            builder.HasKey(sc => new { sc.UserId, sc.CoursesId });
            builder.HasOne(sc => sc.ApplicationUser)
                .WithMany(u => u.StudentCourses)
                .HasForeignKey(sc => sc.UserId);
            builder.HasOne(sc => sc.Course).WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CoursesId);

        }
    }
    
}
