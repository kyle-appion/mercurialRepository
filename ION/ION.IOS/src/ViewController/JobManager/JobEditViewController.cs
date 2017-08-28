using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;
using System.Threading.Tasks;
using System.Xml;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobEditViewController : BaseIONViewController {
    public JobEditViewController(IntPtr handle) : base(handle) {
    }

    EditJobView editView;
    JobSessionView associateView;
    //JobNotesView notesView;
    public UIButton saveButton;
    public int frnJID;
    IION ion;


    static nfloat loadHeight = 0;
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      ion = AppState.context;
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("CarbonBackground"));

			saveButton = new UIButton(new CGRect(0,0,50,40));
			saveButton.SetImage(UIImage.FromBundle("ic_device_add"), UIControlState.Normal);
      saveButton.Layer.BorderWidth = 1f;
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
				  //var job = new ION.Core.Database.JobRow() { jobName = editView.jobName.Text, customerNumber = editView.customerNumber.Text, dispatchNumber = editView.dispatchNumber.Text, poNumber = editView.prodOrderNumber.Text, techName = editView.techName.Text, systemType = editView.systemName.Text, jobAddress = editView.jobAddress.Text, jobLocation = editView.coordinateLabel.Text };
				  var job = new ION.Core.Database.JobRow(){jobName = editView.jobName.Text, customerNumber = editView.customerNumber.Text, dispatchNumber = editView.dispatchNumber.Text, poNumber = editView.prodOrderNumber.Text, techName = editView.techName.Text, systemType = editView.systemName.Text, jobAddress = editView.jobAddress.Text};         
            ion.database.Insert(job);
            this.NavigationController.PopViewController(true);
          } else {
            //TODO alert user that a job already exists with that name
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
						ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobName = ?, customerNumber = ?, dispatchNumber = ?, poNumber = ?, techName = ?, systemType = ?, jobAddress = ? WHERE JID = ?",editView.jobName.Text, editView.customerNumber.Text, editView.dispatchNumber.Text, editView.prodOrderNumber.Text, editView.techName.Text,editView.systemName.Text, editView.jobAddress.Text,frnJID);
          } else {
				  //TODO alert user that a job already exists with that name
			    }
        }

  		  editView.updateNotes(frnJID);
  		  using (XmlReader reader = XmlReader.Create(editView.fileDir)) {
  			  while (reader.Read()) {
  				  // Only detect start elements.
  				  if (reader.IsStartElement()) {
  					  // Get element name and switch on it.
  					  switch (reader.Name) {
  						  case "Info":
  						  // Search for the attribute name on this current node.
  						  string attribute = reader["Info"];
  						  if (attribute != null) {
  							  Console.WriteLine(" Has attribute name: " + attribute);
  						  }
  						  // Next read will contain text.
  						  if (reader.Read()) {
  							  editView.notes.Text = reader.Value.Trim();
  							  Console.WriteLine("Notes: ");
  							  Console.WriteLine(reader.Value.Trim());
  						  }
  						  break;
  					  }
  				  }
  			  }
  		  }
        NavigationController.PopViewController(true);
      };
      

      NavigationItem.Title = Util.Strings.Job.EDIT;

      var button = new UIBarButtonItem(saveButton);

      NavigationItem.RightBarButtonItem = button;
			AutomaticallyAdjustsScrollViewInsets = false;
			
  		setupLayout();
    }

		public async void setupLayout(){
			await Task.Delay(TimeSpan.FromMilliseconds(2));
			//Console.WriteLine("View dimensions " + View.Bounds);
			//Console.WriteLine("Holder dimensions " + holderView.Bounds);
			//Console.WriteLine("Scroller dimensions " + infoScroller.Bounds);
			jobInfoButton = new UIButton(new CGRect(0, 40, .5 * View.Bounds.Width, 40));
      jobInfoButton.BackgroundColor = UIColor.White;
      jobInfoButton.SetTitle("Job Info",UIControlState.Normal);
      jobInfoButton.SetTitleColor(UIColor.Black,UIControlState.Normal);
      jobInfoButton.Font = UIFont.BoldSystemFontOfSize(15f);
      jobInfoHighlight = new UILabel(new CGRect(0, 80, .5 * View.Bounds.Width, 5));
      jobInfoHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);
			dataLogginButton = new UIButton(new CGRect(.5 * View.Bounds.Width,40,.5 * View.Bounds.Width,40));
			dataLogginButton.BackgroundColor = UIColor.LightGray;
			dataLogginButton.SetTitle("Data Logging", UIControlState.Normal);
			dataLogginButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			dataLogginButton.Font = UIFont.BoldSystemFontOfSize(15f);
			dataLoggingHighlight = new UILabel(new CGRect(.5 * View.Bounds.Width, 80, .5 * View.Bounds.Width, 5));
			dataLoggingHighlight.BackgroundColor = UIColor.Black;

			editView = new EditJobView(infoScroller,frnJID);
      infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, 1.5 * infoScroller.Bounds.Height);
      associateView = new JobSessionView(infoScroller,frnJID);
      //notesView = new JobNotesView(infoScroller, frnJID);

      var infoTab = new UITabBarItem();
      infoTab.Tag = 0;
      infoTab.Image = UIImage.FromBundle("ic_small_edit");
      infoTab.Title = Util.Strings.Job.JOBINFO;

      jobInfoButton.TouchUpInside += (sender, e) => {
        jobInfoButton.BackgroundColor = UIColor.White;
        jobInfoHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

  		  dataLogginButton.BackgroundColor = UIColor.LightGray;
  		  dataLoggingHighlight.BackgroundColor = UIColor.Black;

  		  NavigationItem.Title = Util.Strings.Job.EDIT;
  			infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, 1.5 * infoScroller.Bounds.Height);

  		  saveButton.Hidden = false;
  		  associateView.sessionView.Hidden = true;
  		  //notesView.notesView.Hidden = true;
  		  editView.editView.Hidden = false;
      };

      dataLogginButton.TouchUpInside += (sender, e) => {
		    jobInfoButton.BackgroundColor = UIColor.LightGray;
		    jobInfoHighlight.BackgroundColor = UIColor.Black;

  		  dataLogginButton.BackgroundColor = UIColor.White;
  		  dataLoggingHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

  		  NavigationItem.Title = Util.Strings.Job.EDITSESSIONS;
  		  infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, infoScroller.Bounds.Height);
  		  saveButton.Hidden = true;
  		  editView.editView.Hidden = true;
  		  //notesView.notesView.Hidden = true;
  		  associateView.sessionView.Hidden = false;
      };

			View.AddSubview(jobInfoButton);
			View.AddSubview(jobInfoHighlight);
			View.AddSubview(dataLogginButton);
			View.AddSubview(dataLoggingHighlight);

      infoScroller.AddSubview(editView.editView);      
      infoScroller.AddSubview(associateView.sessionView);
      //infoScroller.AddSubview(notesView.notesView);
		}
		
		public void adjustContentSize(object sender, EventArgs e){
			if(editView.expanded){
				infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, 1.5 * infoScroller.Bounds.Height);
			} else {
				infoScroller.ContentSize = new CGSize(infoScroller.Bounds.Width, infoScroller.Bounds.Height);
			}
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


