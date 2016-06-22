namespace ION.IOS.ViewController.GaugeTesting {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using DSoft.Themes.Grid;
  using DSoft.Datatypes.Enums;
  using DSoft.Datatypes.Grid.Data;
  using DSoft.Datatypes.Types;
  using DSoft.Datatypes.Formatters;
  using DSoft.UI.Grid;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Internal;
  using ION.Core.Internal.Testing;
  using ION.Core.IO;
  using ION.Core.Sensors;
  using ION.Core.Util;

  public partial class InternalTestBenchViewController : BaseIONViewController {



    public InternalTestBenchViewController (IntPtr handle) : base (handle) {
      
		}

    /// <summary>
    /// Called when the test procedure meets a new test point.
    /// </summary>
    private void OnTargetPointMet(TestProcedure.TargetPoint tp) {
      for (int i = 0; i < sensors.Count; i++) {
        var sensor = sensors[i];
        table.SetValue(i, tp.target.amount + "", sensor.measurement.amount + "");
      }
    }

    private void Do(object sender, EventArgs e) {
      var view = new LoadingOverlay(View.Bounds, "Testing");
      View.Add(view);
      Task.Factory.StartNew(test.Run)
        .ContinueWith((t) => {
          view.Hide();
        });
    }

    /// <summary>
    /// Views the did load.
    /// </summary>
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      gridView.DataSource = table;
    }
	}


  public class LoadingOverlay : UIView {
    private UIActivityIndicatorView activitySpinner;
    private UILabel loadingLabel;

    public LoadingOverlay(CGRect frame, string message) : base(frame) {
      // configurable bits
      BackgroundColor = UIColor.Black;
      Alpha = 0.75f;
      AutoresizingMask = UIViewAutoresizing.All;

      nfloat labelHeight = 22;
      nfloat labelWidth = Frame.Width - 20;

      // derive the center x and y
      nfloat centerX = Frame.Width / 2;
      nfloat centerY = Frame.Height / 2;

      // create the activity spinner, center it horizontall and put it 5 points above center x
      activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
      activitySpinner.Frame = new CGRect (
        centerX - (activitySpinner.Frame.Width / 2) ,
        centerY - activitySpinner.Frame.Height - 20 ,
        activitySpinner.Frame.Width,
        activitySpinner.Frame.Height);
      activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
      AddSubview (activitySpinner);
      activitySpinner.StartAnimating ();

      // create and configure the "Loading Data" label
      loadingLabel = new UILabel(new CGRect (
        centerX - (labelWidth / 2),
        centerY + 20 ,
        labelWidth ,
        labelHeight
      ));
      loadingLabel.BackgroundColor = UIColor.Clear;
      loadingLabel.TextColor = UIColor.White;
      loadingLabel.Text = message;
      loadingLabel.TextAlignment = UITextAlignment.Center;
      loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
      AddSubview (loadingLabel);
    }

    /// <summary>
    /// Hide this instance.
    /// </summary>
    public void Hide() {
      UIView.Animate (
        0.5, // duration
        () => { Alpha = 0; },
        () => { RemoveFromSuperview(); }
      );
    }
  }
}
