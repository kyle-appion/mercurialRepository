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
		public IWalkthrough walkthrough;

    public string sectionName;
    public int sectionIndex = 1;

    public override void ViewDidLoad() {
      base.ViewDidLoad();

      pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, 50 , .7 * View.Bounds.Width, .6 * View.Bounds.Height));
			
			nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .85 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .85 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      
			explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height + 50,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
      explanation.AdjustsFontSizeToFitWidth = true;
      explanation.Lines = 0;
      
      if (sectionName == "WorkBench") {
      	Console.WriteLine("WorkBench");
	      walkthrough = new WorkBenchWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("WorkBench1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
      } else if (sectionName == "Analyzer"){
      	Console.WriteLine("Analyzer");
	      walkthrough = new AnalyzerWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("Analyzer1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} else if (sectionName == "PT Chart"){
				Console.WriteLine("PT Chart");
	      walkthrough = new PTChartWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("PTChart1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} else if (sectionName == "Superheat/Subcool"){
				Console.WriteLine("SH/SC");
	      walkthrough = new SuperheatSubcoolWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("SHSC1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} else if (sectionName == "Calibration Certificates"){
				Console.WriteLine("Calibration");
	      walkthrough = new CalibrationCertWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("Cert1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} else if (sectionName == "Screenshot Archives"){
				Console.WriteLine("Screenshot");
	      walkthrough = new ScreenshotArchiveWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("Archives1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} else if (sectionName == "Settings"){
				Console.WriteLine("Settings");
	      walkthrough = new AnalyzerWalkthrough(View, explanation,pictureView, nextPicture);
	      pictureView.Image = UIImage.FromBundle("Analyzer1");
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
			} 
      
      nextPicture.SetTitle("Next", UIControlState.Normal);
      nextPicture.SetTitleColor(UIColor.Black, UIControlState.Normal); 
      nextPicture.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      nextPicture.Layer.BorderWidth = 1f;
      nextPicture.TouchDown += (sender, e) => {nextPicture.BackgroundColor = UIColor.Blue;};
      nextPicture.TouchUpOutside += (sender, e) => {nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);};
      nextPicture.TouchUpInside += (sender, e) => {
        nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);
        walkthrough.GoForward();
      };
      
      lastPicture.SetTitle("Previous", UIControlState.Normal);
      lastPicture.SetTitleColor(UIColor.Black, UIControlState.Normal);
      lastPicture.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      lastPicture.Layer.BorderWidth = 1f;
      lastPicture.TouchDown += (sender, e) => {lastPicture.BackgroundColor = UIColor.Blue;};
      lastPicture.TouchUpOutside += (sender, e) => {lastPicture.BackgroundColor = UIColor.FromRGB(255,215,101);};
      lastPicture.TouchUpInside += (sender, e) => {
        lastPicture.BackgroundColor = UIColor.FromRGB(255,215,101);
        walkthrough.GoBackward();
      };

      View.AddSubview(pictureView);
      View.SendSubviewToBack(pictureView);
      View.AddSubview(explanation);
      View.AddSubview(nextPicture);
      View.AddSubview(lastPicture);
    }

    public override void DidReceiveMemoryWarning() {
    	Console.WriteLine("So Much Memory!!");
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


