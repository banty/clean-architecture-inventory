using System;
using CleanArchitectureInventory.Catalog.API.Filters;
using CleanArchitectureInventory.Catalog.API.Services;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance;

namespace CleanArchitectureInventory.Catalog.API
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddAPIService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                    
            });

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

           
            return services;
        }
    }
}

