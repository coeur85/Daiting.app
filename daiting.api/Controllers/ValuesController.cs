using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using daiting.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using daiting.Data;

namespace daiting.api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public DataContext _Context { get; }

        public ValuesController(DataContext context)
        {
            this._Context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // throw new Exception("magdi error 1");
            //return new string[] { "value1", "value2" };
            var list = await _Context.Values.ToListAsync();
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async  Task<IActionResult> Get(int id)
        {
            //return "value";
            var value = await _Context.Values.FirstOrDefaultAsync(x=> x.ID == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
