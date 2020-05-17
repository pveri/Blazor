using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Movie
    {
        public int Id { get; set; } 
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheatres { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        public String Poster { get; set; }

        public ICollection<MoviesGenre> MoviesGenres { get; set; } = new List<MoviesGenre>();
        // This doesn't work in EF CORE
        //public ICollection<Genre> Genres { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public string TitleBrief
        {
            get
            {
                if (String.IsNullOrEmpty(Title))
                {
                    return null;
                }
                else if (Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }

        public string RouteUrl
        {
            get
            {
                return Title.Replace(" ", "-");
            }
        }
    }
}
