using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using CoreLocation;
using UIKit;
using MapKit;
using System.Net.Http;
using HOTCiOSLibrary.Services;
using HOTCAPILibrary.DTOs;
using HOTCiOSLibrary.Models;

namespace HOTCiOSLibrary
{
    public class MapService
    {
        public readonly HttpClient _client;

        public MapService(HttpClient client)
        {
            _client = client;
        }

        public MKMapView GetMapView ()
        {
            var map = new MKMapView(UIScreen.MainScreen.Bounds);
            map.ShowsUserLocation = true;
            return map;
        }

        public void CenterToCurrentLocation(MKMapView map, LocationService LS)
        {
            var LM = LS._locationManager;
            var target = new CLLocationCoordinate2D
                (LM.Location.Coordinate.Latitude,
                LM.Location.Coordinate.Longitude);
            var currentregion = MKCoordinateRegion.FromDistance(target, 15000, 15000);
            map.SetRegion(currentregion, animated: false);
        }

        public void ZoomToCurrentLocation(MKMapView map, LocationService LS)
        {
            var LM = LS._locationManager;
            var target = new CLLocationCoordinate2D(LM.Location.Coordinate.Latitude, LM.Location.Coordinate.Longitude);
            var currentregion = MKCoordinateRegion.FromDistance(target, 15000, 15000);
            map.SetRegion(currentregion, animated: true);

        }

        public void CenterToCurrentLocation(MKMapView map, LocationDTO eventLocation)
        {
            var target = new CLLocationCoordinate2D(eventLocation.Lat, eventLocation.Long);
            var currentregion = MKCoordinateRegion.FromDistance(target, 9000, 9000);
            map.SetRegion(currentregion, animated: false);
        }

        public void CenterToCurrentLocation(MKMapView map, Event currentEvent)
        {
            var target = new CLLocationCoordinate2D(currentEvent.Lat, currentEvent.Long);
            var currentregion = MKCoordinateRegion.FromDistance(target, 9000, 9000);
            map.SetRegion(currentregion, animated: false);
        }
    }
}
