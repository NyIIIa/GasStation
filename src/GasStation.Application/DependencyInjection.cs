using System.Reflection;
using AutoMapper;
using FluentValidation;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GasStation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            var dateTimeProvider = provider.GetService<IDateTimeService>();
            
            cfg.AddProfiles(new List<Profile>()
            {
                new FuelProfile(dateTimeProvider),
                new InvoiceProfile(dateTimeProvider),
                new ReportProfile(dateTimeProvider)
            });
        }).CreateMapper());
        
        return services;
    }
}