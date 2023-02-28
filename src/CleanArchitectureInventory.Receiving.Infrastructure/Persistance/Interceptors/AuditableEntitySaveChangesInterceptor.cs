using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Common.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser,IDateTime dateTime)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            UpdateContext(eventData.Context);
            return this.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateContext(eventData.Context);
            return this.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateContext(DbContext? context)
        {
            if (context == null) return;
            var entries = context.ChangeTracker.Entries<BaseAuditableEntity>();

            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.Created = _dateTime.GetDateTime;
                    entry.Entity.CreatedBy = _currentUser.Name;
                    
                }

                if(entry.State == EntityState.Modified || entry.State== EntityState.Added)
                {
                    entry.Entity.LastModifiedDate = _dateTime.GetDateTime;
                    entry.Entity.LastModifiedBy = _currentUser.Name;
                   
                }
            }
           
            
        }
    }
}

