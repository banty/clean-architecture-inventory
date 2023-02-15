using System;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Persistance.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name)
                    .HasMaxLength(200)
                    .IsRequired();

        }
    }
}

