using System;
using CleanArchitectureInventory.Catalog.API.Filters;
using CleanArchitectureInventory.Catalog.API.Services;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "catalogs");
                });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}

