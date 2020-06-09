using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server
{
    public class MoviesDbContext : IdentityDbContext
    {
        public MoviesDbContext()
        {

        }
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MoviesGenre> MovieGenres { get; set; }
        public DbSet<MovieRatings> MovieRatings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().HasKey(x => new { x.MovieId, x.PersonId });
            modelBuilder.Entity<MoviesGenre>().HasKey(x => new { x.MovieId, x.GenreId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
