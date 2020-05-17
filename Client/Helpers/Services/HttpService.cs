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

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var result = DeserializeResponse<T>(responseStr);
                return new HttpResponseWrapper<T>(result, true, response);
            }
            return new HttpResponseWrapper<T>(default, false, response);
        }
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var toReturn = DeserializeResponse<TResponse>(responseStr);
                return new HttpResponseWrapper<TResponse>(toReturn, true, response);
            }
            return new HttpResponseWrapper<TResponse>(default, false, response);
        }

        private T DeserializeResponse<T>(string str) => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
    }
}
