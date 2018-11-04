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

namespace HeartOfTheCityiOS
{
    [Register("SubmittedEvent")]
    public class SubmittedEvent : UIViewController
    {
        LocationDTO _eventLocation;
        HttpClient _client;

        public SubmittedEvent(LocationDTO EventLocation, HttpClient client)
        {
            _client = client;
            _eventLocation = EventLocation;
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
            LocationService locationService = new LocationService(new CoreLocation.CLLocationManager());
            locationService.CurrentLocation(locationService._locationManager);
            var map = MapService.GetMapView();
            MapService.CenterToCurrentLocation(map, _eventLocation);
            map.AddAnnotations(new MKPointAnnotation() {
                Title = "Test Title Replace This Shit",
                Coordinate = new CLLocationCoordinate2D(_eventLocation.Lat, _eventLocation.Long)
            });
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