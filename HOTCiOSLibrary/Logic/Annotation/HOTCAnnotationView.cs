using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using UIKit;

namespace HOTCLibrary.Logic.Annotation
{
    public class HOTCAnnotationView : MKAnnotationView
    {
        public HOTCAnnotation _annotation { get; set; }
        public string _annotationID { get; set; }

        public HOTCAnnotationView(IMKAnnotation annotation, string AnnotationID)
        {
            _annotation = (HOTCAnnotation)annotation;
            _annotationID = AnnotationID;
            CanShowCallout = true; 
            Image = UIImage.FromFile("heart.png");
        }
    }
}