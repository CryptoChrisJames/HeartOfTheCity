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
        protected CLLocationManager _locationManager;

        public LocationService(CLLocationManager locationManager)
        {
            _locationManager = locationManager;
            _locationManager = new CLLocationManager
            {
                PausesLocationUpdatesAutomatically = false
            };

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                _locationManager.RequestAlwaysAuthorization();
                _locationManager.RequestWhenInUseAuthorization();
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                _locationManager.AllowsBackgroundLocationUpdates = true;
            }


        }

        public void CurrentLocation(CLLocationManager locationManager)
        {
            //locationManager.LocationsUpdated += (sender, e) =>
            //{
            //    foreach (var loc in e.Locations)
            //    {
            //        Console.WriteLine(loc);
            //    }
            //};
            locationManager.StartUpdatingLocation();
        }

        //public CLLocationManager LocationManager
        //{
        //    get { return _locationManager; }
        //}
        
    }
}