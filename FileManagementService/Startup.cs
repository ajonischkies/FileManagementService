using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RedSky.FileManagement.Api.Config;
using RedSky.FileManagement.Business.Services;
using RedSky.FileManagement.Contracts.Context;
using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Contracts.Services;
using RedSky.FileManagement.Data.Context;
using RedSky.FileManagement.Data.Repositories;
using System;
using System.Reflection;

namespace FileManagementService
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
            DbConfigRoot dbConfig = new DbConfigRoot();
            Configuration.Bind(dbConfig);

            IFileManagementMongoDbContext context = new FileManagementMongoDbContext(dbConfig.MongoDb);
            IRepositoryWrapper wrapper = new RepositoryWrapper(context);

            services.AddSingleton(wrapper);
            services.AddScoped<IServiceWrapper, ServiceWrapper>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            // Get assembly version
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = "File Management Service",
                    Description = "Containerized simple file management API using MongoDB"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Get assembly version
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($@"/swagger/{version}/swagger.json", $@"File Management Service {version}");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
