using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        readonly IMapper mapper;

        public MoviesController(MoviesDbContext db, IFileStorageService fileService, IMapper mapper)
        {
            this.dbContext = db;
            this.fileService = fileService;
            this.mapper = mapper;
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
            return Ok(movie.Id);
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
        
        [HttpGet("update/{id}")]
        public async Task<ActionResult<MovieUpdateDTO>> GetForUpdate(int Id)
        {
            // TODO: Cleanup
            var movie = await dbContext.Movies.Where(x => x.Id == Id)
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.Actors).ThenInclude(x => x.Person)
                .FirstOrDefaultAsync();

            var movieGenre = movie.MoviesGenres.Select(x => new GenreDTO(x.Genre)).ToList();
            var movieGenreIds = movieGenre.Select(x => x.Id).ToList();

            var allGenres = await dbContext.Genres.Where(x => !movieGenreIds.Contains(x.Id)).ToListAsync();
            var movieDto = new MovieUpdateDTO(movie, allGenres);
            movieDto.Actors = movie.Actors.Select(x => new PersonDTO(x.Person) { Character = x.CharacterName }).ToList();
            movieDto.Genres = movieGenre;

            return movieDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDTO>> Put(Movie movie)
        {
            var dbMovie = await dbContext.Movies
                .Include(x => x.MoviesGenres).ThenInclude(x=>x.Genre)
                .Include(x => x.Actors).ThenInclude(x=>x.Person)
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            dbMovie = mapper.Map(movie, dbMovie);

            if (!string.IsNullOrEmpty(movie.Poster))
            {
                // TODO
                // Big no-no for me! Will keep it like this for now to finish course, but will move out of controller later
                var newPic = await fileService.EditFile(Convert.FromBase64String(movie.Poster), ".jpg", "movies", dbMovie.Poster);
                dbMovie.Poster = newPic;
            }
            await dbContext.SaveChangesAsync();
            // await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var dbMovie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Movies.Remove(dbMovie);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<Movie>>> Filter(MovieFilterDTO dto)
        {
            // TODO: Move to Manager Method
            var movieList = dbContext.Movies.AsQueryable();

            if (dto.GenreId > 0)
            {
                movieList = movieList.Where(x => x.MoviesGenres.Select(y => y.GenreId).Contains(dto.GenreId));
            }
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                movieList = movieList.Where(x => x.Title.Contains(dto.Title));
            }
            if (dto.UpComingRelease)
            {
                var today = DateTime.Now;
                movieList = movieList.Where(x => x.ReleaseDate > today);
            }
            if (dto.InTheatres)
            {
                movieList = movieList.Where(x => x.InTheatres == true);
            }

            await HttpContext.AddPagesResponse(movieList, dto.RecordPerPage);

            
            var movies = await movieList.Paginate(dto.PaginationDto).ToListAsync();

            return movies;
        }
    }
}