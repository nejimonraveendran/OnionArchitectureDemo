using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Infra.Db.MainDb.Config
{
    class RoleEntityConfig : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            
            builder.HasMany(r => r.Users)
                .WithOne(x => x.Role);

        }

    }
}
