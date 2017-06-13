namespace ION.IOS.ViewController.Workbench {

  using System;

  using UIKit;

	using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  public class MeasurementRecord : SensorPropertyRecord {
    public override WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.Measurement;
      }
    }

    public MeasurementRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
    }
  }

	public partial class MeasurementSensorPropertyTableCell : UITableViewCell, IReleasable {

    public Action onClick { get; set; }

    private MeasurementRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
        }

        __record = value;

        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
          OnSensorPropertyChanged(__record.sensorProperty);
        }
      }
    } MeasurementRecord __record;

    private EventHandler<ISensorProperty> onIconClicked { get; set; }

		public MeasurementSensorPropertyTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonIcon.TouchUpInside += (object sender, EventArgs e) => {
        if (onIconClicked != null) {
          if(record != null){
            onIconClicked(this, record.sensorProperty);
          }
        }
      };
    }

    // Overridden from UITableViewCell
    public override void PrepareForReuse() {
      base.PrepareForReuse();

      Release();
    }

    // Overridden from UITableViewCell
    public override void RemoveFromSuperview() {
      base.RemoveFromSuperview();

      Release();
    }

    // Overridden from IReleasable
    public void Release() { 
      record = null;
    }

    public void UpdateTo(MeasurementRecord measurementRecord, string title, string icon, EventHandler<ISensorProperty> iconClicked) {
    	this.ClipsToBounds = true;
    	this.Layer.BorderWidth = 1f;

    	buttonIcon.Layer.BorderWidth = 1f;
    	buttonIcon.Layer.BorderColor = UIColor.Black.CGColor;
    	buttonIcon.ClipsToBounds = true;
    	
      record = measurementRecord;
      onIconClicked = iconClicked;
      labelTitle.Text = title;
      buttonIcon.SetImage(UIImage.FromBundle(icon), UIControlState.Normal);
      buttonIcon.Hidden = viewDivider.Hidden = !record.sensorProperty.supportedReset;
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      labelMeasurement.Text = SensorUtils.ToFormattedString(sensorProperty.sensor.type, sensorProperty.modifiedMeasurement, true);
    }
	}
}
