using System;
using System.Linq;
using System.Windows;
using UIKit;
using CoreGraphics;

using ION.Core.App;
using ION.Core.Database;

using SQLite;


namespace ION.IOS.ViewController.Logging {
  public partial class SessionCell : UITableViewCell {
    
    public int SID;
    public DateTime start;
    public DateTime finish;
		public UILabel recordDateLabel;
		public UILabel durationLabel;
		public UILabel deviceCountLabel;
		public UILabel dateValueLabel;
		public UILabel durationValueLabel;
		public UILabel deviceCountValueLabel;

		public UIImageView checkImage;
    public IION ion;

    public SessionCell(IntPtr handle ) {
      
    }

    public SessionCell(){
      
    }

    public void makeCellData(int session, DateTime begin, DateTime end, UITableView tableView, nfloat cellHeight){
      SID = session;
      start = begin.ToLocalTime();
      finish = end.ToLocalTime();
      ion = AppState.context;

			var deviceAmount = ion.database.Table<SensorMeasurementRow>()
			  .Where(smr => smr.frn_SID == SID)
			  .Select(smr => smr.serialNumber).Distinct()
			  .Count();

			var duration = end.Subtract(begin).TotalMinutes;

			recordDateLabel = new UILabel(new CGRect(5, 0, .32 * tableView.Bounds.Width, .33 * cellHeight));
			recordDateLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			recordDateLabel.Text = "Date Recorded:";

			durationLabel = new UILabel(new CGRect(5, .33 * cellHeight, .32 * tableView.Bounds.Width, .33 * cellHeight));
			durationLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			durationLabel.Text = "Duration:";

			deviceCountLabel = new UILabel(new CGRect(5, .66 * cellHeight, .32 * tableView.Bounds.Width, .33 * cellHeight));
			deviceCountLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			deviceCountLabel.Text = "# of Sensors";

			dateValueLabel = new UILabel(new CGRect(.35 * tableView.Bounds.Width, 0, .5 * tableView.Bounds.Width, .33 * cellHeight));
			dateValueLabel.Text = start.Date.ToString(@"yyyy-MM-dd") + " | " + start.ToLocalTime().ToShortTimeString();

			durationValueLabel = new UILabel(new CGRect(.35 * tableView.Bounds.Width, .33 * cellHeight, .5 * tableView.Bounds.Width, .33 * cellHeight));
			durationValueLabel.Text = duration.ToString("0.0") + " min";

			deviceCountValueLabel = new UILabel(new CGRect(.35 * tableView.Bounds.Width, .66 * cellHeight, .5 * tableView.Bounds.Width, .33 * cellHeight));
			deviceCountValueLabel.Text = deviceAmount.ToString();

			checkImage = new UIImageView(new CGRect(.9 * tableView.Bounds.Width, .3 * cellHeight, .4 * cellHeight, .4 * cellHeight));

			this.AddSubview(recordDateLabel);
			this.AddSubview(durationLabel);
			this.AddSubview(deviceCountLabel);

			this.AddSubview(dateValueLabel);
			this.AddSubview(durationValueLabel);
			this.AddSubview(deviceCountValueLabel);

			this.AddSubview(checkImage);
    }
      
  }
}

