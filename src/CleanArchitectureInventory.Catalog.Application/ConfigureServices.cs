using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MassTransit;

namespace CleanArchitectureInventory.Catalog.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMassTransit(m =>
            {
                m.SetKebabCaseEndpointNameFormatter();
                m.SetInMemorySagaRepositoryProvider();
                var entryAssembly = Assembly.GetEntryAssembly();

                m.AddConsumers(entryAssembly);
                m.AddSagaStateMachines(entryAssembly);
                m.AddSagas(entryAssembly);
                m.AddActivities(entryAssembly);

                m.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", crd =>
                    {
                        crd.Username("guest");
                        crd.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
           

            return services;
        }

    }
}

