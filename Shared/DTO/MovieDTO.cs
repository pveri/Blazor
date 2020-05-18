using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class MovieDTO
    {
        public MovieDTO()
        {

        }
        public MovieDTO(Movie movie)
        {
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Summary = movie.Summary;
            this.Trailer = movie.Trailer;
            this.InTheatres = movie.InTheatres;
            this.ReleaseDate = movie.ReleaseDate;
            this.Poster = movie.Poster;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheatres { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public List<PersonDTO> Actors { get; set; }
    }
}
