using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorMovies.Client.Helpers.Interfaces;
using Newtonsoft.Json;
namespace BlazorMovies.Client.Helpers.Services
{
    public class HttpService: IHttpService
    {
        private readonly HttpClient httpClient;
        public HttpService(HttpClient client)
        {
            this.httpClient = client;
        }
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }
    }
}
