using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using UIKit;

namespace HOTCiOSLibrary.Services
{
    public class LocationService
    {
        public void CurrentLocation(CLLocationManager locationManager)
        {
            locationManager.StartUpdatingLocation();
        }
    }
}