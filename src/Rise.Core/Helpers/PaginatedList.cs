using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rise.Core.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int PageQty { get; set; }
        public int PageSize { get; set; }
        public int Items { get; set; }
        public bool HasPreviousPage => (PageNumber > 1);
        public bool HasNextPage => (PageNumber < PageQty);

        private PaginatedList()
        {

        }

        private PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageQty = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> Create(IQueryable<T> source,
            int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}