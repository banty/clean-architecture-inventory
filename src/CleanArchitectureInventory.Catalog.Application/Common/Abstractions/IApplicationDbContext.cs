using System;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Application.Common.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }

       Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }


}

