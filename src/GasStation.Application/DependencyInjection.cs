using System.Reflection;
using FluentValidation;
using GasStation.Application.Common.Behavior;
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
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}