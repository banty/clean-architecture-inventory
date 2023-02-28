using System;
using CleanArchitectureInventory.Receiving.Domain.Common;
using CleanArchitectureInventory.Receiving.Domain.Entities;

namespace CleanArchitectureInventory.Receiving.Domain.Events
{
    public class ProductModifiedEvent : BaseEvent
    {
        public ProductModifiedEvent(Product product)
        {
            Product = product;
        }
        public Product Product { get; set; }
    }
}

