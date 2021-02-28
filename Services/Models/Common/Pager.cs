using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Services.Models.Common
{
    public class Pager
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }

    public static class PagerExtension
    {
        public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> query, Pager pager) where TSource : class
        {
            pager.Total = query.Count();

            return query
                .AsExpandable()
                .Skip(pager.Size * (pager.Page - 1))
                .Take(pager.Size);
        }
    }
}
