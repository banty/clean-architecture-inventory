using System;
using CleanArchitectureInventory.Receiving.Domain.Common;

namespace CleanArchitectureInventory.Receiving.Domain.Entities
{
    public class Product:BaseAuditableEntity
    {
        public string? Name { get; set; }
    }
}

