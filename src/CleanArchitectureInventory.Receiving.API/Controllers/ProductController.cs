using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Model;
using CleanArchitectureInventory.Receiving.Applicaiton.Products.Command;
using CleanArchitectureInventory.Receiving.Applicaiton.Products.Query;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitectureInventory.Receiving.API.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ListItemPaginated<ProductDto>>> GetProductsWithPagination([FromQuery] ProductListQuery query)
        {
           
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(ProductCreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await Mediator.Send(new ProductDeleteCommand(id));

            return NoContent();
        }
    }
}

