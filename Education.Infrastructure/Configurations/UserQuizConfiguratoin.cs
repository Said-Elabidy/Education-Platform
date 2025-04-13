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
   

    public class UserQuizConfiguration : IEntityTypeConfiguration<UserQuiz>
    {
        public void Configure(EntityTypeBuilder<UserQuiz> builder)
        {
            builder.HasKey(uq => new { uq.UserId, uq.QuizId });

           // Relation With User ?? 

             builder.HasOne(uq => uq.Quiz)
                .WithMany(q => q.UserQuizzes)
                .HasForeignKey(uq => uq.QuizId)
                .OnDelete(DeleteBehavior.Cascade); // Or Restrict

             builder.Property(uq => uq.Score)
                .IsRequired()
                .HasDefaultValue(0);

             builder.Property(uq => uq.IsPassed)
                .IsRequired()
                .HasDefaultValue(false);
           
        }
    }
}
