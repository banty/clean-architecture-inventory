using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using CleanArchitectureInventory.Catalog.Domain.Events;
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

        public CreateProductCommandHandler(IApplicaitonDbContext context)
        {
            _context = context;
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

            return product.Id;
        }
    }
}



