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
    public UIButton skipButton;
    public UILabel explanation;
    public UILabel skipLabel;
		public IWalkthrough walkthrough;   
		public WalkthroughHelp help;

    public string sectionName;
    public int sectionIndex = 1;
    public int loadCount = 0;
    public bool initial = false;
    public bool skipit = false;

    public override void ViewDidLoad() {
      base.ViewDidLoad();

			if(initial){
				help = new WalkthroughHelp(this.View);
				this.View.AddSubview(help.helpView);
			} 
      
    	pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, 70 , .7 * View.Bounds.Width, .6 * View.Bounds.Height));
    //pictureView = new UIImageView(new CGRect(.15 * View.Bounds.Width, 0, .7 * View.Bounds.Width, .6 * View.Bounds.Height));
			
	  	nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .9 * View.Bounds.Height,.3 * View.Bounds.Width,.075 * View.Bounds.Height));
	  //nextPicture = new UIButton(new CGRect(.6 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .9 * View.Bounds.Height,.3 * View.Bounds.Width,.075 * View.Bounds.Height));
      //lastPicture = new UIButton(new CGRect(.1 * View.Bounds.Width, .8 * View.Bounds.Height,.3 * View.Bounds.Width,.1 * View.Bounds.Height));
      
	  	explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height + 50,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
	  //explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
      explanation.AdjustsFontSizeToFitWidth = true;
      explanation.Lines = 0;
      
      skipButton = new UIButton(new CGRect(.05 * View.Bounds.Width, .84 * View.Bounds.Height, .06 * View.Bounds.Width, .06 * View.Bounds.Width));
      var preWalkthrough = NSUserDefaults.StandardUserDefaults.StringForKey("walkthrough_complete");
      if(string.IsNullOrEmpty(preWalkthrough) || preWalkthrough == "1"){
				skipit = true;
			}
			
      if(skipit){
      	skipButton.SetBackgroundImage(UIImage.FromBundle("filled_checkbox"),UIControlState.Normal);
			} else {
      	skipButton.SetBackgroundImage(UIImage.FromBundle("blank_checkbox"),UIControlState.Normal);
			}
			
      skipButton.TouchUpInside += (sender, e) => {
				if(skipit){
					skipit = false;
					skipButton.SetBackgroundImage(UIImage.FromBundle("blank_checkbox"), UIControlState.Normal);
					NSUserDefaults.StandardUserDefaults.SetString("","walkthrough_complete");
				} else {
					skipit = true;
					skipButton.SetBackgroundImage(UIImage.FromBundle("filled_checkbox"), UIControlState.Normal);
					NSUserDefaults.StandardUserDefaults.SetString("1","walkthrough_complete");
				}
			};
      
      skipLabel = new UILabel(new CGRect(.12 * View.Bounds.Width, .84 * View.Bounds.Height, .6 * View.Bounds.Width, .06 * View.Bounds.Width));
      skipLabel.Text = "Don't show on startup";

      walkthrough = new IntroductoryWalkthrough(View, explanation,pictureView, nextPicture);
      pictureView.Image = UIImage.FromBundle("Intro1");
      explanation.Text = Util.Strings.Walkthrough.INTR1;
      
      nextPicture.SetTitle(Util.Strings.NEXT, UIControlState.Normal);
      nextPicture.SetTitleColor(UIColor.Black, UIControlState.Normal);
      nextPicture.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      nextPicture.Layer.BorderWidth = 1f;
      nextPicture.TouchDown += (sender, e) => {nextPicture.BackgroundColor = UIColor.Blue;};
      nextPicture.TouchUpOutside += (sender, e) => {nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);};
      nextPicture.TouchUpInside += (sender, e) => {
        nextPicture.BackgroundColor = UIColor.FromRGB(255,215,101);
        walkthrough.GoForward();
      };
      
      lastPicture.SetTitle(Util.Strings.PREVIOUS, UIControlState.Normal);
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
 			
      View.AddSubview(pictureView);
      View.SendSubviewToBack(pictureView);
      View.AddSubview(explanation);
      View.AddSubview(nextPicture);
      View.AddSubview(lastPicture);
			View.AddSubview(skipButton);			
			View.AddSubview(skipLabel);			
		}

    public override void DidReceiveMemoryWarning() {
    	Console.WriteLine("So Much Memory!!");
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


