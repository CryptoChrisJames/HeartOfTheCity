using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using System.Net.Http;
using CoreLocation;

namespace HeartOfTheCityiOS
{
    [Register("LoginPage")]
    public class LoginPage : UIViewController
    {
        public HttpClient _client { get; set; }
        public CLLocationManager _locationManager { get; set; }

        public LoginPage(HttpClient client, CLLocationManager locationManager)
        {
            _client = client;
            _locationManager = locationManager;
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _locationManager.PausesLocationUpdatesAutomatically = false;

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                _locationManager.RequestAlwaysAuthorization();
                _locationManager.RequestWhenInUseAuthorization();
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                _locationManager.AllowsBackgroundLocationUpdates = true;
            }
            UIButton ContinueButton = new UIButton(UIButtonType.System)
            {
                Frame = new CoreGraphics.CGRect(75, 323, 450, 200)
            };
            ContinueButton.SetTitle("Stand-In Login Page, press to Continue.", UIControlState.Normal);
            View.BackgroundColor = UIColor.White;

            ContinueButton.TouchUpInside += (object sender, EventArgs e) => 
            {
                var MapController = new MapViewController(_client, _locationManager);
                this.NavigationController.PushViewController(MapController, true);
            };

            View.AddSubview(ContinueButton);
        }
    }
}