using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Domain.Entities;
using CleanArchitectureInventory.Receiving.Domain.Events;
using MediatR;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Products.Command
{
    public class ProductCreateCommand :IRequest<int>
    {
        public string? Name { get; set; }
    }

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProductCreateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product();
            product.Name = request.Name;
            product.AddDomainEvent(new ProductCreatedEvent(product));
             _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product.Id;
                
            
        }
    }
}

