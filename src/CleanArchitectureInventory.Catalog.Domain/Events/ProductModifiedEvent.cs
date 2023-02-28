using System;
using CleanArchitectureInventory.Catalog.Domain.Common;
using CleanArchitectureInventory.Catalog.Domain.Entities;

namespace CleanArchitectureInventory.Catalog.Domain.Events
{
    public class ProductModifiedEvent:BaseEvent
    {
        public ProductModifiedEvent(Product product)
        {
            Product = product;
        }
        public Product Product{get;}
    }
}

