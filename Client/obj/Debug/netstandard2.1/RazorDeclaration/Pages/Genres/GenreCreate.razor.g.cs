#pragma checksum "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\Pages\Genres\GenreCreate.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "489b88fdd7e4abb5c6e95d32c4caf9ea63cd00fa"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorMovies.Client.Pages.Genres
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Client.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Shared.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using static BlazorMovies.Client.Shared.MainLayout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Client.Pages.Genres.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\_Imports.razor"
using BlazorMovies.Client.Pages.People.Components;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/genres/create")]
    public partial class GenreCreate : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\Pages\Genres\GenreCreate.razor"
       
    private Genre Genre=new Genre();

    private void Create()
    {
        Console.WriteLine(Genre.Name);
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
