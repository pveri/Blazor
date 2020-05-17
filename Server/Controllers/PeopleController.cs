using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Server.Helpers;
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
        public PeopleController(MoviesDbContext dbContext, IFileStorageService fileService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
        }
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

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
        public async Task<ActionResult> Get()
        {
            var people = await dbContext.People.ToListAsync();
            return Ok(people);
        }

        // POST api/<controller>
        //[HttpPost]
       // public void Post([FromBody]string value)
       // {
       // }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
