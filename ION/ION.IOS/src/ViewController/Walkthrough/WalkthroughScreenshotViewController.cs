using System;
using System.Threading.Tasks;
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
    public int loadCount = 0;

    public override void ViewDidLoad() {
      base.ViewDidLoad();
			Console.WriteLine("Walkthrough holder bounds: " + walkthroughHolder.Bounds);
    	pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, 70 , .7 * View.Bounds.Width, .6 * View.Bounds.Height));
    //pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, 0, .7 * View.Bounds.Width, .6 * View.Bounds.Height));
			
	  	nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .85 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
	  //nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .85 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      //lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      
	  	explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height + 50,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
	  //explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
      explanation.AdjustsFontSizeToFitWidth = true;
      explanation.Lines = 0;

      walkthrough = new IntroductoryWalkthrough(View, explanation,pictureView, nextPicture);
      pictureView.Image = UIImage.FromBundle("Intro1");
      explanation.Text = Util.Strings.Walkthrough.INTR1;
      
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

			setupWalkthrough();
    }
    
    public async void setupWalkthrough(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			Console.WriteLine("Walkthrough holder bounds: " + walkthroughHolder.Bounds);
			
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


