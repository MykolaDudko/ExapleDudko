using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebApiCore.Api.Configuration;
using WebApiCore.Api.Services;
using WebApiCore.Api.Services.BackgroundService;
using WebApiCore.Data.Context;
using WebApiCore.Data.Models;
using WebApiCore.Data.Repository;

namespace WebApiCore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MongoDBSettings>(
                Configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddSingleton<MongoContext>();

            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<CurrentWeather>, WeatherRepository>();

            services.AddHostedService<TimedHostedService>();
            
            services.Configure<WeatherConfiguration>(Configuration.GetSection("WeatherConfiguration"));
            services.AddScoped<IWeatherService, WeatherService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                        Title = "WebCoreApi API",
                        Description = "A simple example ASP.NET Core Web API",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Nikita Tsyhankov",
                                Email = string.Empty,
                                Url = new Uri("https://twitter.com/vanmxpx"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                                Url = new Uri("https://example.com/license"),
                        }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}