using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get
            {
                return FirstName + " " + LastName;
            } 
        }
        public string Biography { get; set; }
        public string Picture { get; set; }
        public byte[] PictureFile { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
    }
}
