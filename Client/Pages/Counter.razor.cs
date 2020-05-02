using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorMovies.Client.Shared.MainLayout;
namespace BlazorMovies.Client.Pages
{
    public partial class  Counter
    {
        private int currentCount = 0;
        private static int staticCurrentCount = 0;
        
        [JSInvokable]
        public async Task IncrementCount()
        {
            staticCurrentCount = currentCount++;
        }

        [JSInvokable]
        public static async Task<int> GetCurrentCount()
        {
            return await Task.FromResult(staticCurrentCount);
        }
    }
}
