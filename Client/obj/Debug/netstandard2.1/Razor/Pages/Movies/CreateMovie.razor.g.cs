#pragma checksum "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\Pages\Movies\CreateMovie.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "826c6686f32d79408ab598dfc554a6c150c5aff6"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorMovies.Client.Pages.Movies
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/movies/create")]
    public partial class CreateMovie : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>CreateMovie</h3>\r\n\r\n");
            __builder.OpenElement(1, "button");
            __builder.AddAttribute(2, "class", "btn btn-primary");
            __builder.AddAttribute(3, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 5 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\Pages\Movies\CreateMovie.razor"
                                          SaveMovie

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(4, "Save Movie");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 6 "C:\Users\peter\source\repos\BlazorMoviesFinal\Client\Pages\Movies\CreateMovie.razor"
       

    private void SaveMovie()
    {
        navigationManager.NavigateTo("movie");
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591
