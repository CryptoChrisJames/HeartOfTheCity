using Foundation;
using HOTCiOSLibrary.Models;
using System;
using System.Net.Http;
using UIKit;
using CoreGraphics;

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

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillProportionally,
                Spacing = 2,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            //View.Add(stackView);
            View.AddConstraint(stackView.TopAnchor.ConstraintEqualTo(View.TopAnchor));
            View.AddConstraint(stackView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor));
            View.AddConstraint(stackView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor));
            View.AddConstraint(stackView.RightAnchor.ConstraintEqualTo(View.RightAnchor));

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

            stackView.AddArrangedSubview(NameField);
            stackView.AddArrangedSubview(AddressField);
            stackView.AddArrangedSubview(CityFiled);
            stackView.AddArrangedSubview(ZipField);
            stackView.AddArrangedSubview(SubmitButton);
            //Add views to UI
            View.BackgroundColor = UIColor.White;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(g);
            
            var scrollView = new UIScrollView(new CGRect(0, 0, View.Frame.Width, View.Frame.Height));
            scrollView.ContentSize = stackView.Frame.Size;
            scrollView.AddSubview(stackView);
            View.Add(scrollView);
            //View.AddSubview(NameField);
            //View.AddSubview(AddressField);
            //View.AddSubview(CityFiled);
            //View.AddSubview(ZipField);
            //View.AddSubview(SubmitButton);
        }
    }
}