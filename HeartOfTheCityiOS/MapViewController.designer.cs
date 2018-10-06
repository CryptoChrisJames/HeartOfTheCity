// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HeartOfTheCityiOS
{
    [Register ("ViewController")]
    partial class MapViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CreateEvent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LocationButton { get; set; }

        [Action ("CreateEvent_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CreateEvent_TouchUpInside (UIKit.UIButton sender);

        [Action ("LocationButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LocationButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CreateEvent != null) {
                CreateEvent.Dispose ();
                CreateEvent = null;
            }

            if (LocationButton != null) {
                LocationButton.Dispose ();
                LocationButton = null;
            }
        }
    }
}