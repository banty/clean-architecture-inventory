using System;
using CleanArchitectureInventory.Application.Contracts;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Application.Products.Commands;
using CleanArchitectureInventory.Catalog.Application.Products.Queries;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureInventory.Catalog.API.Controllers
{
   // [Authorize]
    public class ProductController :ApiControllerBase
    {
        private readonly IBus _bus;

        public ProductController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination([FromQuery] ProductWithPaginatedListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
           
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id,UpdateProductCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await Mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}

