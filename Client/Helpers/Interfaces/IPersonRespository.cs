using BlazorMovies.Client.Pages.People;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Interfaces
{
    public interface IPersonRepository
    {
        public Task CreatePerson(Person person);
        public Task<List<Person>> GetPeople();
        public Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO dto);
        public Task<List<Person>> GetPeopleByName(string name);
        public Task<Person> GetPerson(int id);
        public Task UpdatePerson(Person person);
        public Task DeletePerson(int Id);
    }
}
