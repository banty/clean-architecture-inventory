using System;
using CleanArchitectureInventory.Receiving.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Common
{
    public static class  PublishDomainEvent 
    {
        public static void  SendDomainEvent(this IMediator mediator,DbContext context, CancellationToken cancellationToken)
        {
            var entries = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(t => t.Entity.DomainEvents.Any())
                .Select(t=>t.Entity)
                .ToList();

            var domainEvents = entries.SelectMany(e => e.DomainEvents).ToList();

            entries.ForEach(e => e.ClearDomainEvent());

            foreach(var domainEvent in domainEvents)
            {
                mediator.Publish(domainEvent,cancellationToken);

            }



        }
    }
}

