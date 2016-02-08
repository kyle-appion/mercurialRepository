namespace ION.Droid.Tasks {
  
  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.OS;

  using ION.Core.App;
  using ION.Core.Devices;

  /// <summary>
  /// A simple UI task that will download and format calibration certificates for the given devices.
  /// </summary>
  public class DownloadCalibrationCertificateTask {

    /// <summary>
    /// The context that is used to poll android stuff and post the dialog to.
    /// </summary>
    private Context context;
    /// <summary>
    /// The application state.
    /// </summary>
    private IION ion;
    /// <summary>
    /// The dialog that will show the current progress of the download.
    /// </summary>
    private ProgressDialog dialog;

    public DownloadCalibrationCertificateTask(Context context, IION ion) {
      this.context = context;
      this.ion = ion;
    }

    /// <summary>
    /// Called immediately before the task is executed.
    /// </summary>
    public void PreExecute() {
      dialog = new ProgressDialog(context);
      dialog.SetTitle(Resource.String.downloading);
    }

    /// <summary>
    /// Executes the task to download the serial number's calibration certificates.
    /// </summary>
    /// <param name="serialNumbers">Serial numbers.</param>
    public void Execute(List<ISerialNumber> serialNumbers) {
    }
  }
}

