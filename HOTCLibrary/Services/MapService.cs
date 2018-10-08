﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using CoreLocation;
using UIKit;
using MapKit;
using System.Net.Http;

namespace HOTCLibrary
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

        public void CenterToCurrentLocation(MKMapView map, Services.LocationService LS)
        {
            var LM = LS.LocationManager;
            var target = new CLLocationCoordinate2D
                (LM.Location.Coordinate.Latitude,
                LM.Location.Coordinate.Longitude);
            var currentregion = MKCoordinateRegion.FromDistance(target, 15000, 15000);
            map.SetRegion(currentregion, animated: false);
        }

        public void ZoomToCurrentLocation(MKMapView map, Services.LocationService LS)
        {
            var LM = LS.LocationManager;
            var target = new CLLocationCoordinate2D(LM.Location.Coordinate.Latitude, LM.Location.Coordinate.Longitude);
            var currentregion = MKCoordinateRegion.FromDistance(target, 15000, 15000);
            map.SetRegion(currentregion, animated: true);

        }

    }
}