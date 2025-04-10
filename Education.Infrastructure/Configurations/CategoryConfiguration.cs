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
    public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(c => c.CategorieId);
            builder.Property(c => c.CategorieId).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired()
                .HasMaxLength(200);
                                                                                        
            builder.HasIndex(c => c.Name).IsUnique();

            
        }
    }
}
