using System;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Application.Common.Abstractions
{
    public interface IApplicaitonDbContext
    {
        DbSet<Product> Products { get; }

       Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }


}

