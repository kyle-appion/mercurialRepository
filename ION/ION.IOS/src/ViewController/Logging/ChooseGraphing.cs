using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseGraphing : UIView {

    public UIView graphingType;
    public UILabel header;

    public nfloat cellHeight;

    public GraphingView graphingView;
    public LegendView legendView;
    public ChooseData checkData;

    public ChooseGraphing(UIView mainView, ChooseData dataSection) {
      graphingType = new UIView(new CGRect(.01 * mainView.Bounds.Width, 0, .98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      graphingType.BackgroundColor = UIColor.White;
      graphingType.Layer.BorderColor = UIColor.Black.CGColor;
      graphingType.Layer.BorderWidth = 1f;
      graphingType.Layer.CornerRadius = 5;
      graphingType.Hidden = true;

      cellHeight = .07f * mainView.Bounds.Height;

      checkData = dataSection;
    }

    public void SetupSettingsButtons(UIView mainView, UIActivityIndicatorView activityLoadingGraphs){
    	Console.WriteLine("Setting buttons being setup");
      graphingView.menuButton.TouchUpInside += (sender, e) => {
        legendView.beginValue.SetTitle(ChosenDates.subLeft.ToString(), UIControlState.Normal);
        legendView.endValue.SetTitle(ChosenDates.subRight.ToString(), UIControlState.Normal);
        checkData.DataType.RemoveGestureRecognizer(checkData.resize);

        UIView.Transition(
          fromView:graphingView.gView,
          toView:legendView.lView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () =>{
            graphingType.SendSubviewToBack(graphingView.gView);
            graphingType.BringSubviewToFront(legendView.lView);
            checkData.DataType.AddGestureRecognizer(checkData.resize);
          }
        );
      };

      legendView.menuButton.TouchUpInside += (sender, e) => {
        checkData.DataType.RemoveGestureRecognizer(checkData.resize);
        ////calculate left tracker size based on manual selected dates
        var leftIndex = ChosenDates.allTimes[ChosenDates.subLeft.ToString()];
        var lwidth = graphingView.dateMultiplier * leftIndex;

        ///resize left tracker to match manual selection
        graphingView.leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, lwidth, graphingView.trackerHeight);
        var trackerRect = graphingView.leftTrackerCircle.Center;
        trackerRect.X = graphingView.leftTrackerView.Frame.Right + (.5f * graphingView.leftTrackerCircle.Bounds.Width);
        graphingView.leftTrackerCircle.Center = trackerRect;

        ////calculate right tracker size based on manual selected dates
        var rightIndex = ChosenDates.allTimes[ChosenDates.subRight.ToString()];
        var rfinal = (graphingView.dateMultiplier * rightIndex) + (.1 * mainView.Bounds.Width);
        double rwidth = ChosenDates.allTimes[ChosenDates.latest.ToString()] - rightIndex;
        rwidth = rwidth * graphingView.dateMultiplier;

        ///resize right tracker to match manual selection
        graphingView.rightTrackerView.Frame = new CGRect(rfinal,.15 * mainView.Bounds.Height,rwidth,graphingView.trackerHeight);
        trackerRect = graphingView.rightTrackerCircle.Center;
        trackerRect.X = graphingView.rightTrackerView.Frame.Left - (.5f * graphingView.rightTrackerCircle.Bounds.Width);
        graphingView.rightTrackerCircle.Center = trackerRect;

        graphingView.subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();

        UIView.Transition(
          fromView:legendView.lView,
          toView:graphingView.gView,
          duration:.5,
          options: UIViewAnimationOptions.TransitionFlipFromRight,
          completion: () =>{
            graphingType.SendSubviewToBack(legendView.lView);
            graphingType.BringSubviewToFront(graphingView.gView);
            checkData.DataType.AddGestureRecognizer(checkData.resize);
          }
        );
      };
      activityLoadingGraphs.StopAnimating();
    }

  }
}

