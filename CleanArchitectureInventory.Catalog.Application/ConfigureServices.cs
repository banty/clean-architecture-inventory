using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}

