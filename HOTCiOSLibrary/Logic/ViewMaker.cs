using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HOTCiOSLibrary.Models;
using UIKit;

namespace HOTCLibrary.Logic
{
    public class ViewMaker
    {
        public UIView _view { get; set; }
        public UIStackView _stackView { get; set; }

        public ViewMaker()
        {
            //Initialize main components
            _view = new UIView();
            _stackView = new UIStackView();
            _view.AddSubview(_stackView);

            //Set view contraints
            _view.AddConstraint(_stackView.TopAnchor.ConstraintEqualTo(_view.TopAnchor));
            _view.AddConstraint(_stackView.BottomAnchor.ConstraintEqualTo(_view.BottomAnchor));
            _view.AddConstraint(_stackView.LeftAnchor.ConstraintEqualTo(_view.LeftAnchor));
            _view.AddConstraint(_stackView.RightAnchor.ConstraintEqualTo(_view.RightAnchor));
        }
        public UIView GetCalloutDetails(Event currentEvent)
        {
            UILabel Address = new UILabel()
            {
                Text = currentEvent.Address,
                TextAlignment = UITextAlignment.Center
            };
            _stackView.AddArrangedSubview(Address);
            return _view;
        }
    }
}