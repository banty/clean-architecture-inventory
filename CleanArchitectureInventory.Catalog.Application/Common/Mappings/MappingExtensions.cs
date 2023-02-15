using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureInventory.Catalog.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Application.Common.Mappings
{
    public static class MappingExtensions
    {

        public static Task<PagenatedList<T>> PagenatedListAsync<T>(this IQueryable<T> quarable, int pageNumber, int pageSize) where T : class
                => PagenatedList<T>.CreatePagenatedListAsync(quarable.AsNoTracking(), pageSize, pageNumber);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable quarable,IConfigurationProvider configuration) where TDestination : class

            => quarable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();


    }
}

