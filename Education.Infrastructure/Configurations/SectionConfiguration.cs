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
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(s => s.SectionId);
            builder.Property(s=>s.SectionId).ValueGeneratedOnAdd();
            builder.HasOne(s => s.Courses).WithMany(c => c.Sections).HasForeignKey(s => s.CourseId);
            builder.Property(s => s.SectionName).HasMaxLength(40);
        }
    }
}
