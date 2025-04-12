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
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.HasKey(f => f.FeedBackId);
            builder.Property(f => f.FeedBackId).ValueGeneratedOnAdd();
            builder.HasOne(f => f.ApplicationUser).WithMany(AU => AU.feedBacks).HasForeignKey(f => f.UserId);
            builder.HasOne(f => f.video).WithMany(v => v.feedBacks).HasForeignKey(f => f.VideoId);
            builder.HasCheckConstraint("CK_Courses_Rating_Range", "[Rating] >= 0 AND [Rating] <= 5");
        }
    }
}
