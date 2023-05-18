using System.Text.Json;
using System.Text.Json.Serialization;
using GasStation.WebApi.CORS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace GasStation.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,
                                                    ConfigurationManager configurationManager)
    {
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.JsonSerializerOptions.WriteIndented = true;
        });

        #region Add SwaggerUI

        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo {
                Title = "GasStation_Web_API", Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        #endregion

        #region Add CORS

        var corsSettings = new CorsSettings();
        configurationManager.Bind(CorsSettings.SectionName, corsSettings);
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins(corsSettings.AllowedOrigins)
                          .WithMethods(corsSettings.AllowedMethods)
                          .AllowAnyHeader();
                });
        });
        
        #endregion
        
        return services;
    }
}