using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Abstractions;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Mappings;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Model;
using CleanArchitectureInventory.Receiving.Domain.Entities;
using MediatR;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Products.Query
{
    public class ProductListQuery:IRequest<ListItemPaginated<ProductDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
    public class ProductListQueryHandler : IRequestHandler<ProductListQuery, ListItemPaginated<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductListQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ListItemPaginated<ProductDto>> Handle(ProductListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                          .OrderBy(t=>t.Name)
                         .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                         .GetListItemPaginated(request.PageNumber,request.PageSize);
        }
    }
}

