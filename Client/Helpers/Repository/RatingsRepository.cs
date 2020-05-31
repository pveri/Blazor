using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Repository
{
    public class RatingsRepository : IRatingsRepository
    {
        readonly IHttpService httpService;
        private string url = "api/ratings";
        public RatingsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task PostRating(MovieRatings rating)
        {
            var response = await httpService.Post(url, rating);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
