using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Receiving.Applicaiton.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Mappings
{
    public static class MappingExtensions 
    {
        public static  Task<ListItemPaginated<T>> GetListItemPaginated<T>(this IQueryable<T> queryable ,int pageNumber, int pageSize) where T:class
        =>  ListItemPaginated<T>.CreatePaginatedListItem(queryable.AsNoTracking(), pageNumber, pageSize);

        public static  Task<List<T>> ProjectToDto<T>(this IQueryable source, IConfigurationProvider configuration) where T : class
            =>  source.ProjectTo<T>(configuration).AsNoTracking().ToListAsync();


    }
}

