using Application.Filters;
using Domain.Models;
using FluentValidation.AspNetCore;
using Infra.CrossCutting.DependencyInjection;
using Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                     .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // Allow any origin
                    .AllowCredentials()); // Allow credentials

            });

            Environment.SetEnvironmentVariable("SQL_CONNECTION", Configuration.GetConnectionString("SQL_CONNECTION"));
            Environment.SetEnvironmentVariable("POSTGRESQL", Configuration.GetConnectionString("PostgreSQL"));
            Environment.SetEnvironmentVariable("AUDIENCE", Configuration.GetValue<string>("Audience"));
            Environment.SetEnvironmentVariable("ISSUER", Configuration.GetValue<string>("Issuer"));
            Environment.SetEnvironmentVariable("KEYSEC", Configuration.GetValue<string>("KeySec"));
            Environment.SetEnvironmentVariable("SECONDS", Configuration.GetValue<string>("Seconds"));

            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", Configuration.GetValue<string>("ExternalProviders:SendGrid:ApiKey"));
            Environment.SetEnvironmentVariable("SENDGRID_SENDER_EMAIL", Configuration.GetValue<string>("ExternalProviders:SendGrid:SenderEmail"));
            Environment.SetEnvironmentVariable("SENDGRID_SENDER_NAME", Configuration.GetValue<string>("ExternalProviders:SendGrid:SenderName"));

            // Dependency Configuration
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidationFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseModel>());

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });


            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ferramentaria.Application", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            if (env.IsDevelopment())
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ferramentaria.Application v1"));

            if (env.IsProduction())
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Ferramentaria.Application v1"));

            // Para uso no IIS
            if (env.IsStaging())
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Ferramentaria.Application v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Migrations            
            if (!env.IsDevelopment())
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                            .CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<FerramentariaContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}