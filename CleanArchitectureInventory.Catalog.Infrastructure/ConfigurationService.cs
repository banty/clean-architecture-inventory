using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Infrastructure.Identity;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CleanArchitectureInventory.Catalog.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;

namespace CleanArchitectureInventory.Catalog.Infrastructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangeInterceptor>();

            if(configuration.GetValue<bool>("UseInMemoryDb"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchInventoryDb")
                ) ;
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("CleanArchInventory"),
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                ); 
            }


            services.AddScoped<IApplicaitonDbContext>(service => service.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitializer>();

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();


            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>

                options.AddPolicy("CanPurge", policy => policy.RequireRole("Admin"))

            );


            return services;

        }
    }
}

