﻿namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.Net;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Devices;
  using ION.Core.IO;
  using ION.Core.Util;

  using ION.Droid.Dialog;
  using ION.Droid.Fragments;

  [Activity(Label = "CalibrationCertificateArchiveActvity", Theme="@style/TerminalActivityTheme")]      
  public class CalibrationCertificateArchiveActvity : IONActivity {

    /// <summary>
    /// The fragment that will allow the user to navigate the calibration certificate directory.
    /// </summary>
    private FileManagerFragment fragment;

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_calibration_certificate_archive);

      fragment = FragmentManager.FindFragmentById<FileManagerFragment>(Resource.Id.content);
      fragment.folder = ion.calibrationCertificateFolder;
      fragment.onFileClicked += OnFileClicked;
      fragment.onFolderClicked += OnFolderClicked;
    }

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.download, menu);

      return true;
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.download:
          RequestDownloadCalibrationCertificates();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Downloads the calibration certificates.
    /// </summary>
    private void RequestDownloadCalibrationCertificates() {
      var adb = new IONAlertDialog(this);
      adb.SetTitle(Resource.String.report_certificates_download);
      adb.SetMessage(Resource.String.report_certificates_download_request);
      adb.SetNegativeButton(Resource.String.cancel, (obj, args) => {
        var dialog = obj as Dialog;
        if (dialog != null) {
          dialog.Dismiss();
        }
      });

      adb.SetPositiveButton(Resource.String.ok, (obj, args) => {
        var dialog = obj as Dialog;
        if (dialog != null) {
          dialog.Dismiss();
        }

        if (HasInternetConnection()) {
          var serials = new List<ISerialNumber>();

          foreach (var d in ion.deviceManager.devices) {
            serials.Add(d.serialNumber);
          }

          DownloadCalibrationCertificates(serials);
        } else {
          Alert(Resource.String.error_no_internet_connection);
        }
      });
    }

    /// <summary>
    /// Attempts to download all of the application's calibration certificates.
    /// </summary>
    private void DownloadCalibrationCertificates(List<ISerialNumber> serials) {
      var pd = new ProgressDialog(this);
      pd.SetTitle(Resource.String.downloading);
      pd.SetMessage(GetString(Resource.String.please_wait));
      
    }

    /// <summary>
    /// Queries whether or not the application has an internet connection.
    /// </summary>
    /// <returns><c>true</c> if this instance has internet connection; otherwise, <c>false</c>.</returns>
    private bool HasInternetConnection() {
      var cm = GetSystemService(Context.ConnectivityService) as ConnectivityManager;
      if (cm != null) {
        return cm.ActiveNetworkInfo != null;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Starts an activity that will allow the user to view the given pdf.
    /// </summary>
    private void StartPdfActivity(IFile file) {
      // TODO ahodder@appioninc.com: We will need to start our own activity if the user does not have their own pdf viewer
      try {
        Intent i = new Intent(Intent.ActionView);
        i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)), "application/pdf");
        i.SetFlags(ActivityFlags.NoHistory);
        StartActivity(Intent.CreateChooser(i, GetString(Resource.String.open_with)));
      } catch (Exception e) {
        Log.E(this, "Failed to start PDF activity chooser", e);
        var adb = new IONAlertDialog(this);
        adb.SetTitle(Resource.String.error_failed_to_open_file);
        adb.SetMessage(Resource.String.error_pdf_viewer_missing);
        adb.SetNegativeButton(Resource.String.cancel, (obj, args) => {
          var dialog = obj as Dialog;
          if (dialog != null) {
            dialog.Dismiss();
          }
        });
      }
    }

    /// <summary>
    /// Called when the user clicks a file in the file manager fragment.
    /// </summary>
    /// <param name="file">File.</param>
    private void OnFileClicked(IFile file) {
      StartPdfActivity(file);
    }

    /// <summary>
    /// Called when the user clicks a folder in the file manager fragment.
    /// </summary>
    /// <param name="file">File.</param>
    private void OnFolderClicked(IFolder file) {
    }
  }
}

