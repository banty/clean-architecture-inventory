using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Application.Common.Mappings
{
    public static class MappingExtensions
    {

        public static Task<PaginatedList<T>> PaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize) where T : class
                => PaginatedList<T>.CreatePaginatedListAsync(queryable.AsNoTracking(), pageSize, pageNumber);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable,IConfigurationProvider configuration) where TDestination : class

            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();


    }
}

