using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Walkthrough {
  public partial class WalkthroughScreenshotViewController : BaseIONViewController {
    public WalkthroughScreenshotViewController(IntPtr handle) : base (handle) {
    }

    public UIImageView pictureView;
    public UIButton nextPicture;
    public UIButton lastPicture;
    public UILabel explanation;
    public WorkBenchWalkthrough walkthrough1;


    public string sectionName;
    public int sectionIndex = 1;

    public override void ViewDidLoad() {
      base.ViewDidLoad();

      pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, .1 * View.Bounds.Height, .7 * View.Bounds.Width, .5 * View.Bounds.Height));

      explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height,.9 * View.Bounds.Width, .2 * View.Bounds.Height ));
      explanation.AdjustsFontSizeToFitWidth = true;
      explanation.Lines = 0;

      if (sectionName == "WorkBench") {
        Console.WriteLine("Chose workbench walkthrough");
        walkthrough1 = new WorkBenchWalkthrough(View, explanation,pictureView);
        pictureView.Image = UIImage.FromBundle("WorkBench1");
        explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
      }

      nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      nextPicture.SetTitle("Next", UIControlState.Normal);
      nextPicture.SetTitleColor(UIColor.Black, UIControlState.Normal);
      nextPicture.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      nextPicture.Layer.BorderWidth = 1f;
      nextPicture.TouchDown += (sender, e) => {nextPicture.BackgroundColor = UIColor.Blue;};
      nextPicture.TouchUpOutside += (sender, e) => {nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);};
      nextPicture.TouchUpInside += (sender, e) => {
        nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);
        walkthrough1.GoForward();
      };
      lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      lastPicture.SetTitle("Previous", UIControlState.Normal);
      lastPicture.SetTitleColor(UIColor.Black, UIControlState.Normal);
      lastPicture.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      lastPicture.Layer.BorderWidth = 1f;
      lastPicture.TouchDown += (sender, e) => {lastPicture.BackgroundColor = UIColor.Blue;};
      lastPicture.TouchUpOutside += (sender, e) => {lastPicture.BackgroundColor = UIColor.FromRGB(255,215,101);};
      lastPicture.TouchUpInside += (sender, e) => {
        lastPicture.BackgroundColor = UIColor.FromRGB(255,215,101);
        walkthrough1.GoBackward();
      }; 
      // Perform any additional setup after loading the view, typically from a nib.

      View.AddSubview(pictureView);
      View.AddSubview(explanation);
      View.AddSubview(nextPicture);
      View.AddSubview(lastPicture);
      View.AddSubview(walkthrough1.arrowImage);
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


