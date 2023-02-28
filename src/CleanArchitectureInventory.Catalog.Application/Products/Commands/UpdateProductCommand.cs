using System;
using CleanArchitectureInventory.Application.Contracts;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Exceptions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using CleanArchitectureInventory.Catalog.Domain.Events;
using MassTransit;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IApplicaitonDbContext _context;
        private readonly IPublishEndpoint _publishEndPoint;

        public UpdateProductCommandHandler(IApplicaitonDbContext context,
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndPoint = publishEndpoint;
        }
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id,cancellationToken);

            if (product == null) throw new NotFoundException(nameof(Product), request.Id);

            product.Name = request.Name;

            product.AddDomainEvent(new ProductModifiedEvent(product));

            _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);

            await _publishEndPoint.Publish<ProductUpdated>(new
            {
                Id = product.Id,
                Name = product.Name,
                CommandId = Guid.NewGuid().ToString()
            }) ;

            // Create product create domain event
            // publish domain event

            return product.Id;

        }
    }
}

