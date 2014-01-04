using ProjectREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectREST.Controllers
{
    public class DateController : ApiController
    {
        // GET api/date
        public IEnumerable<DateTime> Get()
        {
            return FestivalDA.GetDates();
        }

        // GET api/date/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/date
        public void Post([FromBody]string value)
        {
        }

        // PUT api/date/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/date/5
        public void Delete(int id)
        {
        }
    }
}
