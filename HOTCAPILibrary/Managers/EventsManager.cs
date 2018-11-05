using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOTCAPILibrary.Data;
using HOTCAPILibrary.DTOs;
using HOTCAPILibrary;
using HOTCAPILibrary.Models;

namespace HOTCAPILibrary.Managers
{
    public class EventsManager
    {
        private readonly ApplicationDbContext _context;

        public EventsManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LocationDTO> CreateNewEvent(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            var location = new LocationDTO();
            location.Lat = newEvent.Lat;
            location.Long = newEvent.Long;
            location.EventID = newEvent.ID;
            return location;
        }

        public void AttachImage(byte[] byteArray, int eventID)
        {
            Event currentEvent = new Event();
            currentEvent = _context.Events.Where(e => e.ID == eventID).FirstOrDefault();
            currentEvent.Picture = byteArray;
            _context.Events.Update(currentEvent);
            _context.SaveChanges();
        }
    }
}
