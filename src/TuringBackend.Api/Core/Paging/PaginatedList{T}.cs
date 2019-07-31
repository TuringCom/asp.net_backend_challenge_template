using System.Collections.Generic;
using System.Linq;

namespace TuringBackend.Api.Core
{
    public class PaginatedList<T>
    {
        public PaginatedList(IQueryable<T> source, int count)
        {
            TotalCount = count;
            Rows = source.ToList();
        }

        public int TotalCount { get; }

        public List<T> Rows { get; }
    }
}