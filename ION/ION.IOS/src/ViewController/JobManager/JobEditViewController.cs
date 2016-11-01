using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;
using System.Threading.Tasks;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobEditViewController : BaseIONViewController {
    public JobEditViewController(IntPtr handle) : base(handle) {
    }

    UITabBar tabManager;
    EditJobView editView;
    JobSessionView associateView;
    JobNotesView notesView;
    public UIButton saveButton;
    public int frnJID;
    IION ion;
    static int loadCount = 0;
    static nfloat loadHeight = 0;

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      ion = AppState.context;
			
      saveButton = new UIButton(new CGRect(0,0,60,30));
      saveButton.SetTitle("Save", UIControlState.Normal);
      saveButton.Layer.BorderWidth = 1f;
      saveButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      saveButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      saveButton.TouchDown += (sender, e) => {saveButton.BackgroundColor = UIColor.Blue;};
      saveButton.TouchUpOutside += (sender, e) => {saveButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      saveButton.TouchUpInside += (sender, e) => {      	
        saveButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);

        if(editView.jobName.Text.Length.Equals(0)){
          UIView.Animate(.75,0,UIViewAnimationOptions.CurveEaseInOut,
            () =>{
              editView.jobName.BackgroundColor = UIColor.Red;
              editView.jobName.BackgroundColor = UIColor.White;
            },()=>{});
          return;
        }

        if(frnJID.Equals(0)){
          var jobCheck = ion.database.Query<ION.Core.Database.JobRow>("SELECT JID FROM JobRow WHERE jobName = ?",editView.jobName.Text);

          if(jobCheck.Count == 0){
            var job = new ION.Core.Database.JobRow(){jobName = editView.jobName.Text, customerNumber = editView.customerNumber.Text, dispatchNumber = editView.dispatchNumber.Text, poNumber = editView.prodOrderNumber.Text};         
            ion.database.Insert(job);
            this.NavigationController.PopViewController(true);
          } else {
            editView.confirmLabel.TextColor = UIColor.Red;
            editView.confirmLabel.Text = "Job Name Already Exists";
          }
        } else {
          var jobCheck = ion.database.Query<ION.Core.Database.JobRow>("SELECT JID FROM JobRow WHERE jobName = ?",editView.jobName.Text);

          if(jobCheck.Count == 0 || jobCheck[0].JID == frnJID){
	          var previousName = ion.database.Query<ION.Core.Database.JobRow>("SELECT jobName FROM JobRow WHERE JID = ?", frnJID);
	          var fileDir = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), previousName[0].jobName + ".xml");
	          if(System.IO.File.Exists(fileDir) && ((editView.jobName.Text+".xml") != (previousName[0].jobName + ".xml"))){
	            var newFileDir = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), editView.jobName.Text + ".xml");
	            System.IO.File.Copy(fileDir,newFileDir,true);
	            System.IO.File.Delete(fileDir);
						}	
						ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobName = ?, customerNumber = ?, dispatchNumber = ?, poNumber = ? WHERE JID = ?",editView.jobName.Text, editView.customerNumber.Text, editView.dispatchNumber.Text, editView.prodOrderNumber.Text,frnJID);
            editView.confirmLabel.TextColor = UIColor.Green;
            editView.confirmLabel.Text = "Job Updated";
          } else {
            editView.confirmLabel.TextColor = UIColor.Red;
            editView.confirmLabel.Text = "Job Name Already Exists";
          }
        }

        editView.confirmLabel.Hidden = false;     
      };

      NavigationItem.Title = "Edit Job";

      var button = new UIBarButtonItem(saveButton);

      NavigationItem.RightBarButtonItem = button;
			AutomaticallyAdjustsScrollViewInsets = false;
			
      Console.WriteLine("View bounds start " + View.Bounds);
			Console.WriteLine("Holder dimensions " + holderView.Bounds);
			Console.WriteLine("Scroller dimensions " + infoScroller.Bounds);
  		setupLayout();
    }

		public async void setupLayout(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			Console.WriteLine("View dimensions " + View.Bounds);
			Console.WriteLine("Holder dimensions " + holderView.Bounds);
			Console.WriteLine("Scroller dimensions " + infoScroller.Bounds);
			var managerOffset = 50;
			if(loadCount == 0){
				loadHeight = infoScroller.Bounds.Height;
				loadCount++;
			} else {
				if(infoScroller.Bounds.Height != loadHeight){
					Console.WriteLine("Heights didn't match for scrollview " + infoScroller.Bounds + " stored height: " + loadHeight);
					var tempBounds = infoScroller.Bounds;
					tempBounds.Height = loadHeight;
					infoScroller.Bounds = tempBounds;
					managerOffset = 40;
					var tabBounds = holderView.Bounds;
					tabBounds.Height += 20;
					holderView.Bounds = tabBounds;
					Console.WriteLine("Now scrollview bounds are: " + infoScroller.Bounds);
				}
			}			

      tabManager = new UITabBar(new CGRect(0,holderView.Bounds.Height - managerOffset, holderView.Bounds.Width,60));

      editView = new EditJobView(infoScroller,frnJID);
      associateView = new JobSessionView(infoScroller,frnJID);
      notesView = new JobNotesView(infoScroller, frnJID);

      var infoTab = new UITabBarItem();
      infoTab.Tag = 0;
      infoTab.Image = UIImage.FromBundle("ic_small_edit");
      infoTab.Title = "Job Info";

      if (!frnJID.Equals(0)) {
        var sessionTab = new UITabBarItem();
        sessionTab.Tag = 1;
        sessionTab.Image = UIImage.FromBundle("ic_small_list");
        sessionTab.Title = "Sessions";

        var notesTab = new UITabBarItem();
        notesTab.Tag = 2;
        notesTab.Image = UIImage.FromBundle("ic_notes");
        notesTab.Title = "Notes";

        tabManager.Items = new UITabBarItem[]{ infoTab, sessionTab, notesTab };
      } else {
        tabManager.Items = new UITabBarItem[]{ infoTab };
      }

      tabManager.SelectedItem = infoTab;
      tabManager.ItemSelected += (sender, e) => {    
        switch(e.Item.Tag) {    
          case 0:
            NavigationItem.Title = "Edit Job";
            saveButton.Hidden = false;
            associateView.sessionView.Hidden = true;
            notesView.notesView.Hidden = true;
            editView.editView.Hidden = false;
            break;
          case 1:
            NavigationItem.Title = "Edit Sessions Links";    
						//infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, infoScroller.Bounds.Height);
            saveButton.Hidden = true;
            editView.editView.Hidden = true;
            notesView.notesView.Hidden = true;
            associateView.sessionView.Hidden = false;
            break;
          case 2:
            NavigationItem.Title = "Add Notes";
						//infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, infoScroller.Bounds.Height);
            saveButton.Hidden = true;
            editView.editView.Hidden = true;
            associateView.sessionView.Hidden = true;
            notesView.notesView.Hidden = false;
            break;
        }   
      };
      
      holderView.AddSubview(tabManager);
      infoScroller.AddSubview(editView.editView);      
      infoScroller.AddSubview(associateView.sessionView);
      infoScroller.AddSubview(notesView.notesView);
			holderView.BringSubviewToFront(tabManager);
		}		
		public override void ViewDidAppear(bool animated) {
			base.ViewDidAppear(animated);
			View.LayoutSubviews();
		}
    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


