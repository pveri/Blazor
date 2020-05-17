using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var movies = await dbContext.MovieGenres.ToListAsync();
            return Ok(movies);
        }
    }
}