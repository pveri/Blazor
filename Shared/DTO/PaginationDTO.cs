using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class PaginationDTO
    {
        public int CurrentPage { get; set; } = 1;
        public int Records { get; set; } = 10;
    }

    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
        public int TotalPages { get; set; }
    }
}
