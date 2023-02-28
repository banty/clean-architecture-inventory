using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Execptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Products.Command
{
    public class ProductDeleteCommand :IRequest
    {
        public ProductDeleteCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand>
    {
        private readonly IApplicationDbContext _context;

        public ProductDeleteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async  Task<Unit> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(t => t.Id == request.Id);

            if(product == null)
            {
                throw new NotFoundException(nameof(product), request.Id.ToString());
            }
            _context.Products.Remove(product);
           await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

