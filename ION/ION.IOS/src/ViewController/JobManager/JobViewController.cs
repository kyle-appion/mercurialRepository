using System;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobViewController : BaseIONViewController {

    CreateJobView createJob;
    public JobViewController(IntPtr handle): base(handle){
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));

      InitNavigationBar("ic_nav_workbench", false);

      backAction = () => {
        root.navigation.ToggleMenu();
      }; 
      Title = "Job Manager"; 
      createJob = new CreateJobView(View);
      View.AddSubview(createJob.cjView);
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


