using System;
using CleanArchitectureInventory.Catalog.Domain.Common;

namespace CleanArchitectureInventory.Catalog.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public Product()
        {
        }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}

