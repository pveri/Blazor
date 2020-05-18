using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class GenreDTO
    {
        public GenreDTO()
        {
           
        }
        public GenreDTO(Genre genre)
        {
            this.Id = genre.Id;
            this.Name = genre.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
