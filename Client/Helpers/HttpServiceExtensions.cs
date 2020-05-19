using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public static class HttpServiceExtensions
    {
        public static async Task<T> GetHelper<T>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public static async Task<PaginatedResponse<T>> GetHelper<T>(this IHttpService httpService, string url, PaginationDTO dto)
        {
            var pageRequest = $"CurrentPage={dto.CurrentPage}&Records={dto.Records}";
            pageRequest = url.Contains("?") ? $"{url}&{pageRequest}" :  $"{url}?{pageRequest}";

            var response = await httpService.Get<T>(pageRequest);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            var totalPage = int.Parse(response.ResponseMessage.Headers.GetValues("TotalPages").FirstOrDefault());

            return new PaginatedResponse<T>
            {
                Response = response.Response,
                TotalPages = totalPage
            };
        }
    }
}
