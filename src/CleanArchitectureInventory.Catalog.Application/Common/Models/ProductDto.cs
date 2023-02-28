using System;
using CleanArchitectureInventory.Catalog.Application.Common.Mappings;

namespace CleanArchitectureInventory.Catalog.Application.Common.Models
{
    public class ProductDto:IMapFrom<ProductDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

