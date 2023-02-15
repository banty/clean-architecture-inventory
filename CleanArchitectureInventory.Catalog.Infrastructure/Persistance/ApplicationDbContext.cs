using System;
using System.Reflection;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using CleanArchitectureInventory.Catalog.Infrastructure.Common;
using CleanArchitectureInventory.Catalog.Infrastructure.Identity;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Persistance
{
    public class ApplicationDbContext :ApiAuthorizationDbContext<ApplicationUser>,  IApplicaitonDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangeInterceptor _auditableEntitySaveChangeInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                    IOptions<OperationalStoreOptions> operationalStoreOptions,
                                    IMediator mediator,
                                    AuditableEntitySaveChangeInterceptor auditableEntitySaveChangeInterceptor
            ):base(options,operationalStoreOptions)
                                    
        {
            _mediator = mediator;
            _auditableEntitySaveChangeInterceptor = auditableEntitySaveChangeInterceptor;
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangeInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

