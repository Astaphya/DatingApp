using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Common.Helpers
{
    // this class is used to create a paged list of data
    public class PagedList<T> : List<T>
    {
      
        public PagedList(IEnumerable<T> items,int count, int pageNumber,int pageSize) 
        {
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            this.TotalCount = count;
            AddRange(items);
        }
       
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set;}

        // static method to create a paged list from a queryable
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        
    }
}