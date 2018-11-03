using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HOTCAPILibrary.Data;
using HOTCAPILibrary.DTOs;
using HOTCAPILibrary.Managers;
using HOTCLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http;
using System.Web;

namespace HOTCApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EventsManager _EM;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
            _EM = new EventsManager(_context);

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<LocationDTO> Post([FromBody]Event newEvent)
        {
            var newEventLocation = new LocationDTO();
            newEventLocation = await _EM.CreateNewEvent(newEvent);
            return newEventLocation;
        }

        [HttpPut]
        [Route("image/{EventID}")]
        public void AddImageToEvent([FromBody]byte[] ByteArray, int EventID)
        {
            _EM.AttachImage(ByteArray, EventID);
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
