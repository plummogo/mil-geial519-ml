using System;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using IO.Swagger.Filters;
using LicensePlate.Service.Interfaces;
using LicensePlate.Service;
using LicensePlate.Repository.Repositories;
using LicensePlate.Repository;

namespace IO.Swagger
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            _configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                    {
                        options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                        options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                    })
                    .AddNewtonsoftJson(opts =>
                    {
                        opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                    })
                    .AddXmlSerializerFormatters();

            services.AddTransient<ILicensePlateService, LicensePlateService>();
            services.AddTransient<ILicensePlateRepository, LicensePlateRepository>();

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.0.11", new OpenApiInfo
                    {
                        Version = "1.0.11",
                        Title = "GEIAL519-ML - Distributed system 'Plate system' project",
                        Description = "Swagger Petstore - OpenAPI 3.0 (ASP.NET Core 3.1)",
                        Contact = new OpenApiContact()
                        {
                            Name = "Swagger Codegen Contributors",
                            Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
                            Email = "apiteam@swagger.io"
                        },
                        TermsOfService = new Uri("http://swagger.io/terms/")
                    });
                    c.CustomSchemaIds(type => type.FullName);
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                    // Sets the basePath property in the Swagger document generated
                    c.DocumentFilter<BasePathFilter>("/api/v3");
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/1.0.11/swagger.json", "Swagger Petstore - OpenAPI 3.0"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
        }
    }
}