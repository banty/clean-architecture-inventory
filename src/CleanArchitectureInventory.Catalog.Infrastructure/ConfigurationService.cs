using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureInventory.Catalog.Infrastructure.Services;

namespace CleanArchitectureInventory.Catalog.Infrastructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangeInterceptor>();

            
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("CleanArchInventory"),
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                ); 
            
            services.AddScoped<IApplicationDbContext>(service => service.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitializer>();
            services.AddTransient<IDateTime, DateTimeService>();

          
         


            return services;

        }
    }
}

