using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // This doesn't work in EF CORE
        //public ICollection<Movie> Movies { get; set; }
        public ICollection<MoviesGenre> MoviesGenres { get; set; } = new List<MoviesGenre>();
    }
}
