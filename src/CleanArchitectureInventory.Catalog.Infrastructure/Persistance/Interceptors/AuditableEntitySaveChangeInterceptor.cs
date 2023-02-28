using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Persistance.Interceptors
{
    public class AuditableEntitySaveChangeInterceptor :SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangeInterceptor(ICurrentUserService currentUserService,IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntity(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntity(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntity(DbContext? context)
        {
            if (context == null) return;

            foreach(var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.Created = _dateTime.GetDateTime();
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                }

                if(entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = _dateTime.GetDateTime();
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                }
            }
        }
    }
}

