using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnionApp.Infra.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnionApp.CrossCutting.Logging.Interfaces;
using OnionApp.CrossCutting.Logging.Implementations;
using OnionApp.AppServices.Api;
using OnionApp.Domain.Models.Repos;
using OnionApp.AppServices.Repository;
using OnionApp.AppServices.Api.Services;
using OnionApp.AppServices.Repository.DataInterfaces;

namespace OnionApp.Ui.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<IDataContext, MainDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("MainDbContext"))
            );

            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, UserSqlRepository>();
            services.AddScoped<IRoleRepository, RoleSqlRepository>();



            services.AddScoped<IAppLogger>(x => new NLogSqlLogger(new NLogSqlOptions 
                { 
                    InstallConnectionString = Configuration.GetConnectionString("LogsDbInstall"), //connection string used to create the logs database (need elevated DB user privileges in the connection string)
                    ConnectionString = Configuration.GetConnectionString("LogsDb"), //connection string used to insert records into log DB
                    LogsTableName = "Logs",
                    CreateDatabaseIfNotExists = true, //may want to set to false in prod
                    CreateLogsTableIfNotExists = true //may want to set to false in prod
                }));

            services.AddControllers();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Onion App UI API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
