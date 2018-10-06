using Foundation;
using System;
using UIKit;
using HOTCLibrary;
using RestSharp;
using HOTCLibrary.Services;
using MapKit;
using System.Net.Http;

namespace HeartOfTheCityiOS
{
    public partial class MapViewController : UIViewController
    {
        private MapService _mapservice { get; set; }
        private LocationService _locationservice { get; set; }
        private MKMapView _map { get; set; }
        public HttpClient _client { get; set; }
        public UINavigationController _navigationcontroller { get; set; }

        public MapViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            UINavigationController navContr = new UINavigationController();
            _navigationcontroller = navContr;
            var client = new HttpClient();
            _client = client;
            MapService MapService = new MapService(_client);
            LocationService locationService = new LocationService(new CoreLocation.CLLocationManager());
            locationService.CurrentLocation(locationService.LocationManager);
            _mapservice = MapService;
            _locationservice = locationService;
            var map = _mapservice.GetMapView();
            _map = map;
            MapService.CenterToCurrentLocation(_map, _locationservice);
            View.AddSubview(_map);
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