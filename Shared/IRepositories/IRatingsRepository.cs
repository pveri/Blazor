using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.IRepositories
{
    public interface IRatingsRepository
    {
        Task PostRating(MovieRatings rating);
    }
}
