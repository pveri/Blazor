using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        readonly MoviesDbContext dbContext;
        readonly IFileStorageService fileService;

        public MoviesController(MoviesDbContext db, IFileStorageService fileService)
        {
            this.dbContext = db;
            this.fileService = fileService;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Movie movie)
        {
            if (!String.IsNullOrEmpty(movie.Poster))
            {
                var posterBytes = Convert.FromBase64String(movie.Poster);
                movie.Poster = await fileService.SaveFile(posterBytes, "jpg", "movies");
            }

            dbContext.Movies.Add(movie);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var movies = await dbContext.Movies.ToListAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> Get(int Id)
        {
            var movie = await dbContext.Movies.Where(x => x.Id == Id)
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.Actors).ThenInclude(x => x.Person)
                .FirstOrDefaultAsync();

            var movieDto = new MovieDTO(movie);
            movieDto.Actors = movie.Actors.Select(x => new PersonDTO(x.Person) { Character = x.CharacterName }).ToList();
            movieDto.Genres = movie.MoviesGenres.Select(x => new GenreDTO(x.Genre)).ToList();
            return movieDto;
        }
    }
}