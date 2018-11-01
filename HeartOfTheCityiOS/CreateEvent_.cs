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
using System.Linq;
using System.Runtime.InteropServices;

namespace HeartOfTheCityiOS
{
    public partial class CreateEvent_ : UIViewController
    {
        public Event userEvent { get; set; }
        public HttpClient _client { get; set; }
        public UIImage userImage {get;set;}

        UIImagePickerController galleryImagePicker;
        UIImagePickerController cameraImagePicker;

        public CreateEvent_ (HttpClient client) : base ()
        {
            _client = client;
            userEvent = new Event();
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
                Frame = new CGRect(25, 90, 300, 30)
            };
            var GetImage = new UIButton(UIButtonType.System)
            {
                Frame = new CGRect(25, 60, 300, 30)
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
            GetImage.SetTitle("Pick An Image", UIControlState.Normal);
            

            //Start putting the components together to build the view.

            stackView.AddArrangedSubview(NameField);
            stackView.AddArrangedSubview(AddressField);
            stackView.AddArrangedSubview(CityFiled);
            stackView.AddArrangedSubview(ZipField);
            stackView.AddArrangedSubview(DatePicker);
            stackView.AddArrangedSubview(GetImage);
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
                userEvent.Address = AddressField.Text;
                userEvent.EventName = NameField.Text;
                userEvent.City = CityFiled.Text;
                //userEvent.State
                //userEvent.Country
                userEvent.ZipCode = int.Parse(ZipField.Text);
                DateTime.SpecifyKind((DateTime)DatePicker.Date, DateTimeKind.Utc).ToLocalTime();
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
                if(userImage != null)
                {
                    LocationDTO EventLocation = await ES.CreateNewEvent(userEvent, userImage);
                }
                else
                {
                    LocationDTO EventLocation = await ES.CreateNewEvent(userEvent);
                }
                
            };

            //Button function #2: opening Image Picker
            GetImage.TouchUpInside += (object sender, EventArgs e) =>
            {
                ShowSelectPicPopup();
            };
            
        }

        void ShowSelectPicPopup()
        {
            var actionSheetAlert = UIAlertController.Create("Select picture",
                                                       "Complete action using", UIAlertControllerStyle.ActionSheet);
            actionSheetAlert.AddAction(UIAlertAction.Create("Gallery",
                                              UIAlertActionStyle.Default, (action) => HandleGalleryButtonClick()));
            actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel,
                                              (action) => Console.WriteLine("Cancel button pressed.")));
            // Required for iPad - You must specify a source for the Action Sheet since it is
            // displayed as a popover
            var presentationPopover = actionSheetAlert.PopoverPresentationController;
            if (presentationPopover != null)
            {
                presentationPopover.SourceView = View;
                presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
            }

            PresentViewController(actionSheetAlert, true, null);
        }

        void HandleGalleryButtonClick()
        {
            if (galleryImagePicker == null)
            {
                galleryImagePicker = new UIImagePickerController();
                galleryImagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                galleryImagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
                galleryImagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
                galleryImagePicker.Canceled += Handle_Canceled;
            }
            PresentViewController(galleryImagePicker, true, () => { });
        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            galleryImagePicker.DismissViewController(true, () => { });
        }

        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            var referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL);

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                var originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    userImage = originalImage;
                }
            }
            else
            { // if it's a video
              // get video url
                var mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL);
                }
            }
            // dismiss the picker
            galleryImagePicker.DismissViewController(true, () => { });
        }

        void Handle_CameraCanceled(object sender, EventArgs e)
        {
            cameraImagePicker.DismissViewController(true, () => { });
        }

    }
}