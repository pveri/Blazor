using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.IRepositories
{
    public interface IGenreRepository
    {
        public Task CreateGenre(Genre genre);

        public Task<List<Genre>> GetGenres();
        public Task<Genre> GetGenre(int id);
        public Task UpdateGenre(Genre genre);
        public Task DeleteGenre(int Id);
    }
}
