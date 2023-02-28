using System;
using CleanArchitectureInventory.Receiving.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

