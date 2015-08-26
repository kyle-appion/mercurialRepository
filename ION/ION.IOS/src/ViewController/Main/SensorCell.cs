// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Sensors;
using ION.IOS.UI;

namespace ION.IOS.ViewController.Main {
	public partial class SensorCell : UITableViewCell, IReleasable {

    /// <summary>
    /// The action that will be called when the workbench button
    /// is clicked.
    /// </summary>
    public Action onWorkbenchClicked {
      get {
        return __onWorkbenchClicked;
      }
      set {
        __onWorkbenchClicked = value;
        buttonWorkbench.Hidden = __onWorkbenchClicked == null;
      }
    } Action __onWorkbenchClicked;
    /// <summary>
    /// The action that will be called when the analyzer button
    /// is clicked.
    /// </summary>
    /// <value>The on analyzer clicked.</value>
    public Action onAnalyzerClicked { 
      get {
        return __onAnalyzerClicked;
      }
      set {
        __onAnalyzerClicked = value;
        buttonAnalyzer.Hidden = __onAnalyzerClicked == null;
      }
    } Action __onAnalyzerClicked;
    /// <summary>
    /// The action that will be called when the add button is
    /// clicked.
    /// </summary>
    /// <value>The on add clicked.</value>
    public Action onAddClicked {
      get {
        return __onAddClicked;
      } 
      set {
        __onAddClicked = value;
        buttonAdd.Hidden = __onAddClicked == null;
      }
    } Action __onAddClicked;
    /// <summary>
    /// The sensor that is providing real time content updates to this view.
    /// </summary>
    /// <value>The sensor.</value>
    private Sensor sensor { 
      get {
        return __sensor;
      }
      set {
        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent -= OnSensorUpdated;
        }

        __sensor = value;

        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent += OnSensorUpdated;
          OnSensorUpdated(__sensor);
        }
      }
    } Sensor __sensor;

		public SensorCell (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from IReleasable
    public void Release() {
      sensor = null;
    }

    // Overriden from UITableCellView
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonWorkbench.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonWorkbench.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Highlighted);
      buttonWorkbench.TouchUpInside += (object sender, EventArgs e) => {
        if (onWorkbenchClicked != null) {
          onWorkbenchClicked();
        }
      };

      buttonAnalyzer.Hidden = true; // TODO ahodder@appioninc.com: Hidden until analyzer is implemented.
      buttonAnalyzer.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonAnalyzer.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Highlighted);
      buttonAnalyzer.TouchUpInside += (object sender, EventArgs e) => {
        if (onAnalyzerClicked != null) {
          onAnalyzerClicked();
        }
      };

      buttonAdd.Hidden = true; // TODO ahodder@appioninc.com: Hidden until viewcontroller call support is working.
      buttonAdd.SetImage(UIImage.FromBundle("ic_device_add"), UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Highlighted);
      buttonAdd.TouchUpInside += (object sender, EventArgs e) => {
        if (onAddClicked != null) {
          onAddClicked();
        }
      };
    }

    // Overridden from UITabelCellView
    public override void PrepareForReuse() {
      base.PrepareForReuse();
      sensor = null;
    }

    /// <summary>
    /// Updates the view to the given sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void Update(Sensor sensor) {
      this.sensor = sensor;
    }

    /// <summary>
    /// The delegate that is called when the view's attached sensor updates.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorUpdated(Sensor sensor) {
      Log.D(this, sensor.measurement + "");
      labelSensorType.Text = sensor.type.GetTypeString();
      labelSensorMeasurement.Text = sensor.measurement.ToString();
    }
  }
}
