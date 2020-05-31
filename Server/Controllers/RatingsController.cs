using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly MoviesDbContext dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingsController(MoviesDbContext db, UserManager<IdentityUser> userManager)
        {
            this.dbContext = db;
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Rate (MovieRatings ratings)
        {
            var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var dbRating = await dbContext.MovieRatings.FirstOrDefaultAsync(x => x.MovieId == ratings.MovieId && x.UserId == user.Id);

            if (dbRating == null)
            {
                ratings.UserId = user.Id;
                ratings.RatingDate = DateTime.UtcNow;
                dbContext.MovieRatings.Add(ratings);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                dbRating.RatingDate = DateTime.UtcNow;
                dbRating.Rate = ratings.Rate;

            }

            return NoContent();
        }
    }
}
