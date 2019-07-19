using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SimpleDto>> Get()
        {
            return new SimpleDto[] { new SimpleDto { Text = "value1" }, new SimpleDto { Text = "value2" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<SimpleDto> Get(int id)
        {
            return new SimpleDto { Text = "value1" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] SimpleDto value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SimpleDto value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("{id}")]
        public ActionResult<IDictionary<string, SimpleDto>> BigPost(int id, BigDto bigDto)
        {
            throw new NotImplementedException();
        }
    }
}
