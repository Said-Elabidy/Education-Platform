using Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.Configurations
{
    
        public class QuestionConfiguration : IEntityTypeConfiguration<Question>
        {
            public void Configure(EntityTypeBuilder<Question> builder)
            {
                
                builder.HasKey(q => q.Id);

                
                builder.Property(q => q.Header)
                    .IsRequired()
                    .HasMaxLength(500); 

                builder.Property(q => q.Order)
                    .IsRequired();

                
                builder.Property(q => q.CorrectAnswer)
                    .IsRequired();

                
                builder.HasOne(q => q.Quiz)
                    .WithMany(q => q.Questions)
                    .HasForeignKey(q => q.QuizId)
                    .OnDelete(DeleteBehavior.Cascade); 

                builder.HasIndex(q => q.Order);

                
            }
        }
    
}
