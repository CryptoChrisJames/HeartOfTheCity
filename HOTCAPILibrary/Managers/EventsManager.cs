using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HOTCAPILibrary.Data;
using HOTCLibrary.Models;

namespace HOTCAPILibrary.Managers
{
    public class EventsManager
    {
        private readonly ApplicationDbContext _context;

        public EventsManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateNewEvent(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            return "ok";
        }
    }
}
