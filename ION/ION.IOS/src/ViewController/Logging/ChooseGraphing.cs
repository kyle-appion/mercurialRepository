using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseGraphing : UIView {

    public UIView graphingType;
    public UILabel header;
    public UITextView rawData;
    public nfloat cellHeight;

    public GraphingView graphingView;
    public LegendView legendView;

    public List<deviceReadings> pressuresTemperatures = new List<deviceReadings> ();

    public ChooseGraphing(UIView mainView, UIViewController mainVC) {
      graphingType = new UIView(new CGRect(.01 * mainView.Bounds.Width, .14 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      graphingType.BackgroundColor = UIColor.White;
      graphingType.Layer.BorderColor = UIColor.Black.CGColor;
      graphingType.Layer.BorderWidth = 1f;
      graphingType.Layer.CornerRadius = 8;
      graphingType.Hidden = true;

      cellHeight = .07f * mainView.Bounds.Height;
    }
    public void SetupSettingsButtons(UIView mainView){
      graphingView.menuButton.TouchUpInside += (sender, e) => {
        legendView.beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
        legendView.endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);

        UIView.Transition(
          fromView:graphingView.gView,
          toView:legendView.lView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () =>{
            graphingType.SendSubviewToBack(graphingView.gView);
            graphingType.BringSubviewToFront(legendView.lView);
          }
        );
      };

      legendView.menuButton.TouchUpInside += (sender, e) => {
        ////calculate left tracker size based on manual selected dates
        var TotalTime = graphingView.latest.Subtract(graphingView.earliest).TotalMilliseconds;
        var ldifference = ChosenDates.subLeft.Subtract(graphingView.earliest).TotalMilliseconds;
        var width = ldifference /  TotalTime;
        var final = width * (.8 * graphingView.graphTable.Bounds.Width);

        ///resize left tracker to match manual selection
        graphingView.leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.065 * mainView.Bounds.Height, final, graphingView.trackerHeight);
        var trackerRect = graphingView.leftTrackerCircle.Center;
        trackerRect.X = graphingView.leftTrackerView.Center.X + (.5f * graphingView.leftTrackerView.Bounds.Width);
        graphingView.leftTrackerCircle.Center = trackerRect;

        ////calculate right tracker size based on manual selected dates
        var rdifference = graphingView.latest.Subtract(ChosenDates.subRight).TotalMilliseconds;
        var rwidth = rdifference /  TotalTime;
        var rfinal = rwidth * (.8 * graphingView.graphTable.Bounds.Width);
        rfinal = .915 * graphingView.graphTable.Bounds.Width - (nfloat)rfinal;
        rwidth = .915 * graphingView.graphTable.Bounds.Width - rfinal;

        ///resize right tracker to match manual selection
        graphingView.rightTrackerView.Frame = new CGRect(rfinal,.065 * mainView.Bounds.Height,rwidth,graphingView.trackerHeight);
        trackerRect = graphingView.rightTrackerCircle.Center;
        trackerRect.X = graphingView.rightTrackerView.Center.X - (.5f * graphingView.rightTrackerView.Bounds.Width);
        graphingView.rightTrackerCircle.Center = trackerRect;

        graphingView.subDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();

        UIView.Transition(
          fromView:legendView.lView,
          toView:graphingView.gView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () =>{
            graphingType.SendSubviewToBack(legendView.lView);
            graphingType.BringSubviewToFront(graphingView.gView); 
          }
        );
      };
    }

  }
}

