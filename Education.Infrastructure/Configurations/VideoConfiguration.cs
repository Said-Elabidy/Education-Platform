using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Education.Domain.Entities;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // Primary Key
        builder.HasKey(v => v.VideoId);

        // Properties configuration
        builder.Property(v => v.VideoDuration)
    .HasColumnType("time");

        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.Description)
            .HasMaxLength(1000);


        builder.Property(v => v.IsFree)
            .IsRequired();

        builder.Property(v => v.VideoImageUrl)
            .HasMaxLength(500);

        builder.Property(v => v.VideoFileUrl)
            .IsRequired()
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(v => v.Section)
            .WithMany(s => s.Videos)
            .HasForeignKey(v => v.SectionId)
            .OnDelete(DeleteBehavior.Cascade); // Consider if you want cascade delete

        

        
    }
}