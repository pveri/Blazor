using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Library;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.ServerSide.Repositories
{
    public class MoviesRepository : IMovieRepository
    {

        private readonly MoviesDbContext dbContext;
        public MoviesRepository(MoviesDbContext db)
        {
            this.dbContext = db;
        }
        public Task<int> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResponse<List<Movie>>> Filter(MovieFilterDTO filterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get<T>(string url)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDTO> GetMovieDTO(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetMovies()
        {
            var movies = await dbContext.Movies.ToListAsync();
            return movies;
        }

        public Task UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
