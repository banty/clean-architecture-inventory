using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Infrastructure.Common.Interceptors;
using CleanArchitectureInventory.Receiving.Infrastructure.Persistance;
using CleanArchitectureInventory.Receiving.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureInventory.Receiving.Infrastructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddDbContext<ApplicationDbContext>(options =>
               {
                   options.UseSqlServer(configuration.GetConnectionString("ReceivingDb"),
                       builder=>builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                   
               }
               
                );
            services.AddScoped<IApplicationDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicaitonDbContextInitializer>();


            return services;
        }
    }
}

