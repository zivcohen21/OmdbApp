using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using omdbApp.Models;
using System.Net;


namespace omdbApp.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        OmdbDataAccessLayer omdbData = new OmdbDataAccessLayer();

        // POST: api/Search
        [HttpPost]
        public Results Post([FromBody] Search value)
        {
            return omdbData.search(value);
        }
    }
}
