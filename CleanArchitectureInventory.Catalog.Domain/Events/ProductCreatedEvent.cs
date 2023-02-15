using System;
using CleanArchitectureInventory.Catalog.Domain.Common;
using CleanArchitectureInventory.Catalog.Domain.Entities;

namespace CleanArchitectureInventory.Catalog.Domain.Events
{
    public class ProductCreatedEvent : BaseEvent
    {

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get;  }

    }
}

