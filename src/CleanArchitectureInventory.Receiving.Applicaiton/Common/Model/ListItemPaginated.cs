using System;
using CleanArchitectureInventory.Receiving.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Receiving.Applicaiton.Common.Model
{
    public class ListItemPaginated<T> where T:class
    {

        private ListItemPaginated(List<T> items,int pageNumber, int pageSize,int count)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
            TotalPages = (int)Math.Ceiling(Count /(double) pageSize);
            Items = items;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
        public List<T> Items {get; private set;}
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public static async Task<ListItemPaginated<T>> CreatePaginatedListItem(IQueryable<T> source,int pageNumber,int pageSize)
        {
            int count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new ListItemPaginated<T>(items, pageNumber, pageSize, count);

        }


    }
}

