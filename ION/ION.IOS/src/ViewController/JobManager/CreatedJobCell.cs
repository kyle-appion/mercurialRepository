using System;
using UIKit;
using CoreGraphics;

using ION.Core.App;
using System.Drawing;
using Foundation;
using static ION.IOS.ViewController.JobManager.JobViewController;

namespace ION.IOS.ViewController.JobManager {
  public class CreatedJobCell : UITableViewCell {
		/// <summary>
		/// The action that will be fired when the user selects a job to be active
		/// </summary>
		public SetActiveJob clickedJobActive { get; set; }

    IION ion;
		public UILabel jobIDLabel;
		public UILabel jobNameLabel;
		public UILabel customerNumberLabel;
		public UILabel dispatchNumberLabel;
		public UILabel poNumberLabel;

		public UILabel idValueLabel;
		public UILabel nameValueLabel;
		public UILabel customerValueLabel;
		public UILabel dispatchValueLabel;
		public UILabel poValueLabel;

		public UIButton editJobImage;
		public UIImage scaleImage;

    public CreatedJobCell(IntPtr handle) {
      
    }

    public CreatedJobCell(){
      
    }

    public void makeCellData(int JobID, double cellWidth, double cellHeight, SetActiveJob chooseActiveJob){
      Console.WriteLine("Get cell called to make celldata");
      ion = AppState.context;
      clickedJobActive = chooseActiveJob;

			jobIDLabel = new UILabel(new CGRect(5, 5, (.4 * cellWidth) - 5, (.19 * cellHeight) - 5));
      jobIDLabel.Text = "Job ID:";
      jobIDLabel.Font = UIFont.BoldSystemFontOfSize(15f);
			jobNameLabel = new UILabel(new CGRect(5, (.19 * cellHeight) + 5, (.4 * cellWidth) - 5, (.19 * cellHeight) - 5));
      jobNameLabel.Text = Util.Strings.Job.JOBNAME+":";
			jobNameLabel.Font = UIFont.BoldSystemFontOfSize(15f);
			customerNumberLabel = new UILabel(new CGRect(5, (.38 * cellHeight) + 5, (.4 * cellWidth) - 5, (.19 * cellHeight) - 5));
			customerNumberLabel.Text = Util.Strings.Job.CUSTOMERSIGN+ ":";
			customerNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);
			dispatchNumberLabel = new UILabel(new CGRect(5, (.57 * cellHeight) + 5, (.4 * cellWidth) - 5, (.19 * cellHeight) - 5));
			dispatchNumberLabel.Text = Util.Strings.Job.DISPATCHSIGN+ ":";
			dispatchNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);
			poNumberLabel = new UILabel(new CGRect(5, (.76 * cellHeight) + 5, (.4 * cellWidth) - 5,(.19 * cellHeight) - 5));
			poNumberLabel.Text = Util.Strings.Job.POSIGN+ ":";
			poNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);

      idValueLabel = new UILabel(new CGRect(.4 * cellWidth, 5,.4 * cellWidth, (.19 * cellHeight) - 5));
      nameValueLabel = new UILabel(new CGRect(.4 * cellWidth, (.19 * cellHeight) + 5, .4 * cellWidth, (.19 * cellHeight) - 5));
      customerValueLabel = new UILabel(new CGRect(.4 * cellWidth, (.38 * cellHeight) + 5, .4 * cellWidth, (.19 * cellHeight) - 5));
      dispatchValueLabel = new UILabel(new CGRect(.4 * cellWidth, (.57 * cellHeight) + 5, .4 * cellWidth, (.19 * cellHeight) - 5));
      poValueLabel = new UILabel(new CGRect(.4 * cellWidth, (.76 * cellHeight) + 5, .4 * cellWidth, (.19 * cellHeight) - 5));

      editJobImage = new UIButton(new CGRect(.84 * cellWidth,.25 * cellHeight,.5 * cellHeight, .5 * cellHeight));
      editJobImage.SetImage(UIImage.FromBundle("ic_bookmark").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),UIControlState.Normal);

			if (JobID == ion.preferences.jobs.activeJob) {
				editJobImage.TintColor = UIColor.Yellow;
      } else {
				editJobImage.TintColor = UIColor.Black;
			}

      editJobImage.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      editJobImage.Layer.CornerRadius = 5f;
      editJobImage.ContentMode = UIViewContentMode.Center;   

      editJobImage.TouchUpInside += (sender, e) => {
  		 if (ion.preferences.jobs.activeJob == JobID) {
  		    Console.WriteLine("Unhooking job id " + JobID + " as active job");
          ion.preferences.jobs.activeJob = 0;
          editJobImage.TintColor = UIColor.Black;
  	    } else {
  		    Console.WriteLine("Setting active job to " + JobID);
  		    ion.preferences.jobs.activeJob = JobID;
  		    editJobImage.TintColor = UIColor.Yellow;
  	    }
        clickedJobActive();
      };

			var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT * FROM JobRow WHERE JID = ?", JobID);

      foreach (var job in jobQuery) {
        idValueLabel.Text = job.JID.ToString();
        nameValueLabel.Text = job.jobName;
        customerValueLabel.Text = job.customerNumber;
        dispatchValueLabel.Text = job.dispatchNumber;
        poValueLabel.Text = job.poNumber;
      }

			this.AddSubview(jobIDLabel);
			this.AddSubview(jobNameLabel);
			this.AddSubview(customerNumberLabel);
			this.AddSubview(dispatchNumberLabel);
			this.AddSubview(poNumberLabel);

			this.AddSubview(idValueLabel);
			this.AddSubview(nameValueLabel);
			this.AddSubview(customerValueLabel);
			this.AddSubview(dispatchValueLabel);
			this.AddSubview(poValueLabel);

			this.AddSubview(editJobImage);
    }
  }
}

