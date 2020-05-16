using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public class HttpResponseWrapper <T>
    {
        public HttpResponseWrapper(T response, bool success, HttpResponseMessage msg)
        {
            Success = success;
            Response = response;
            ResponseMessage = msg;
        }

        public bool Success { get; set; }
        public T Response { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }
        public async Task<string> GetBody()
        {
           return await ResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
