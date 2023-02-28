using System;
using CleanArchitectureInventory.Receiving.Domain.Common;
using CleanArchitectureInventory.Receiving.Domain.Entities;

namespace CleanArchitectureInventory.Receiving.Domain.Events
{
    public class ProductCreatedEvent :BaseEvent
    {
        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
        public Product Product { get; set; }
    }
}

