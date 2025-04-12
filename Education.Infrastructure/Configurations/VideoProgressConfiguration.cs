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
    public class VideoProgressConfiguration : IEntityTypeConfiguration<VideoProgress>
    {
        public void Configure(EntityTypeBuilder<VideoProgress> builder)
        {
            

            builder.HasKey(vp => new { vp.VideoId, vp.UserId });

            

            builder.Property(vp => vp.IsWatched)
                .IsRequired();

            builder.HasOne(vp => vp.video)
                .WithMany()
                .HasForeignKey(vp => vp.VideoId);

            builder.HasOne(vp => vp.applicationUser)
                .WithMany()
                .HasForeignKey(vp => vp.UserId);
        }

    }
    
}
