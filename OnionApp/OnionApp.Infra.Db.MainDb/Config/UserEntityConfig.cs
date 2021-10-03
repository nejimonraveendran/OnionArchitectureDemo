using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Infra.Db.MainDb.Config
{
    class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DateCreated);
        }
    }
}
