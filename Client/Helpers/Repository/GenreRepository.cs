using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Helpers.Repository
{
    public class GenreRepository: IGenreRepository
    {
        readonly IHttpService httpService;
        private string url = "api/genres";
        public GenreRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task CreateGenre(Genre genre)
        {
            var response = await httpService.Post(url, genre);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
