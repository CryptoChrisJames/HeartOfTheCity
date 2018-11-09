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

namespace HeartOfTheCityiOS
{
    [Register("SubmittedEvent")]
    public class SubmittedEvent : UIViewController
    {
        Event _eventLocation;
        HttpClient _client;
        HOTCMapDelegate _hOTCMapDelegate;

        public SubmittedEvent(Event EventLocation, HttpClient client)
        {
            _client = client;
            _eventLocation = EventLocation;
            _hOTCMapDelegate = new HOTCMapDelegate();
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
            map.AddAnnotations(new HOTCAnnotation(_eventLocation));
            var CancelButton = new UIButton(UIButtonType.System);
            CancelButton.Frame = new CGRect(25, 500, 300, 150);
            CancelButton.SetTitle("Cancel", UIControlState.Normal);
            map.Delegate = _hOTCMapDelegate;
            View.AddSubview(map);
            View.AddSubview(CancelButton);

            CancelButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                this.NavigationController.PopViewController(true);
            };


        }
        public class HOTCMapDelegate : MKMapViewDelegate
        {
            public string AnnotationID = "HOTCAnnotation";

            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                MKAnnotationView AnnotationView = null;

                if(annotation is MKUserLocation)
                {
                    return null;
                }
                
                if(annotation is HOTCAnnotation)
                {
                    AnnotationView = mapView.DequeueReusableAnnotation(AnnotationID);

                    if(AnnotationView == null)
                    {
                        AnnotationView = new MKAnnotationView(annotation, AnnotationID);
                        AnnotationView.Image = UIImage.FromFile("Resources/Images/heart.png");
                        AnnotationView.CanShowCallout = true;
                        AnnotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.RoundedRect);
                    }
                }
                return AnnotationView;
            }
        }
    }
}