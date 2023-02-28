using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Catalog.Application.Common.Abstractions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using CleanArchitectureInventory.Catalog.Application.Common.Mappings;
using MediatR;

namespace CleanArchitectureInventory.Catalog.Application.Products.Queries
{
    public class ProductWithPagenatedListQuery : IRequest<PagenatedList<ProductDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class ProductWithPagenatedListQueryHandler : IRequestHandler<ProductWithPagenatedListQuery,PagenatedList<ProductDto>>
    {
        private readonly IApplicaitonDbContext _context;
        private readonly IMapper _mapper;

        public ProductWithPagenatedListQueryHandler(IApplicaitonDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagenatedList<ProductDto>> Handle(ProductWithPagenatedListQuery request, CancellationToken cancellationToken)
        {
          return await _context.Products
                 .OrderBy(t => t.Name)
                 .ProjectTo<ProductDto>( _mapper.ConfigurationProvider)
                 .PagenatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}

