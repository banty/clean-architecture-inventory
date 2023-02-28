using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureInventory.Receiving.Applicaiton
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());

            return service;
        }
    }
}

