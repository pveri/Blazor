﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorMovies.Server.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        readonly MoviesDbContext dbContext;
        public PeopleController(MoviesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task<ActionResult> Post (Person person)
        {
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        // GET api/<controller>/5
        [HttpPost("{id}")]
        public string Get(int id)
        {
            return "value";
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
