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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p=>p.PaymentId);
            builder.Property(p => p.PaymentId).ValueGeneratedOnAdd();

            builder.HasOne(p => p.ApplicationUser).WithMany(AU => AU.payments).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Courses).WithMany(c => c.payments).HasForeignKey(p => p.CourseId);
        }
    }
}
