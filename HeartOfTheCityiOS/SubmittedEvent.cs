using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using HOTCAPILibrary.DTOs;
using System.Net.Http;
using HOTCiOSLibrary;
using HOTCiOSLibrary.Services;
using MapKit;
using CoreLocation;
using CoreGraphics;
using HOTCLibrary.Logic;
using HOTCiOSLibrary.Models;
using HOTCLibrary.Logic.Annotation;

namespace HeartOfTheCityiOS
{
    [Register("SubmittedEvent")]
    public class SubmittedEvent : UIViewController
    {
        public Event _eventLocation;
        public HttpClient _client;
        public CLLocationManager _locationmanager;

        public SubmittedEvent(Event EventLocation, HttpClient client, CLLocationManager LM)
        {
            _client = client;
            _eventLocation = EventLocation;
            _locationmanager = LM;
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.SetHidesBackButton(true, false);
            MapService MapService = new MapService(_client);
            LocationService locationService = new LocationService();
            locationService.CurrentLocation(_locationmanager);
            var map = MapService.GetMapView();
            MapService.CenterToCurrentLocation(map, _eventLocation);
            map.AddAnnotations(new HOTCAnnotation(_eventLocation));
            var CancelButton = new UIButton(UIButtonType.System);
            CancelButton.Frame = new CGRect(25, 500, 300, 150);
            CancelButton.SetTitle("Cancel", UIControlState.Normal);
            View.AddSubview(map);
            View.AddSubview(CancelButton);

            CancelButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                this.NavigationController.PopViewController(true);
            };
        }
    }
}