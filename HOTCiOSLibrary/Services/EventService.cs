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
using System.Threading.Tasks;
using HOTCLibrary.Logic;

namespace HOTCiOSLibrary.Services
{
    public class EventService
    {
        public HttpClient _client { get; set; }

        public EventService(HttpClient client)
        {
            _client = client;
        }

        public async Task<LocationDTO> CreateNewEvent(Event userEvent)
        {
            var EventJson = JsonConvert.SerializeObject(userEvent);
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("events/", content).Result;
            HttpContent responseContent = response.Content;
            string result = await responseContent.ReadAsStringAsync();
            LocationDTO EventLocation = JsonConvert.DeserializeObject<LocationDTO>(result);
            return EventLocation;

        }

        public async Task<LocationDTO> CreateNewEvent(Event userEvent, UIImage userImage)
        {
            var EventJson = JsonConvert.SerializeObject(userEvent);
            var content = new StringContent(EventJson, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("events/", content).Result;
            HttpContent responseContent = response.Content;
            string result = await responseContent.ReadAsStringAsync();
            LocationDTO EventLocation = JsonConvert.DeserializeObject<LocationDTO>(result);
            AttachUserPicture(userImage, EventLocation.EventID);
            return EventLocation;

        }

        private void AttachUserPicture(UIImage userImage, int EventID)
        {
            var IC = new ImageConverter();
            var ImageByteArray = IC.ConvertImageToBytes(userImage);
            var requestContent = new MultipartFormDataContent();
            ByteArrayContent content = new ByteArrayContent(ImageByteArray);
            requestContent.Add(content);
            var result = _client.PostAsync("events/image/" + EventID, content);

        }
    }
}