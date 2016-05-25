using System;
using System.Linq;
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
    public UILabel beginLabel;
    public UILabel endLabel;
    public UILabel blueLabel;
    public UILabel redLabel;
    public UILabel burgundyLabel;

    public UIImageView bluePlot;
    public UIImageView redPlot;
    public UIImageView burgundyPlot;

    public UIButton menuButton;
    public UIButton beginValue;
    public UIButton endValue;

    public UITableView extraInfoTable;
    public List<deviceReadings> selectedData;

    public UIViewController parentVC;

    public LegendView (UIView mainView,List<deviceReadings> pressuresTemperatures, UIViewController mainVC)
    {
      selectedData = pressuresTemperatures;
      parentVC = mainVC;

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
      menuButton = new UIButton (new CGRect (.9 * lView.Bounds.Width,5,.1 * lView.Bounds.Width,.1 * lView.Bounds.Width));
      menuButton.SetBackgroundImage(UIImage.FromBundle("ic_graph"),UIControlState.Normal);

      header = new UILabel (new CGRect (0,0,lView.Bounds.Width,.1 * lView.Bounds.Height));
      header.Layer.CornerRadius = 8;
      header.TextAlignment = UITextAlignment.Center;
      header.Text = "Graph Information";
      header.Font = UIFont.BoldSystemFontOfSize(20);

      bluePlot = new UIImageView(new CGRect(.3 * lView.Bounds.Width,.1 * lView.Bounds.Height,.15 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      bluePlot.Image = UIImage.FromBundle("img_blue_plot");
      blueLabel = new UILabel(new CGRect(.45 * lView.Bounds.Width,.1 * lView.Bounds.Height,.25 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      blueLabel.AdjustsFontSizeToFitWidth = true;
      blueLabel.Text = "Pressure";
      blueLabel.TextAlignment = UITextAlignment.Center;

      burgundyPlot = new UIImageView(new CGRect(.3 * lView.Bounds.Width,.165 * lView.Bounds.Height,.15 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      burgundyPlot.Image = UIImage.FromBundle("img_burgundy_plot");
      burgundyLabel = new UILabel(new CGRect(.45 * lView.Bounds.Width,.165 * lView.Bounds.Height,.25 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      burgundyLabel.AdjustsFontSizeToFitWidth = true;
      burgundyLabel.Text = "Vacuum";
      burgundyLabel.TextAlignment = UITextAlignment.Center;

      redPlot = new UIImageView(new CGRect(.3 * lView.Bounds.Width,.23 * lView.Bounds.Height,.15 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      redPlot.Image = UIImage.FromBundle("img_red_plot");
      redLabel = new UILabel(new CGRect(.45 * lView.Bounds.Width,.23 * lView.Bounds.Height,.35 * lView.Bounds.Width, .065 * lView.Bounds.Height));
      redLabel.AdjustsFontSizeToFitWidth = true;
      redLabel.Text = "Temperature";
      redLabel.TextAlignment = UITextAlignment.Center;


      createDatePicker ();

      createTableInfo ();

      lView.AddSubview (header);
      lView.AddSubview (bluePlot);
      lView.AddSubview (redPlot);
      lView.AddSubview (burgundyPlot);
      lView.AddSubview (blueLabel);
      lView.AddSubview (redLabel);
      lView.AddSubview (burgundyLabel);
      lView.AddSubview (beginLabel);
      lView.AddSubview (beginValue);

      lView.AddSubview (endLabel);
      lView.AddSubview (endValue);

      lView.AddSubview (menuButton);
      lView.AddSubview (extraInfoTable);
    }

    public void createDatePicker(){     
      beginValue = new UIButton (new CGRect (.19 * lView.Bounds.Width, .34 * lView.Bounds.Height, .62 * lView.Bounds.Width, .05 * lView.Bounds.Height));
      beginValue.SetTitle (ChosenDates.subLeft.ToString (), UIControlState.Normal);
      beginValue.SetTitleColor (UIColor.Black, UIControlState.Normal);
      beginValue.Layer.BorderColor = UIColor.Black.CGColor;
      beginValue.Layer.BorderWidth = 1f;
      beginValue.Layer.CornerRadius = 5;
      beginValue.UserInteractionEnabled = true;
      beginValue.TouchUpInside += BeginningDatePickerTapped;

      beginLabel = new UILabel (new CGRect (0, .3 * lView.Bounds.Height, lView.Bounds.Width, .04 * lView.Bounds.Height));
      beginLabel.AdjustsFontSizeToFitWidth = true;
      beginLabel.TextAlignment = UITextAlignment.Center;
      beginLabel.Text = "Select a start time for the graph";

      endValue = new UIButton (new CGRect (.19 * lView.Bounds.Width, .43 * lView.Bounds.Height,.62 * lView.Bounds.Width, .05 * lView.Bounds.Height));
      endValue.SetTitle (ChosenDates.subRight.ToString (), UIControlState.Normal);
      endValue.SetTitleColor (UIColor.Black, UIControlState.Normal);
      endValue.Layer.BorderColor = UIColor.Black.CGColor;
      endValue.Layer.BorderWidth = 1f;
      endValue.Layer.CornerRadius = 5;
      endValue.UserInteractionEnabled = true;
      endValue.TouchUpInside += EndingDatePickerTapped;

      endLabel = new UILabel (new CGRect (0, .39 * lView.Bounds.Height, lView.Bounds.Width, .04 * lView.Bounds.Height));
      endLabel.AdjustsFontSizeToFitWidth = true;
      endLabel.TextAlignment = UITextAlignment.Center;
      endLabel.Text = "Select an end time for the graph";
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
      extraInfoTable.Bounces = false;
    } 

    async void BeginningDatePickerTapped (object sender, EventArgs e)
    {
      var modalPicker = new ModalPickerViewController(ModalPickerType.Custom, "Select A Start Time", parentVC)
      {
        HeaderBackgroundColor = UIColor.Red,
        HeaderTextColor = UIColor.White,
        TransitioningDelegate = new ModalPickerTransitionDelegate(),
        ModalPresentationStyle = UIModalPresentationStyle.Custom
      };
      //Create the model for the Picker View
      var timeList = ChosenDates.allTimes.Select(kvp => kvp.Key).ToList();
      timeList.OrderBy(x => x);
      modalPicker.PickerView.Model = new CustomPickerModel(timeList, lView);

      modalPicker.OnModalPickerDismissed += (s, ea) => 
      {
        modalPicker.DidChangeValue("stuff");

        var index = modalPicker.PickerView.SelectedRowInComponent(0);
        ChosenDates.subLeft = DateTime.Parse(timeList[(int)index]);

        beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
        parentVC.DismissViewController(false,null);
      };

      await parentVC.PresentViewControllerAsync(modalPicker, true);

//      yearLabel = new UILabel (new CGRect (265,44,80,44));
//      yearLabel.MinimumFontSize = 28f;
//      yearLabel.Text = ChosenDates.subLeft.Year.ToString();
//
//      var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", parentVC, yearLabel)
//      {
//        HeaderBackgroundColor = UIColor.Red,
//        HeaderTextColor = UIColor.White,
//        TransitioningDelegate = new ModalPickerTransitionDelegate(),
//        ModalPresentationStyle = UIModalPresentationStyle.Custom
//      };
//
//      modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
//      NSDate conStart = (NSDate)(DateTime.SpecifyKind (ChosenDates.subLeft, DateTimeKind.Local));
//      NSDate conEarly = (NSDate)(DateTime.SpecifyKind (earliest, DateTimeKind.Local));
//      NSDate conLatest = (NSDate)(DateTime.SpecifyKind (latest, DateTimeKind.Local));
//      modalPicker.DatePicker.MinimumDate = conEarly;
//      modalPicker.DatePicker.MaximumDate = conLatest;
//      modalPicker.DatePicker.Date = conStart;
//
//      modalPicker.DatePicker.AllEvents += delegate {
//        var dateFormatter = new NSDateFormatter()
//        {
//          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
//        };
//        var currentTime = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));
//        Console.WriteLine("Stopped on date: " + currentTime);
//        yearLabel.Text = currentTime.Year.ToString();
//      };
//
//      modalPicker.OnModalPickerDismissed += (s, ea) => 
//      {
//        var dateFormatter = new NSDateFormatter()
//        {
//          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
//        };
//        var time = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));
//
//        if( time >= earliest){
//          ChosenDates.subLeft = time;
//        } else {
//          ChosenDates.subLeft = earliest;
//        }
//        beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
//      };

//      await parentVC.PresentViewControllerAsync(modalPicker, true);
    }

    async void EndingDatePickerTapped (object sender, EventArgs e)
    {
      var modalPicker = new ModalPickerViewController(ModalPickerType.Custom, "Select An End Time", parentVC)
      {
        HeaderBackgroundColor = UIColor.Red,
        HeaderTextColor = UIColor.White,
        TransitioningDelegate = new ModalPickerTransitionDelegate(),
        ModalPresentationStyle = UIModalPresentationStyle.Custom
      };
      //Create the model for the Picker View
      var endingDates = new List<string>();
      foreach (var time in ChosenDates.allTimes) {
        if (DateTime.Parse (time.Key) > ChosenDates.subLeft) {
          endingDates.Add (time.Key);
        }
      }
      endingDates.OrderBy(x => x);
      modalPicker.PickerView.Model = new CustomPickerModel(endingDates,lView);

      modalPicker.OnModalPickerDismissed += (s, ea) => 
      {
        var index = modalPicker.PickerView.SelectedRowInComponent(0);
        ChosenDates.subRight = DateTime.Parse(endingDates[(int)index]);
        endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);
        parentVC.DismissViewController(false,null);
      };
      await parentVC.PresentViewControllerAsync(modalPicker, true);
//      var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", parentVC, yearLabel)
//      {
//        HeaderBackgroundColor = UIColor.Red,
//        HeaderTextColor = UIColor.White,
//        TransitioningDelegate = new ModalPickerTransitionDelegate(),
//        ModalPresentationStyle = UIModalPresentationStyle.Custom
//      };
//
//      modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
//      NSDate conStart = (NSDate)(DateTime.SpecifyKind (ChosenDates.subRight, DateTimeKind.Local));
//      NSDate conEarly = (NSDate)(DateTime.SpecifyKind (ChosenDates.subLeft, DateTimeKind.Local));
//      NSDate conLatest = (NSDate)(DateTime.SpecifyKind (latest, DateTimeKind.Local));
//      modalPicker.DatePicker.MinimumDate = conEarly;
//      modalPicker.DatePicker.MaximumDate = conLatest;
//      modalPicker.DatePicker.Date = conStart;
//
//      modalPicker.OnModalPickerDismissed += (s, ea) => 
//      {
//        var dateFormatter = new NSDateFormatter()
//        {
//          DateFormat = "MM'/'dd'/'yyyy hh':'mm a"
//        };
//
//        ChosenDates.subRight = DateTime.Parse(dateFormatter.ToString(modalPicker.DatePicker.Date));
//        endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);
//      };
//
//      await parentVC.PresentViewControllerAsync(modalPicker, true);
    }
  }
}

