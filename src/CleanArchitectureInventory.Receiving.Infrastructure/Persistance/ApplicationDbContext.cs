using System;
using System.Collections.Generic;
using System.Reflection;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Domain.Entities;
using CleanArchitectureInventory.Receiving.Infrastructure.Common;
using CleanArchitectureInventory.Receiving.Infrastructure.Common.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext,IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IMediator mediator):base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Product> Products =>   Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _mediator.SendDomainEvent(this, cancellationToken);
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}

