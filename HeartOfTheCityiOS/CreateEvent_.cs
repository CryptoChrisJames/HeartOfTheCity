using Foundation;
using HOTCiOSLibrary.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using HOTCiOSLibrary.Services;
using HOTCAPILibrary.DTOs;

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
            SubmitButton.SetTitle("Submit Event", UIControlState.Normal);

            //Start putting the components together to build the view.

            stackView.AddArrangedSubview(NameField);
            stackView.AddArrangedSubview(AddressField);
            stackView.AddArrangedSubview(CityFiled);
            stackView.AddArrangedSubview(ZipField);
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

            //Button actions. 
            SubmitButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                Event userEvent = new Event();
                userEvent.Address = AddressField.Text;
                userEvent.EventName = NameField.Text;
                userEvent.City = CityFiled.Text;
                userEvent.ZipCode = int.Parse(ZipField.Text);

                EventService ES = new EventService(_client);
                LocationDTO EventLocation = ES.CreateNewEvent(userEvent);
                
            };
        }
    }
}