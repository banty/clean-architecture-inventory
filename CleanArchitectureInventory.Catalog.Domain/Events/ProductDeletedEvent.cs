using System;
using CleanArchitectureInventory.Catalog.Domain.Entities;

namespace CleanArchitectureInventory.Catalog.Domain.Events
{
    public class ProductDeletedEvent
    {
        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }
        public Product  Product { get;  }
    }
}

