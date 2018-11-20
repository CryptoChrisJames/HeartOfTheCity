using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MapKit;
using HOTCiOSLibrary.Models;
using CoreLocation;

namespace HOTCLibrary.Logic.Annotation
{
    public class HOTCAnnotation : MKAnnotation
    {
        public HOTCAnnotation(Event currentEvent)
        {
            _currentEvent = currentEvent;
        }

        public Event _currentEvent
        {
            get; private set;
        }

        public override CLLocationCoordinate2D Coordinate => new CLLocationCoordinate2D(_currentEvent.Lat, _currentEvent.Long);
        public override string Title => _currentEvent.EventName;
        public override string Subtitle => _currentEvent.Description;
    }
}