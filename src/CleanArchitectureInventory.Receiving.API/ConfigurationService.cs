using System;
using System.Reflection;
using CleanArchitectureInventory.Application.Contracts;
using CleanArchitectureInventory.Receiving.API.Common.Filters;
using CleanArchitectureInventory.Receiving.API.Services;
using CleanArchitectureInventory.Receiving.API.Worker;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Infrastructure.Persistance;
using MassTransit;

namespace CleanArchitectureInventory.Receiving.API
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddAPIService(this IServiceCollection services)
        {
           
            services.AddSingleton<ICurrentUser, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilters));
            });

            services.AddMassTransit(m =>
            {
                var entryAssembly = Assembly.GetEntryAssembly();

                m.AddConsumers(entryAssembly);
                m.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });

                    cfg.ConfigureEndpoints(ctx);
                });

            });

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
            return services;
        }
    }
}

