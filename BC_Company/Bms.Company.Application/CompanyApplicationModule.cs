using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bms.Application;

public static class CompanyApplicationModule
{
    public static IServiceCollection RegisterCompanyApplicationModule(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator>();
        services.AddMediatR(new[]
        {
            typeof(CreateCompanyWithUserCommandHandler).GetTypeInfo().Assembly
        });

        return services;
    }
}