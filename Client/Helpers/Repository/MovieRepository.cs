using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Repository
{
    
    public class MovieRepository: IMovieRepository
    {
        readonly IHttpService httpService;
        readonly string url = "api/movies";

        public MovieRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<int> CreateMovie(Movie movie)
        {
            var response = await httpService.Post<Movie, int>(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var response = await httpService.Get<List<Movie>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<MovieDTO> GetMovieDTO(int id)
        {
            return await Get<MovieDTO>($"{url}/{id}");
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            return await Get<MovieUpdateDTO>($"{url}/update/{id}");
        }

        public async Task UpdateMovie(Movie movie)
        {
            var response = await httpService.Put<Movie>($"{url}/{movie.Id}", movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteMovie(int Id)
        {
            var response = await httpService.Delete($"{url}/{Id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
