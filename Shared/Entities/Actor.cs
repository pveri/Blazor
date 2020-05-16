using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Actor
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public Movie Movie { get; set; }
        public Person Person { get; set; }
        public String CharacterName { get; set; }
        public int Order { get; set; }
    }
}
