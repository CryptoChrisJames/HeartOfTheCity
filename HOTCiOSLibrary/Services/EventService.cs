using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Foundation;
using HOTCiOSLibrary.Models;
using UIKit;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using HOTCAPILibrary.DTOs;

namespace HOTCiOSLibrary.Services
{
    public class EventService
    {
        public HttpClient _client { get; set; }

        public EventService(HttpClient client)
        {
            _client = client;
        }

        public LocationDTO CreateNewEvent(Event userEvent)
        {
            var EventJson = JsonConvert.SerializeObject(userEvent);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var respone = _client.PostAsync("events/", content);
            LocationDTO EventLocation = new LocationDTO();
            return EventLocation;

        }
    }
}