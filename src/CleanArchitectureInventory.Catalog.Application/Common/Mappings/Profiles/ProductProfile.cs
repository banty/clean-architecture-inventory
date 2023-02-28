using System;
using AutoMapper;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Domain.Entities;

namespace CleanArchitectureInventory.Catalog.Application.Common.Mappings.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}

