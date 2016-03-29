using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

namespace ION.IOS.ViewController.Logging
{
  public class LegendView : UIView
  {
    public UIView lView;
    public UILabel header;
    public UILabel description;
    public UILabel beginLabel;
    public UILabel endLabel;
    public UILabel yearLabel;

    public UIButton menuButton;

    public UIButton beginValue;
    public UIButton endValue;

    public DateTime earliest;
    public DateTime latest;

    public UITableView extraInfoTable;
    public List<deviceReadings> selectedData;

    public UIViewController parentVC;

    public LegendView (UIView mainView,List<deviceReadings> pressuresTemperatures, UIViewController mainVC, DateTime start, DateTime end)
    {
      selectedData = pressuresTemperatures;
      parentVC = mainVC;
      earliest = start;
      latest = end;

      //lView = new UIView (new CGRect (.01 * mainView.Bounds.Width,.14 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .86 * mainView.Bounds.Height));
      lView = new UIView (new CGRect (0,0,mainView.Bounds.Width,mainView.Bounds.Height));
      lView.BackgroundColor = UIColor.White;
      lView.Layer.CornerRadius = 8;
      lView.Layer.BorderColor = UIColor.Black.CGColor;
      lView.Layer.BorderWidth = 1f;

      lView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        beginValue.ResignFirstResponder();
        endValue.ResignFirstResponder();
      }));

      createLegend ();
    }

    public void createLegend(){
      menuButton = new UIButton (new CGRect (.9 * lView.Bounds.Width,5,.075 * lView.Bounds.Width,.05 * lView.Bounds.Height));
      menuButton.SetBackgroundImage(UIImage.FromBundle("ic_graph"),UIControlState.Normal);

      header = new UILabel (new CGRect (0,0,lView.Bounds.Width,.1 * lView.Bounds.Height));
      header.Layer.CornerRadius = 8;
      header.TextAlignment = UITextAlignment.Center;
      header.Text = "Graph Information";

      description = new UILabel (new CGRect (0,.1 * lView.Bounds.Height,lView.Bounds.Width,.2 * lView.Bounds.Height));
      description.AdjustsFontSizeToFitWidth = true;
      description.Lines = 0;
      description.TextAlignment = UITextAlignment.Center;
      description.Text = "Pressure readings\n" + 
        "\tBlue Lines\n" +
        "Temperature readings\n" + 
        "\tRed Lines\n" +
        "Vacuum readings\n" + 
        "\tBurgandy Lines";

      createDatePicker ();

      createTableInfo ();

      lView.AddSubview (header);
      lView.AddSubview (description);
      lView.AddSubview (beginLabel);
      lView.AddSubview (beginValue);

      lView.AddSubview (endLabel);
      lView.AddSubview (endValue);

      lView.AddSubview (menuButton);
      lView.AddSubview (extraInfoTable);
    }

    public void createDatePicker(){     
      beginValue = new UIButton (new CGRect (.15 * lView.Bounds.Width, .34 * lView.Bounds.Height, .62 * lView.Bounds.Width, .05 * lView.Bounds.Height));
      beginValue.SetTitle (ChosenDates.subLeft.ToString (), UIControlState.Normal);
      beginValue.SetTitleColor (UIColor.Black, UIControlState.Normal);
      beginValue.Layer.BorderColor = UIColor.Black.CGColor;
      beginValue.Layer.BorderWidth = 1f;
      beginValue.UserInteractionEnabled = true;
      beginValue.TouchUpInside += BeginningDatePickerTapped;

      beginLabel = new UILabel (new CGRect (.08 * lView.Bounds.Width, .3 * lView.Bounds.Height, .8 * lView.Bounds.Width, .04 * lView.Bounds.Height));
      beginLabel.AdjustsFontSizeToFitWidth = true;
      beginLabel.Text = "Choose a manual start date for the graph";

      endValue = new UIButton (new CGRect (.15 * lView.Bounds.Width, .43 * lView.Bounds.Height,.62 * lView.Bounds.Width, .05 * lView.Bounds.Height));
      endValue.SetTitle (ChosenDates.subRight.ToString (), UIControlState.Normal);
      endValue.SetTitleColor (UIColor.Black, UIControlState.Normal);
      endValue.Layer.BorderColor = UIColor.Black.CGColor;
      endValue.Layer.BorderWidth = 1f;
      endValue.UserInteractionEnabled = true;
      endValue.TouchUpInside += EndingDatePickerTapped;

      endLabel = new UILabel (new CGRect (.08 * lView.Bounds.Width, .39 * lView.Bounds.Height, .8 * lView.Bounds.Width, .04 * lView.Bounds.Height));
      endLabel.AdjustsFontSizeToFitWidth = true;
      endLabel.Text = "Choose a manual end date for the graph";
    }

    public void createTableInfo(){
      extraInfoTable = new UITableView (new CGRect (.01 * lView.Bounds.Width,.5 * lView.Bounds.Height,.98 * lView.Bounds.Width,.5 * lView.Bounds.Height));
      extraInfoTable.RegisterClassForCellReuse(typeof(legendCell),"legendCell");

      if (selectedData.Count <= 3) {
        extraInfoTable.ScrollEnabled = false;
      }
      extraInfoTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      extraInfoTable.Layer.CornerRadius = 3;
      extraInfoTable.Source = new legendSource (selectedData,lView);
    }

    async void BeginningDatePickerTapped (object sender, EventArgs e)
    {
      //      var modalPicker = new ModalPickerViewController(ModalPickerType.Custom, "Select A Date", parentVC)
      //      {
      //        HeaderBackgroundColor = UIColor.Red,
      //        HeaderTextColor = UIColor.White,
      //        TransitioningDelegate = new ModalPickerTransitionDelegate(),
      //        ModalPresentationStyle = UIModalPresentationStyle.Custom
      //      };
      //      //Create the model for the Picker View
      //      modalPicker.PickerView.Model = new CustomPickerModel(ChosenDates.allTimes, lView);
      //
      //      //modalPicker.PickerView;
      //
      //      modalPicker.OnModalPickerDismissed += (s, ea) => 
      //      {
      //        modalPicker.DidChangeValue("stuff");
      //
      //        var index = modalPicker.PickerView.SelectedRowInComponent(0);
      //        ChosenDates.subLeft = DateTime.Parse(ChosenDates.allTimes[(int)index]);
      //
      //        beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
      //        parentVC.DismissViewController(false,null);
      //      };
      //
      //      await parentVC.PresentViewControllerAsync(modalPicker, true);

      yearLabel = new UILabel (new CGRect (265,44,80,44));
      yearLabel.MinimumFontSize = 28f;
      yearLabel.Text = ChosenDates.subLeft.Year.ToString();

      var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", parentVC, yearLabel)
      {
        HeaderBackgroundColor = UIColor.Red,
        HeaderTextColor = UIColor.White,
        TransitioningDelegate = new ModalPickerTransitionDelegate(),
        ModalPresentationStyle = UIModalPresentationStyle.Custom
      };

      modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
      NSDate conStart = (NSDate)(DateTime.SpecifyKind (ChosenDates.subLeft, DateTimeKind.Local));
      NSDate conEarly = (NSDate)(DateTime.SpecifyKind (earliest, DateTimeKind.Local));
      NSDate conLatest = (NSDate)(DateTime.SpecifyKind (latest, DateTimeKind.Local));
      modalPicker.DatePicker.MinimumDate = conEarly;
      modalPicker.DatePicker.MaximumDate = conLatest;
      modalPicker.DatePicker.Date = conStart;

      modalPicker.DatePicker.AllEvents += delegate {
        var dateFormatter = new NSDateFormatter()
        {
          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
        };
        var currentTime = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));
        Console.WriteLine("Stopped on date: " + currentTime);
        yearLabel.Text = currentTime.Year.ToString();
      };

      modalPicker.OnModalPickerDismissed += (s, ea) => 
      {
        var dateFormatter = new NSDateFormatter()
        {
          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
        };
        var time = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));

        if( time >= earliest){
          ChosenDates.subLeft = time;
        } else {
          ChosenDates.subLeft = earliest;
        }
        beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
      };

      await parentVC.PresentViewControllerAsync(modalPicker, true);
    }

    async void EndingDatePickerTapped (object sender, EventArgs e)
    {
      //      var modalPicker = new ModalPickerViewController(ModalPickerType.Custom, "Select A Date", parentVC)
      //      {
      //        HeaderBackgroundColor = UIColor.Red,
      //        HeaderTextColor = UIColor.White,
      //        TransitioningDelegate = new ModalPickerTransitionDelegate(),
      //        ModalPresentationStyle = UIModalPresentationStyle.Custom
      //      };
      //      //Create the model for the Picker View
      //      var endingDates = new List<string>();
      //      foreach (var time in ChosenDates.allTimes) {
      //        if (DateTime.Parse (time) > ChosenDates.subLeft) {
      //          endingDates.Add (time);
      //        }
      //      }
      //      modalPicker.PickerView.Model = new CustomPickerModel(endingDates,lView);
      //
      //      modalPicker.OnModalPickerDismissed += (s, ea) => 
      //      {
      //        var index = modalPicker.PickerView.SelectedRowInComponent(0);
      //        ChosenDates.subRight = DateTime.Parse(endingDates[(int)index]);
      //        endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);
      //        parentVC.DismissViewController(false,null);
      //      };

      var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", parentVC, yearLabel)
      {
        HeaderBackgroundColor = UIColor.Red,
        HeaderTextColor = UIColor.White,
        TransitioningDelegate = new ModalPickerTransitionDelegate(),
        ModalPresentationStyle = UIModalPresentationStyle.Custom
      };

      modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
      NSDate conStart = (NSDate)(DateTime.SpecifyKind (ChosenDates.subRight, DateTimeKind.Local));
      NSDate conEarly = (NSDate)(DateTime.SpecifyKind (ChosenDates.subLeft, DateTimeKind.Local));
      NSDate conLatest = (NSDate)(DateTime.SpecifyKind (latest, DateTimeKind.Local));
      modalPicker.DatePicker.MinimumDate = conEarly;
      modalPicker.DatePicker.MaximumDate = conLatest;
      modalPicker.DatePicker.Date = conStart;

      modalPicker.OnModalPickerDismissed += (s, ea) => 
      {
        var dateFormatter = new NSDateFormatter()
        {
          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
        };

        ChosenDates.subRight = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));
        endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);
      };

      await parentVC.PresentViewControllerAsync(modalPicker, true);
    }
  }
}

