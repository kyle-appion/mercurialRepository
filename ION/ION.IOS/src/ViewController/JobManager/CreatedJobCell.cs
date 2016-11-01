using System;
using UIKit;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class CreatedJobCell : UITableViewCell {

    IION ion;
    public UILabel cellHeader;
    public UILabel jobDetails;

    public CreatedJobCell(IntPtr handle) {
      
    }

    public CreatedJobCell(){
      
    }

    public void makeCellData(int JobID, double cellWidth, double cellHeight){
      ion = AppState.context;

      //cellHeader = new UILabel(new CGRect(0,0,cellWidth,.1 * cellHeight));
      //cellHeader.TextAlignment = UITextAlignment.Center;
      //cellHeader.AdjustsFontSizeToFitWidth = true;
      //cellHeader.TextColor = UIColor.White;
      //cellHeader.BackgroundColor = UIColor.Black;

      //jobDetails = new UILabel(new CGRect(0,.1 * cellHeight, cellWidth, .9 * cellHeight));
      jobDetails = new UILabel(new CGRect(0,0, cellWidth, cellHeight));
      jobDetails.Font = UIFont.BoldSystemFontOfSize(30);
      jobDetails.BackgroundColor = UIColor.White;
      jobDetails.AdjustsFontSizeToFitWidth = true;
      jobDetails.Lines = 0;

      var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT * FROM JobRow WHERE JID = ?", JobID);

      var detailString = "";
      foreach (var job in jobQuery) {
        detailString += " Job Name: " + job.jobName;

        detailString += "\n Customer #: ";
        if (job.customerNumber != null) {
          detailString += job.customerNumber;
        } else {
          detailString += "N/A";
        }
        detailString += "\n Dispatch #: ";
        if (job.dispatchNumber != null) {
          detailString += job.dispatchNumber;
        } else {
          detailString += "N/A";
        }
        detailString += "\n Purchase Order #: ";
        if (job.poNumber != null) {
          detailString += job.poNumber;
        } else {
          detailString += "N/A";
        }
      }

      jobDetails.Text = detailString;

      //this.AddSubview(cellHeader);
      this.AddSubview(jobDetails);
    }
  }
}

