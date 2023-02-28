using System;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Application.Common.Models
{
    public class PaginatedList<T> 
    {
        public PaginatedList(List<T> items,int pageNumber,int pageSize,int count)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalCount = count;
            TotalPages= (int)Math.Ceiling(count / (double)pageSize);
        }
        public int TotalPages { get; }
        public int TotalCount { get;  }
        public int PageNumber { get;  }
        public List<T> Items { get;  }

        public bool HasPreviousPage => PageNumber > 1;


        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreatePaginatedListAsync(IQueryable<T> source,int pageSize,int pageNumber)
        {
            int count = source.Count();
            var items = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();


            return new PaginatedList<T>(items, pageNumber, pageSize, count);


        }


    }
}

