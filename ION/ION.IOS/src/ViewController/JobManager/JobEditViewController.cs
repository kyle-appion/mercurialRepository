using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobEditViewController : BaseIONViewController {
    public JobEditViewController(IntPtr handle) : base(handle) {
    }

    UITabBar tabManager;
    EditJobView editView;
    JobSessionView associateView;
    public UIButton saveButton;
    public int frnJID;
    IION ion;

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
          var job = new ION.Core.Database.JobRow(){jobName = editView.jobName.Text, customerNumber = editView.customerNumber.Text, dispatchNumber = editView.dispatchNumber.Text, poNumber = editView.prodOrderNumber.Text};         
          ion.database.Insert(job);
          this.NavigationController.PopViewController(true);
        } else {
          ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobName = ?, customerNumber = ?, dispatchNumber = ?, poNumber = ? WHERE JID = ?",editView.jobName.Text, editView.customerNumber.Text, editView.dispatchNumber.Text, editView.prodOrderNumber.Text,frnJID);
          this.NavigationController.PopViewController(true);
        }

        editView.confirmLabel.Hidden = false;
      };

      NavigationItem.Title = "Edit Job";

      var button = new UIBarButtonItem(saveButton);

      NavigationItem.RightBarButtonItem = button;
      tabManager = new UITabBar(new CGRect(0,View.Bounds.Height - 70, View.Bounds.Width,60));

      editView = new EditJobView(View,frnJID);
      associateView = new JobSessionView(View,tabManager,UIApplication.SharedApplication.StatusBarFrame.Size.Height * 2,frnJID);

      var tab1 = new UITabBarItem();
      tab1.Tag = 0;
      tab1.Image = UIImage.FromBundle("ic_small_edit");
      tab1.Title = "Job Info";

      if (!frnJID.Equals(0)) {
        var tab2 = new UITabBarItem();
        tab2.Tag = 1;
        tab2.Image = UIImage.FromBundle("ic_small_list");
        tab2.Title = "Sessions";

        tabManager.Items = new UITabBarItem[]{ tab1, tab2 };
      } else {
        tabManager.Items = new UITabBarItem[]{ tab1 };
      }

      tabManager.SelectedItem = tab1;
      tabManager.ItemSelected += (sender, e) => {
        switch(e.Item.Tag) {
          case 0:
            NavigationItem.Title = "Edit Job";
            saveButton.Hidden = false;
            associateView.sessionView.Hidden = true;
            editView.editView.Hidden = false;
            break;
          case 1:
            NavigationItem.Title = "Edit Sessions Links";
            saveButton.Hidden = true;
            editView.editView.Hidden = true;
            associateView.sessionView.Hidden = false;
            break;
        }
      };

      View.AddSubview(tabManager);
      View.AddSubview(editView.editView);
      View.AddSubview(associateView.sessionView);
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


