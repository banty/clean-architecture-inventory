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
        public async Task<ActionResult<PagenatedList<ProductDto>>> GetProductsWithPagination([FromQuery] ProductWithPagenatedListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
           
            //await _bus.Publish<ProductCreated>(new
            //{
            //    Id = 1,
            //    Name = command.Name,
            //    CommandId = Guid.NewGuid().ToString()
            //}); 
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

