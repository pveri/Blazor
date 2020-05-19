using BlazorMovies.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            var toReturn = queryable.Skip((paginationDTO.CurrentPage - 1) * paginationDTO.Records)
                            .Take(paginationDTO.Records);
            return toReturn;
        }
    }
}
