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
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(v => v.VideoId);

            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.Description)
                .HasMaxLength(1000);

            builder.Property(v => v.VideoDuration)
                .IsRequired();
            builder.Property(v => v.IsFree)
               .IsRequired();

            builder.Property(v => v.VideoImage)
                .HasMaxLength(500);

            builder.Property(v => v.VideoData)
                .HasMaxLength(500);

            builder.HasOne(v => v.Section)
                .WithMany(s => s.Videos)
                .HasForeignKey(v => v.SectionId);
            builder.Property(v => v.IsFree)
               .IsRequired();

            builder.Property(v => v.VideoImage)
                .HasMaxLength(500);

            builder.Property(v => v.VideoData)
                .HasMaxLength(500);

           
        }
    }
}
