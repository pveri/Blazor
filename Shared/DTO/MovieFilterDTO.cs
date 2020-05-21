using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class MovieFilterDTO
    {
        public int Page { get; set; }
        public int RecordPerPage { get; set; }
        public PaginationDTO PaginationDto
        {
            get
            {
                return new PaginationDTO () { Records = RecordPerPage, CurrentPage = Page };
            }
        }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public bool InTheatres { get; set; }
        public bool UpComingRelease { get; set; }
    }
}
