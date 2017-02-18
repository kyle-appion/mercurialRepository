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
    public UIButton leaveButton;
    public UILabel explanation;
    public UILabel skipLabel;
    public UILabel progressLabel;
		public IWalkthrough walkthrough;   
		public WalkthroughHelp help;
		public UISwipeGestureRecognizer swipeLeft;
		public UISwipeGestureRecognizer swipeRight;

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
      pictureView.UserInteractionEnabled = true;
      ///ADD SWIPE GESTURES FOR ADVANCING/REWINDING WALKTHROUGH PAGE
    	swipeLeft = new UISwipeGestureRecognizer( swiper => {
    		Console.WriteLine("Swipe direction: " + swiper.Direction);
					walkthrough.GoForward();          
      });
      swipeLeft.Direction = UISwipeGestureRecognizerDirection.Left;
      
    	swipeRight = new UISwipeGestureRecognizer( swiper => {
    		Console.WriteLine("Swipe direction: " + swiper.Direction);  
					walkthrough.GoBackward();          
      });
      swipeRight.Direction = UISwipeGestureRecognizerDirection.Right;
      
      pictureView.AddGestureRecognizer(swipeLeft);
      pictureView.AddGestureRecognizer(swipeRight);
      			
	  	nextPicture = new UIButton(new CGRect(.85 * View.Bounds.Width, 70,.15 * View.Bounds.Width,.6 * View.Bounds.Height));
      lastPicture = new UIButton(new CGRect(0, 70,.15 * View.Bounds.Width,.6 * View.Bounds.Height));
      
	  	explanation = new UILabel(new CGRect(.05 * View.Bounds.Width, .6 * View.Bounds.Height + 50,.9 * View.Bounds.Width, .25 * View.Bounds.Height - 50));
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
      
      skipLabel = new UILabel(new CGRect(.12 * View.Bounds.Width, .84 * View.Bounds.Height, .5 * View.Bounds.Width, .06 * View.Bounds.Width));
      skipLabel.Text = "Don't show on startup";
      
      progressLabel = new UILabel(new CGRect(.25 * View.Bounds.Width, .9 * View.Bounds.Height, .5 * View.Bounds.Width, .1 * View.Bounds.Height));
      progressLabel.TextAlignment = UITextAlignment.Center;
      
    	leaveButton = new UIButton(new CGRect(.65 * View.Bounds.Width, .85 * View.Bounds.Height, .25 * View.Bounds.Width, .1 * View.Bounds.Width));
			leaveButton.SetTitle("Skip",UIControlState.Normal);
			leaveButton.SetTitleColor(UIColor.Black,UIControlState.Normal);
			leaveButton.TouchDown += (sender, e) => {leaveButton.BackgroundColor = UIColor.Blue;};
			leaveButton.TouchUpOutside += (sender, e) => {leaveButton.BackgroundColor = UIColor.FromRGB(255,215,101);};
			leaveButton.TouchUpInside += (sender, e) => {leaveButton.BackgroundColor = UIColor.FromRGB(255,215,101); NavigationController.PopViewController(true); };
			
			leaveButton.Layer.BorderWidth = 1f;
			leaveButton.BackgroundColor = UIColor.FromRGB(255, 215, 101); 

      walkthrough = new IntroductoryWalkthrough(View, explanation,pictureView, nextPicture, progressLabel);
      pictureView.Image = UIImage.FromBundle("Intro1");
      explanation.Text = Util.Strings.Walkthrough.INTR1;
      
      nextPicture.SetImage(UIImage.FromBundle("ic_arrow_rightwhite_long"), UIControlState.Normal);
      nextPicture.TouchDown += (sender, e) => {nextPicture.ImageView.BackgroundColor = UIColor.Blue;};
      nextPicture.TouchUpOutside += (sender, e) => {nextPicture.ImageView.BackgroundColor = UIColor.White;};
      nextPicture.TouchUpInside += (sender, e) => {
        nextPicture.ImageView.BackgroundColor = UIColor.White;
        walkthrough.GoForward();
      };
      
      lastPicture.SetImage(UIImage.FromBundle("ic_arrow_leftwhite_long"), UIControlState.Normal);
      lastPicture.TouchDown += (sender, e) => {lastPicture.ImageView.BackgroundColor = UIColor.Blue;};
      lastPicture.TouchUpOutside += (sender, e) => {lastPicture.ImageView.BackgroundColor = UIColor.White;};
      lastPicture.TouchUpInside += (sender, e) => {
        lastPicture.ImageView.BackgroundColor = UIColor.White;
        walkthrough.GoBackward();
      };

			setupWalkthrough();
    }
    
    public async void setupWalkthrough(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
 			
      View.AddSubview(pictureView);
			View.BringSubviewToFront(pictureView);
      View.AddSubview(explanation);
      View.AddSubview(nextPicture);
      View.AddSubview(lastPicture);
			View.AddSubview(skipButton);			
			View.AddSubview(skipLabel);
			View.AddSubview(progressLabel);
			View.AddSubview(leaveButton);
		}

    public override void DidReceiveMemoryWarning() {
    	Console.WriteLine("So Much Memory!!");
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


