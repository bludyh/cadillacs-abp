using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class Extensions
    {

        public static async Task<List<T>> ToListAsyncFallback<T>(this IQueryable<T> collection)
        {
            return (collection is IAsyncEnumerable<T>)
                ? await collection.ToListAsync()
                : collection.ToList();
        }

    }
}
