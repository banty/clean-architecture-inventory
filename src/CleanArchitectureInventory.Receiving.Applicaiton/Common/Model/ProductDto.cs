using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Model
{
    public class ProductDto:IDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}

