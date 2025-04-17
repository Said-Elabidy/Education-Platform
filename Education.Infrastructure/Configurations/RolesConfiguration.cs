using Education.Domain.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Education.Infrastructure.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roles = new List<IdentityRole>
            {
            new() { Id ="1", Name = MyRoles.Admin, NormalizedName = MyRoles.Admin.ToUpper() },
            new() { Id ="2", Name = MyRoles.User, NormalizedName = MyRoles.User.ToUpper() },
            
            };

           builder.HasData(roles);
        }
    }
}
