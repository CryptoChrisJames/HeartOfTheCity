using Foundation;
using HOTCiOSLibrary.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using CoreLocation;
using HOTCiOSLibrary.Services;
using HOTCAPILibrary.DTOs;
using System.Threading.Tasks;

namespace HeartOfTheCityiOS
{
    public partial class CreateEvent_ : UIViewController
    {
        public Event _newEvent { get; set; }
        public HttpClient _client { get; set; }

        public CreateEvent_ (HttpClient client) : base ()
        {
            _client = client;
            _newEvent = new Event();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Starting off by initilizing all of the components of the view. 

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            var scrollView = new UIScrollView(new CGRect(0, 0, View.Frame.Width, View.Frame.Height));

            var NameField = new UITextField
            {
                Frame = new CGRect(25, 20, 35, 15),
                Placeholder = "Event Name"
            };
            var AddressField = new UITextField
            {
                Frame = new CGRect(25, 30, 35, 15),
                Placeholder = "Address"
            };
            var CityFiled = new UITextField
            {
                Frame = new CGRect(25, 40, 35, 15),
                Placeholder = "City"
            };
            var ZipField = new UITextField
            {
                Frame = new CGRect(25, 50, 35, 15),
                Placeholder = "ZipCode"
            };
            var SubmitButton = new UIButton(UIButtonType.System)
            {
                Frame = new CGRect(25, 70, 300, 30)
            };

            //UIPickerView DatePicker = new UIPickerView(new CGRect(
            //        UIScreen.MainScreen.Bounds.X - UIScreen.MainScreen.Bounds.Width,
            //        UIScreen.MainScreen.Bounds.Height - 230,
            //        UIScreen.MainScreen.Bounds.Width,
            //        180
            //        ));
            UIDatePicker DatePicker = new UIDatePicker(new CGRect(
                    UIScreen.MainScreen.Bounds.X - UIScreen.MainScreen.Bounds.Width,
                    UIScreen.MainScreen.Bounds.Height - 230,
                    UIScreen.MainScreen.Bounds.Width,
                    180));
            var calendar = new NSCalendar(NSCalendarType.Gregorian);
            calendar.TimeZone = NSTimeZone.LocalTimeZone;
            var currentDate = NSDate.Now;
            var components = new NSDateComponents();
            components.Year = -60;
            NSDate minDate = calendar.DateByAddingComponents(components, NSDate.Now, NSCalendarOptions.None);
            DatePicker.MinimumDate = currentDate;
            DatePicker.Mode = UIDatePickerMode.DateAndTime;
            

            SubmitButton.SetTitle("Submit Event", UIControlState.Normal);

            //Start putting the components together to build the view.

            stackView.AddArrangedSubview(NameField);
            stackView.AddArrangedSubview(AddressField);
            stackView.AddArrangedSubview(CityFiled);
            stackView.AddArrangedSubview(ZipField);
            stackView.AddArrangedSubview(DatePicker);
            stackView.AddArrangedSubview(SubmitButton);

            scrollView.AddSubview(stackView);

            View.Add(scrollView);

            //Finializing the layout.

            scrollView.ContentSize = stackView.Frame.Size;
            scrollView.AddConstraint(stackView.TopAnchor.ConstraintEqualTo(scrollView.TopAnchor));
            scrollView.AddConstraint(stackView.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor));
            scrollView.AddConstraint(stackView.LeftAnchor.ConstraintEqualTo(scrollView.LeftAnchor));
            scrollView.AddConstraint(stackView.RightAnchor.ConstraintEqualTo(scrollView.RightAnchor));

            View.AddConstraint(scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor));
            View.AddConstraint(scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor));
            View.AddConstraint(scrollView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor));
            View.AddConstraint(scrollView.RightAnchor.ConstraintEqualTo(View.RightAnchor));
            View.BackgroundColor = UIColor.White;
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(g);

            //Button function #1: submitting the event and presenting the submission to the user.
            SubmitButton.TouchUpInside += async (object sender, EventArgs e) =>
            {
                //Populate the Event model. 
                Event userEvent = new Event();
                userEvent.Address = AddressField.Text;
                userEvent.EventName = NameField.Text;
                userEvent.City = CityFiled.Text;
                //userEvent.State
                //userEvent.Country
                userEvent.ZipCode = int.Parse(ZipField.Text);
                userEvent.DateOfEvent = (DateTime)DatePicker.Date;
                var geoCoder = new CLGeocoder();
                var location = new CLLocation();
                string worldAddress = userEvent.Address + ", " + userEvent.City + ", ";
                var placemarks = geoCoder.GeocodeAddressAsync(userEvent.Address);
                await placemarks.ContinueWith((addresses) =>
                {
                    foreach (var address in addresses.Result)
                    {
                        location = address.Location;
                    }
                });
                userEvent.Lat = location.Coordinate.Latitude;
                userEvent.Long = location.Coordinate.Longitude;
                EventService ES = new EventService(_client);
                LocationDTO EventLocation = await ES.CreateNewEvent(userEvent);
                
            };
        }
    }
}