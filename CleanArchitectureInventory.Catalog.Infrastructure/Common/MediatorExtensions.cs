using System;
using CleanArchitectureInventory.Catalog.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Common
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEvents (this IMediator mediator, DbContext context)
        {
            var entities = context.ChangeTracker
                                 .Entries<BaseEntity>()
                                 .Where(t => t.Entity.DomainEvents.Any())
                                 .Select(t => t.Entity);
            var domainEvents = entities.SelectMany(t => t.DomainEvents).ToList();

            entities.ToList().ForEach(entity => entity.ClearDomainEvent());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
               
        }
    }
}

