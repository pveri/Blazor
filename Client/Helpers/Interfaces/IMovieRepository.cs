using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Interfaces
{
    public interface IMovieRepository
    {
        public Task<int> CreateMovie(Movie movie);
        Task<T> Get<T>(string url);
        Task<MovieDTO> GetMovieDTO(int id);
        public Task<List<Movie>> GetMovies();
    }
}
