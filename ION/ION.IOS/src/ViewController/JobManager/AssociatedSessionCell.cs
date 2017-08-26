using System;
using UIKit;
using CoreGraphics;

using ION.Core.App;
using ION.Core.Database;
using System.Linq;

namespace ION.IOS.ViewController.JobManager {
  public class AssociatedSessionCell : UITableViewCell {
		public UILabel recordDateLabel;
		public UILabel durationLabel;
		public UILabel deviceCountLabel;
		public UILabel dateValueLabel;
		public UILabel durationValueLabel;
		public UILabel deviceCountValueLabel;

    public UIImageView checkImage;


    IION ion;
    public AssociatedSessionCell(IntPtr handle) {
      ion = AppState.context;
    }

    public AssociatedSessionCell() {
      
    }

    public void SetupCell(ION.IOS.ViewController.Logging.SessionData data, double cellHeight, double cellWidth){


			var deviceAmount = ion.database.Table<SensorMeasurementRow>()
			  .Where(smr => smr.frn_SID == data.SID)
			  .Select(smr => smr.serialNumber).Distinct()
			  .Count();

      var duration = data.finish.Subtract(data.start).TotalMinutes;

      recordDateLabel = new UILabel(new CGRect(5, 0, .32 * cellWidth, .33 * cellHeight));
      recordDateLabel.Font = UIFont.BoldSystemFontOfSize(16f);
      recordDateLabel.Text = "Date Recorded:";

			durationLabel = new UILabel(new CGRect(5, .33 * cellHeight, .32 * cellWidth, .33 * cellHeight));
			durationLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			durationLabel.Text = "Duration:";

			deviceCountLabel = new UILabel(new CGRect(5, .66 * cellHeight, .32 * cellWidth, .33 * cellHeight));
			deviceCountLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			deviceCountLabel.Text = "# of Sensors";

			dateValueLabel = new UILabel(new CGRect(.35 * cellWidth, 0, .5 * cellWidth, .33 * cellHeight));
			dateValueLabel.Text = data.start.Date.ToString(@"yyyy-MM-dd") + " | " + data.start.ToLocalTime().ToShortTimeString();

			durationValueLabel = new UILabel(new CGRect(.35 * cellWidth, .33 * cellHeight, .5 * cellWidth, .33 * cellHeight));
      durationValueLabel.Text = duration.ToString("0.0") + " min";

			deviceCountValueLabel = new UILabel(new CGRect(.35 * cellWidth, .66 * cellHeight, .5 * cellWidth, .33 * cellHeight));
			deviceCountValueLabel.Text = deviceAmount.ToString();

      checkImage = new UIImageView(new CGRect(.9 * cellWidth, .3 * cellHeight, .4 * cellHeight, .4 * cellHeight));

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

