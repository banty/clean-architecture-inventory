using System;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Execptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Products.Command
{
    public class ProductUpdateCommand :IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        private readonly IApplicationDbContext _context;

        public ProductUpdateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product =await  _context.Products.SingleOrDefaultAsync(t => t.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(product), request.Id.ToString());
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

