using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public static class HttpContextHelper
    {
        public static async Task AddPagesResponse<T> (this HttpContext context, IQueryable<T> query, int recordsPerPage)
        {
            if (context == null)
            {
                throw new ApplicationException();
            }

            var totalPages = Math.Ceiling((double)await query.CountAsync()/recordsPerPage);
            context.Response.Headers.Add("TotalPages", totalPages.ToString());
        }
    }
}
