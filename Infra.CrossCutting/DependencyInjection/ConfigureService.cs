using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Interfaces.Services;
using Domain.Security;
using Infra.CrossCutting.Automapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.EmailSender;
using Service.EmailSender.Interfaces;
using Service.EmailService;
using Service.EmailService.Interfaces;
using Service.Services;
using System;

namespace Infra.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Services
            serviceCollection.AddTransient<IFerramentaService, FerramentaService>();
            serviceCollection.AddTransient<IColaboradorService, ColaboradorService>();

            // Automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new ModelToDtoProfile());
            });

            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);

            // Authentication
            var signingConfigurations = new SigningConfiguration();
            serviceCollection.AddSingleton(signingConfigurations);

            serviceCollection.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("ISSUER");

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Authorization
            serviceCollection.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            // Email
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<ISendGridEmailSender, SendGridEmailSender>();
            serviceCollection.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                options.SenderEmail = Environment.GetEnvironmentVariable("SENDGRID_SENDER_EMAIL");
                options.SenderName = Environment.GetEnvironmentVariable("SENDGRID_SENDER_NAME");
            });
        }
    }
}