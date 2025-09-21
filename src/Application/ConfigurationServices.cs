using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ConfigurationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager = new FluentValidationCustomLanguageManager();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }

}