using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Walkthrough {
  
  public class WorkBenchWalkthrough {
    public int currentStep;
    public int maxStep;
    public UIView parentView;
    public UILabel explanation;
    public UIImageView arrowImage;
    public UIImageView screenshot;

    public WorkBenchWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage) {
      currentStep = 1;
      maxStep = 20;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
    }

    public void GoForward(){
      currentStep++;

      switch (currentStep) {
        case 1:
          explainMenuDrawer();
          Console.WriteLine("Step 1 forward");
          break;
        case 2:
          explainRecordButton();
          Console.WriteLine("Step 2 forward");
          break;
        case 3:
          explainScreenshotButton();
          Console.WriteLine("Step 3 forward");
          break;
        case 4:
          explainAddViewerButton();
          Console.WriteLine("Step 4 forward");
          break;
        default:
          currentStep = 1;
          explainMenuDrawer();
          Console.WriteLine("Step 1 restart forward");
          break;
      }
    }

    public void GoBackward(){
        currentStep--;
      switch (currentStep) {
        case 1:
          explainMenuDrawer();
          Console.WriteLine("Step 1 backward");
          break;
        case 2:
          explainRecordButton();
          Console.WriteLine("Step 2 backward");
          break;
        case 3:
          explainScreenshotButton();
          Console.WriteLine("Step 3 backward");
          break;
        case 4:
          screenshot.Image = UIImage.FromBundle("WorkBench1");
          explainAddViewerButton();
          Console.WriteLine("Step 4 backward");
          break;
        default:
          currentStep = 4;
          screenshot.Image = UIImage.FromBundle("WorkBench1");
          explainAddViewerButton();
          Console.WriteLine("Step 4 restart backward");
          break;
      }
    }

    private void explainMenuDrawer(){
      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
    }
    private void explainRecordButton(){
      arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the record button for starting and ending a logging session of your connected devices' readings.";
    }
    private void explainScreenshotButton(){
      arrowImage.Frame = new CGRect(.79 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the screenshot button that captures your device's screen to allow for keeping records of important moments.";
    }
    private void explainAddViewerButton(){
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.04 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This button lets you add a device to the workbench for monitoring. You will recieve the information as soon as the physical device does.";
    }

  }

}

