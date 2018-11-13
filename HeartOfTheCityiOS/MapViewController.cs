using Foundation;
using System;
using UIKit;
using HOTCiOSLibrary;
using RestSharp;
using HOTCiOSLibrary.Services;
using MapKit;
using System.Net.Http;
using CoreGraphics;
using CoreLocation;
using System.Collections.Generic;
using HOTCiOSLibrary.Models;
using HOTCAPILibrary.DTOs;

namespace HeartOfTheCityiOS
{
    public partial class MapViewController : UIViewController
    {
        private MapService _mapservice { get; set; }
        private LocationService _locationservice { get; set; }
        public CLLocationManager _locationManager { get; set; }
        private MKMapView _map { get; set; }
        public HttpClient _client { get; set; }
        private EventService _eventservice { get; set; }
        public List<Event> _localEvents { get; set; }

        public MapViewController (HttpClient client, CLLocationManager locationManager) : base ()
        {
            _client = client;
            _locationservice = new LocationService();
            _locationManager = locationManager;
            _mapservice = new MapService(_client);
            _eventservice = new EventService(_client);
            _map = _mapservice.GetMapView();
            LocationDTO currentLocation = new LocationDTO()
            {
                Lat = _locationManager.Location.Coordinate.Latitude,
                Long = _locationManager.Location.Coordinate.Longitude
            };
            _localEvents = _eventservice.GetLocalEvents(currentLocation);
            
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad();
            if (CLLocationManager.LocationServicesEnabled)
            {
                _locationservice.CurrentLocation(_locationManager);
                _mapservice.ZoomToCurrentLocation(_map, _locationManager);
            }
            View.AddSubview(_map);

            //Create UI
            var LocationButton = new UIButton(UIButtonType.System);
            LocationButton.Frame = new CGRect(25, 25, 300, 150);
            LocationButton.SetTitle("Location", UIControlState.Normal);

            var CreateEvent = new UIButton(UIButtonType.System);
            CreateEvent.Frame = new CGRect(25, 500, 300, 150);
            CreateEvent.SetTitle("Create", UIControlState.Normal);

            LocationButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                _mapservice.ZoomToCurrentLocation(_map, _locationManager);
            };

            CreateEvent.TouchUpInside += (object sender, EventArgs e) =>
            {
                var CreateEventController = new CreateEvent_(_client, _locationManager);
                this.NavigationController.PushViewController(CreateEventController, true);
            };

            View.AddSubview(LocationButton);
            View.AddSubview(CreateEvent);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void LocationButton_TouchUpInside(UIButton sender)
        {
            _mapservice.ZoomToCurrentLocation(_map, _locationManager);
        }
    }
}