using System;
using System.Linq;
using System.Collections.Generic;

namespace DataBase.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> If<TResult>(this IEnumerable<TResult> source, bool cond, Func<TResult, bool> func)
        {
            if (cond)
            {
                source = source.Where(func);
            }

            return source;
        }

        public static IOrderedEnumerable<TResult> OrderBy<TResult>(this IQueryable<TResult> source, Func<TResult, object> func, bool desc)
        {
            return desc ? source.AsEnumerable().OrderByDescending(func) : source.AsEnumerable().OrderBy(func);
        }
    }
}
