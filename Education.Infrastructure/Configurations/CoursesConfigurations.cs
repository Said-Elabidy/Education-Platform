using Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Configurations
{
    public class CoursesConfigurations : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(c => c.CoursesId);
            builder.Property(c => c.CoursesId).ValueGeneratedOnAdd();
            builder.HasOne(c => c.Categories).WithMany(c => c.courses).HasForeignKey(c => c.CategoriesId).OnDelete(DeleteBehavior.Restrict);
            builder.HasCheckConstraint("CK_Courses_DiscountPercentage", "[DiscountPercentage] >= 0 AND [DiscountPercentage] <= 100");
            builder.Property(c => c.Description).HasMaxLength(1000);
            builder.Property(c => c.Title).HasMaxLength(30);
            builder.Property(c=>c.CourseStatus).HasConversion<string>().IsRequired().HasMaxLength(20);
          

        }
    }
}
