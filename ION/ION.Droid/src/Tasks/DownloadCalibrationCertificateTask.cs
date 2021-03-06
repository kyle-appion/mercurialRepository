﻿namespace ION.Droid.Tasks {
  
  using System;
  using System.Collections.Generic;
  using System.Text;
	using System.Threading.Tasks;

  using Android.App;
  using Android.Content;

	using Appion.Commons.Util;

  using ION.Core.App;
	using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Devices.Certificates;
  using ION.Core.IO;
  using ION.Core.Net;

  using ION.Droid.Dialog;

  /// <summary>
  /// A simple UI task that will download and format calibration certificates for the given devices.
  /// </summary>
  public class DownloadCalibrationCertificateTask : IONTask<ISerialNumber, string, List<ISerialNumber>> {

    /// <summary>
    /// The action that is called when the download is complete.
    /// </summary>
    /// <value>The on completed.</value>
    public Action onCompleted { get; set; }

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
    /// Raises the pre execute event.
    /// </summary>
    protected override void OnPreExecute() {
      dialog = new ProgressDialog(context);
      dialog.SetTitle(Resource.String.report_certificates_downloading);
      dialog.SetMessage(context.GetString(Resource.String.please_wait));
      dialog.Indeterminate = true;
      dialog.Show();
    }

    /// <Docs>To be added.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Dos the in background.
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    protected override List<ISerialNumber> DoInBackground(List<ISerialNumber> parameters) {
      var ret = new List<ISerialNumber>();

			try {
	      var task = new RequestCalibrationCertificates(ion, parameters.ToArray()).Request();
	      task.Wait();

	      foreach (var cr in task.Result) {
	        Log.D(this, "Resolving result: " + cr.serialNumber);
	        if (cr.success) {
	          var file = ion.calibrationCertificateFolder.GetFile(cr.serialNumber + " Certification.pdf", EFileAccessResponse.ReplaceIfExists);
						UpdateLastNistDate(cr.certificate).Wait();
	          try {
	            using (var s = file.OpenForWriting()) {
	              GaugeDeviceCertificatePdfExporter.Export(ion, cr.certificate, s);
	            }
	          } catch (Exception e) {
	            Log.E(this, "Failed to export calibration pdf for " + cr.serialNumber, e);
	            file.Delete();
	            ret.Add(cr.serialNumber);
	          }
	        } else {
	          ret.Add(cr.serialNumber);
	        }
	      }

	      return ret;
			} catch (Exception e) {
				Log.E(this, "Failed to request calibration certificates.", e);
				return parameters;
			}
    }

		/// <summary>
		/// Stores the cert calibration date into the database so it can be used in reporting.
		/// </summary>
		/// <param name="cert">Cert.</param>
		private async Task<bool> UpdateLastNistDate(GaugeDeviceCalibrationCertificate cert) {
			var table = ion.database.Table<LoggingDeviceRow>();

			var row = table.Where(ldr => ldr.serialNumber == cert.testSerialNumber.ToString()).FirstOrDefault();
			if (row == null) {
				// The logging device row does not exist in the database for the given serial number. We will need to add it
				row = new LoggingDeviceRow() {
					serialNumber = cert.testSerialNumber.ToString(),
					nistDate = cert.lastTestCalibrationDate.ToShortDateString(),
				};
				return await ion.database.SaveAsync<LoggingDeviceRow>(row);
			} else {
				// The logging device row exists in the database. Pull it and update the date.
				row.nistDate = cert.lastTestCalibrationDate.ToShortDateString();
				return await ion.database.SaveAsync<LoggingDeviceRow>(row);
			}
		}

    /// <Docs>To be added.</Docs>
    /// <remarks>To be added.</remarks>
    /// <summary>
    /// Raises the post execute event.
    /// </summary>
    /// <param name="result">Result.</param>
    protected override void OnPostExecute(List<ISerialNumber> result) {
      Log.D(this, "Finished downloading");
      dialog.Dismiss();

      var sb = new StringBuilder();

      if (result.Count > 0) {
        for (int i = 0; i < result.Count - 1; i++) {
          sb.Append(result[i].ToString()).Append(", ");
        }
        sb.Append(result[result.Count - 1]);
			}

      var d = new IONAlertDialog(context, Resource.String.download_error);
      d.SetMessage(string.Format(context.GetString(Resource.String.report_certificates_error_download_fails_1sarg, sb.ToString())));
      d.SetNegativeButton(Resource.String.ok, (obj, args) => {
        var dialog = obj as Dialog;
        if (dialog != null) {
          dialog.Dismiss();
        }
      });
      d.Show();

      if (onCompleted != null) {
        onCompleted();
      }
    }
  }
}

