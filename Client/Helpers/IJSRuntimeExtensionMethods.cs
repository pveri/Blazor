using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async Task<bool> Confirm(this IJSRuntime jSRuntime, string param)
        {
            return await jSRuntime.InvokeAsync<bool>("confirm", param);
        }

        public static async ValueTask MyFunction(this IJSRuntime jsRuntime)
        {
            await Task.Delay(5000);
            await jsRuntime.InvokeVoidAsync("testme");
        }

        public static ValueTask<T> GetAsync<T>(IJSRuntime jsRuntime, string key)
            => jsRuntime.InvokeAsync<T>("localStorage.getItem", key);

        public static ValueTask SetAsync(IJSRuntime jsRuntime, string key, object value)
            => jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);

        public static ValueTask DeleteAsync(IJSRuntime jsRuntime, string key)
            => jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}
