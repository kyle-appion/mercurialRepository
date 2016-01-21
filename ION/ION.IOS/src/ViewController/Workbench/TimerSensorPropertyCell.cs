namespace ION.IOS.ViewController.Workbench {

  using System;

  using Foundation;
  using UIKit;

  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.IOS.Util;

  public class TimerRecord : SensorPropertyRecord {
    // Overridden from IWorkbenchSourceRecord
    public override WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.Timer;
      }
    }

    public TimerSensorProperty timer { get; set; }

    public TimerRecord(Manifold manifold, TimerSensorProperty sensorProperty) : base(manifold, sensorProperty) {
      this.timer = sensorProperty;
    }
  }

	public partial class TimerSensorPropertyCell : UITableViewCell {

    private NSTimer uiUpdateTimer { get; set; }

    private TimerRecord timer {
      get {
        return __timer;
      }
      set {
        if (__timer != null) {
          __timer.timer.onSensorPropertyChanged -= OnSensorPropertyChanged;
        }

        __timer = value;

        if (__timer != null) {
          __timer.timer.onSensorPropertyChanged += OnSensorPropertyChanged;
          OnSensorPropertyChanged(__timer.timer);
          uiUpdateTimer = NSTimer.CreateRepeatingTimer(TimeSpan.FromSeconds(1/3.0), (action) => { Update(); });
          NSRunLoop.Current.AddTimer(uiUpdateTimer, NSRunLoopMode.Common);
        } else {
          uiUpdateTimer.Invalidate();
          uiUpdateTimer.Dispose();
          uiUpdateTimer = null;
        }
      }
    } TimerRecord __timer;

		public TimerSensorPropertyCell (IntPtr handle) : base (handle) {
		}

    // Overridden from AwakeFromNib
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonReset.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
      buttonReset.TouchUpInside += (object sender, EventArgs e) => {
        if (timer != null) {
          timer.timer.Reset();
        }
      };

      buttonPlayPause.SetImage(UIImage.FromBundle("ic_play"), UIControlState.Normal);
      buttonPlayPause.TouchUpInside += (object sender, EventArgs e) => {
        if (timer != null) {
          if (timer.timer.isStarted) {
            buttonPlayPause.SetImage(UIImage.FromBundle("ic_play"), UIControlState.Normal);
            timer.timer.Stop();
          } else {
            buttonPlayPause.SetImage(UIImage.FromBundle("ic_pause"), UIControlState.Normal);
            timer.timer.Start();
          }
        }
      };

      labelTitle.Text = Strings.Workbench.Viewer.TIMER;
    }

    public void UpdateTo(TimerRecord timer) {
      this.timer = timer;
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      Update();
    }

    private void Update() {
      var time = timer.timer.ellapsedTime;
      labelMeasurement.Text = String.Format("{0}:{1}", time.Minutes.ToString("00"), time.Seconds.ToString("00"));
    }
	}
}
