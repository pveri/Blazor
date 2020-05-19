using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.Shared.DTO
{
    public class PersonDTO
    {
        public PersonDTO()
        {

        }
        public PersonDTO(Person person)
        {
            this.Id = person.Id;
            this.LastName = person.LastName;
            this.FirstName = person.FirstName;
            this.Biography = person.Biography;
            this.Picture = person.Picture;
            this.BirthDate = person.BirthDate;
        }
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
        public DateTime? BirthDate { get; set; }
        public string Character { get; set; }
        public Person Person()
        {
            return new Person
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                Biography = this.Biography,
                Picture = this.Picture,
                BirthDate = this.BirthDate
            };
        }
    }
}
