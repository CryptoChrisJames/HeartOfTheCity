using CoreLocation;
using HOTCAPILibrary.DTOs;
using HOTCiOSLibrary.Models;
using HOTCLibrary.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace HOTCiOSLibrary.Services
{
    public class EventService
    {
        public HttpClient _client { get; set; }
        private JsonSerializer _serializer { get; set; }

        public EventService(HttpClient client)
        {
            _client = client;
            _serializer = new JsonSerializer();
        }

        public async Task<Event> CreateNewEvent(Event userEvent)
        {
            var EventJson = JsonConvert.SerializeObject(userEvent);
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("events/", content);
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
            var response = await _client.PostAsync("events/", content);
            HttpContent responseContent = response.Content;
            string result = await responseContent.ReadAsStringAsync();
            Event EventLocation = JsonConvert.DeserializeObject<Event>(result);
            //AttachUserPicture(userImage, EventLocation.EventID);
            return EventLocation;
        }

        public async Task<List<Event>> GetLocalEvents(CLLocation currentLocation)
        {
            var geocoder = new CLGeocoder();
            string Local = null;
            var placemarks = await geocoder.ReverseGeocodeLocationAsync(currentLocation);
            foreach(var placemark in placemarks)
            {
                Local = placemark.Locality;
            }
            var response = await _client.GetAsync("events/" + Local);
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {

                return _serializer.Deserialize<List<Event>>(json);
            }
            
        }
    }
}