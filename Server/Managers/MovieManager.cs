using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Managers
{
    public interface IMovieManager
    {
        Task<MovieDTO> GetMovieDetails(int id, string userid);
        Task<List<MovieRatings>> GetRatings(int movieId);

        // double AverageRating(List<MovieRatings> ratings);
    }
    public class MovieManager : IMovieManager
    {
        private readonly MoviesDbContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public MovieManager(MoviesDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this._userManager = userManager;
        }

        public async Task<MovieDTO> GetMovieDetails(int movieId, string userId)
        {
            var movie = await db.Movies.Where(x => x.Id == movieId)
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.Actors).ThenInclude(x => x.Person)
                .FirstOrDefaultAsync();

            var movieDto = new MovieDTO(movie);
            movieDto.Actors = movie.Actors.Select(x => new PersonDTO(x.Person) { Character = x.CharacterName }).ToList();
            movieDto.Genres = movie.MoviesGenres.Select(x => new GenreDTO(x.Genre)).ToList();
            var ratings = await GetRatings(movieId);
            movieDto.AverageRating = AverageRating(ratings);
            movieDto.UserRating = ratings.FirstOrDefault(x => x.UserId == userId)?.Rate ?? 0;
            return movieDto;
        }

        public async Task<List<MovieRatings>> GetRatings (int MovieId)
        {
            return await db.MovieRatings.Where(x => x.MovieId == MovieId).ToListAsync();
        }

        private double AverageRating (List<MovieRatings> ratings)
        {
            if (ratings!=null && ratings.Count() > 0)
                return ratings.Select(x => x.Rate).Average();
            return 0.0;
        }
    }
}
