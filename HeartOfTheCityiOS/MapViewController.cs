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
using HOTCLibrary.Logic.Annotation;
using HOTCLibrary.Logic;

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
        public List<Event> _Events { get; set; }
        public CLLocation _currentLocation { get; set; }

        public MapViewController (HttpClient client, CLLocationManager locationManager) : base ()
        {
            _client = client;
            _locationservice = new LocationService();
            _locationManager = locationManager;
            _mapservice = new MapService(_client);
            _eventservice = new EventService(_client);
            _map = _mapservice.GetMapView();
            _Events = new List<Event>();
            _currentLocation = new CLLocation
                (_locationManager.Location.Coordinate.Latitude,
                _locationManager.Location.Coordinate.Longitude);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad();
            this.NavigationItem.SetHidesBackButton(true, false);
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

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            if(_Events.Count == 0 )
            {
                _Events = await _eventservice.GetLocalEvents(_currentLocation);
            }

            foreach(Event currentEvent in _Events)
            {
                _map.AddAnnotation(new HOTCAnnotation(currentEvent));
            }
            _map.GetViewForAnnotation += GetAnnotationView;
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
        
        public MKAnnotationView GetAnnotationView (MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation("AnnocationID");

            if (annotationView == null)
            {
                annotationView = new HOTCAnnotationView(annotation, "AnnotationID");
            }

            annotationView.Annotation = (MKAnnotation)annotation;

            return annotationView;
        }
    }
}