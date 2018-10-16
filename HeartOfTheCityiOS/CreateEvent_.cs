using Foundation;
using HOTCiOSLibrary.Models;
using System;
using System.Net.Http;
using UIKit;

namespace HeartOfTheCityiOS
{
    public partial class CreateEvent_ : UIViewController
    {
        public Event _newEvent { get; set; }
        public HttpClient _client { get; set; }

        public CreateEvent_ (HttpClient client) : base ()
        {
            _client = client;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _newEvent = new Event();
        }
    }
}