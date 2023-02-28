using System;
using CleanArchitectureInventory.Receiving.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Persistance.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
       

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name)
                 .HasMaxLength(200)
                .IsRequired();

           
        }
    }
}

