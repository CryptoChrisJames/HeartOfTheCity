using Foundation;
using System;
using UIKit;
using HOTCiOSLibrary;
using RestSharp;
using HOTCiOSLibrary.Services;
using MapKit;
using System.Net.Http;
using CoreGraphics;

namespace HeartOfTheCityiOS
{
    public partial class MapViewController : UIViewController
    {
        private MapService _mapservice { get; set; }
        private LocationService _locationservice { get; set; }
        private MKMapView _map { get; set; }
        public HttpClient _client { get; set; }

        public MapViewController (HttpClient client) : base ()
        {
            _client = client;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            //Services
            MapService MapService = new MapService(_client);
            LocationService locationService = new LocationService(new CoreLocation.CLLocationManager());
            locationService.CurrentLocation(locationService.LocationManager);
            _mapservice = MapService;
            _locationservice = locationService;
            var map = _mapservice.GetMapView();
            _map = map;
            MapService.CenterToCurrentLocation(_map, _locationservice);
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
                _mapservice.ZoomToCurrentLocation(_map, _locationservice);
            };

            CreateEvent.TouchUpInside += (object sender, EventArgs e) =>
            {
                var CreateEventController = new CreateEvent_(_client);
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
            _mapservice.ZoomToCurrentLocation(_map, _locationservice);
        }
    }
}