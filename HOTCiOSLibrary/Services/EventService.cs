using HOTCAPILibrary.DTOs;
using HOTCiOSLibrary.Models;
using HOTCLibrary.Logic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace HOTCiOSLibrary.Services
{
    public class EventService
    {
        public HttpClient _client { get; set; }

        public EventService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Event> CreateNewEvent(Event userEvent)
        {
            var EventJson = JsonConvert.SerializeObject(userEvent);
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("events/", content).Result;
            HttpContent responseContent = response.Content;
            string result = await responseContent.ReadAsStringAsync();
            Event EventLocation = JsonConvert.DeserializeObject<Event>(result);
            return EventLocation;

        }

        public async Task<Event> CreateNewEvent(Event userEvent, UIImage userImage)
        {
            var IC = new ImageConverter();
            userEvent.Picture = IC.ConvertImageToBytes(userImage);
            
            var EventJson = JsonConvert.SerializeObject(userEvent);
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("events/", content).Result;
            HttpContent responseContent = response.Content;
            string result = await responseContent.ReadAsStringAsync();
            Event EventLocation = JsonConvert.DeserializeObject<Event>(result);
            //AttachUserPicture(userImage, EventLocation.EventID);
            return EventLocation;

        }
    }
}