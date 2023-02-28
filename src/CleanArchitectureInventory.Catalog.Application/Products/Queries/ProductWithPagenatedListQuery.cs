using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Application.Common.Mappings;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application.Products.Queries
{
    public class ProductWithPaginatedListQuery : IRequest<PaginatedList<ProductDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class ProductWithPaginatedListQueryHandler : IRequestHandler<ProductWithPaginatedListQuery,PaginatedList<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductWithPaginatedListQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(ProductWithPaginatedListQuery request, CancellationToken cancellationToken)
        {
          return await _context.Products
                 .OrderBy(t => t.Name)
                 .ProjectTo<ProductDto>( _mapper.ConfigurationProvider)
                 .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}

