using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public static class NavigationManagerExtensions
    {
        public static Dictionary<string,string> ParseQueryString(this Microsoft.AspNetCore.Components.NavigationManager nm, string qs)
        {
            try
            {
                var toreturn = qs.Split("?", StringSplitOptions.None)[1];
                var qsDict = toreturn.Split("&").ToDictionary(
                    x => x.Split("=")[0],
                    x => Uri.UnescapeDataString(x.Split("=")[1]));
                return qsDict;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
