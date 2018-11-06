using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MapKit;
using CoreGraphics;

namespace HOTCLibrary.Logic
{
    public class HOTCAnnotationView : MKAnnotationView
    {
        public UIView customCalloutView;
        public override bool CanShowCallout { get => false; set => base.CanShowCallout = false; }

        public override void SetSelected(bool selected, bool animated)
        {
            base.SetSelected(selected, animated);

            if(selected)
            {
                this.RemoveFromSuperview();
                var newCustomCalloutView = loadEventDetailMapView();
                this.AddSubview(newCustomCalloutView);
                this.customCalloutView = newCustomCalloutView;
            }
            else
            {
                if (customCalloutView != null)
                {
                    if(animated)
                    {
                        UIView.Animate(1.5, () =>
                        {
                            this.customCalloutView.Alpha = 0;
                        },
                        () => 
                        {
                            this.customCalloutView.RemoveFromSuperview();
                        });
                    }
                    else
                    {
                        this.customCalloutView.RemoveFromSuperview();
                    }
                }
            }
        }

        private UIView loadEventDetailMapView()
        {
            var EventDetailMapView = new UIView(new CGRect(0,0,240,280));
            return EventDetailMapView;
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            this.customCalloutView.RemoveFromSuperview();
        }

        public override IMKAnnotation Annotation { get => base.Annotation; set =>  this.customCalloutView.RemoveFromSuperview(); }
    }
}