using System;
using CleanArchitectureInventory.Application.Contracts;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using CleanArchitectureInventory.Catalog.Domain.Events;
using MassTransit;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string? Name { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicaitonDbContext _context;
        private readonly IBus _publishEndPoint;

        public CreateProductCommandHandler(IApplicaitonDbContext context,
            IBus publishEndpoint)
        {
            _context = context;
            _publishEndPoint = publishEndpoint;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = request.Name
            };


            product.AddDomainEvent(new ProductCreatedEvent(product));

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            await _publishEndPoint.Publish<ProductCreated>(new
            {
                Id = product.Id,
                Name = product.Name,
                CommandId = Guid.NewGuid().ToString()
            });

            return product.Id;
        }
    }
}



