using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Exceptions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using CleanArchitectureInventory.Catalog.Domain.Events;
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

        public UpdateProductCommandHandler(IApplicaitonDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id,cancellationToken);

            if (product == null) throw new NotFoundException(nameof(Product), request.Id);

            product.Name = request.Name;

            product.AddDomainEvent(new ProductModifiedEvent(product));

            _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;

        }
    }
}

