using Education.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
             
            builder.HasKey(q => q.Id);

             
            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(500); 


            
            builder.Property(q => q.NumOfQuestion)
                .IsRequired(false); 

            
            builder.HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(s => s.Section)
                   .WithOne(q => q.Quiz)
                   .HasForeignKey<Quiz>(f => f.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
