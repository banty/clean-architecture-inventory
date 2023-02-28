using System;
namespace CleanArchitectureInventory.Application.Contracts
{
    public class ProductCreated
    {
        public Guid CommandId { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
    }
}

