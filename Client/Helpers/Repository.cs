using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public class RepositoryInMemory : IRepository
    {
        public List<Movie> GetMovies()
        {
            return new List<Movie> {
                                    new Movie{ReleaseDate = DateTime.Parse("July/03/2019"), Title = "Pretty Woman"},
                                    new Movie{ReleaseDate = DateTime.Parse("July/04/2019"), Title = "Independence Day"},
                                    new Movie{ReleaseDate = DateTime.Parse("October/31/2019"), Title = "Nightmare Before Christmas"}
                                   };
        }
    }
}
