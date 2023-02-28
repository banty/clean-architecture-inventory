using System;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Exceptions;
using CleanArchitectureInventory.Catalog.Domain.Entities;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicaitonDbContext _context;

        public DeleteProductCommandHandler(IApplicaitonDbContext context)
        {
            _context = context;
        }
       public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id,cancellationToken);
            if (product == null) throw new NotFoundException(nameof(Product), request.Id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }

      
    }
}

