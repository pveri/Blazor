using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Client.Pages.Genres;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly MoviesDbContext context;

        public GenresController(MoviesDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Genre genre)
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var genres = await context.Genres.ToListAsync();
            return Ok(genres);
        }
    }
}