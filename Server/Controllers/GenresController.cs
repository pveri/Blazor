using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Client.Pages.Genres;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genres = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(genres);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genre genre)
        {
            // context.Genres.Update(genre);
            context.Attach(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}