using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorMovies.Library;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorMovies.Server.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        readonly MoviesDbContext dbContext;
        readonly IFileStorageService fileService;
        readonly IMapper mapper;
        public PeopleController(MoviesDbContext dbContext, IFileStorageService fileService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
            this.mapper = mapper;
        }
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet("search/{searchText}")]
        public async Task<ActionResult> FilterByName(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return Ok(new List<Person>());
            }
            var result = await dbContext.People.Where(x => x.FirstName.Contains(searchText))
                .Take(5)
                .ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Post (Person person)
        {
            // Todo: Move to manager method
            if (!String.IsNullOrEmpty(person.Picture)) {
                var pictureBytes = Convert.FromBase64String(person.Picture);
                person.Picture = await fileService.SaveFile(pictureBytes, ".jpg", "people");
            }
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        // GET api/<controller>/5
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get([FromQuery]PaginationDTO dto)
        {
            var people = dbContext.People.AsQueryable();
            await HttpContext.AddPagesResponse(people, dto.Records);
            
            return Ok(await people.Paginate(dto).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(x => x.Id == id);
            return person;
        }
        // POST api/<controller>
        //[HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }
        [HttpPut]
        public async Task<ActionResult> Put(Person person)
        {
            var dbPerson =await dbContext.People.FirstOrDefaultAsync(x => x.Id == person.Id);
            dbPerson = mapper.Map(person, dbPerson);

            if (!string.IsNullOrEmpty(person.Picture))
            {
                // TODO
                // Big no-no for me! Will keep it like this for now to finish course, but will move out of controller later
                var newPic = await fileService.EditFile(Convert.FromBase64String(person.Picture), ".jpg", "people", dbPerson.Picture);
                dbPerson.Picture = newPic;
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await dbContext.People.FindAsync(id);
            if (person!=null)
            {
                dbContext.People.Remove(person);
                await dbContext.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
