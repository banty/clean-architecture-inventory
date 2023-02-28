using System;
using AutoMapper;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Model;
using CleanArchitectureInventory.Receiving.Domain.Entities;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>();
        }
    }
}

