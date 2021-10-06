using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using OnionApp.Infra.Db.MainDb.Config;
using System;
using System.IO;

namespace OnionApp.Infra.Db
{
    public sealed class MainDbContext : DbContext
    {
        public MainDbContext() { }

        public MainDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.ChangeTracker.AutoDetectChangesEnabled = true;
        }

 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Install-Package Microsoft.Extensions.Configuration.Json
            //Install-Package Microsoft.EntityFrameworkCore.SqlServer

            //this is for DB migration
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("dbsettings.json")
                    .Build();

                var connectionString = config.GetConnectionString("MainDbContext");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleEntityConfig());
            modelBuilder.ApplyConfiguration(new UserEntityConfig());
            
        }
    }
}
