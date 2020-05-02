using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Movie
    {
        public int Id { get; set; } = 1;
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Poster { get; set; }

        public string TitleBrief
        {
            get
            {
                if (String.IsNullOrEmpty(Title))
                {
                    return null;
                }
                else if (Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }

        public string RouteUrl
        {
            get
            {
                return Title.Replace(" ", "-");
            }
        }
    }
}
