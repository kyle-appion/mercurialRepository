namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.IO;
  using ION.Core.Util;

  using ION.Droid.Dialog;
  using ION.Droid.Fragments;

  [Activity(Label = "ScreenshotArchiveActivity", Theme = "@style/TerminalActivityTheme")]      
  public class ScreenshotArchiveActivity : IONActivity {

    /// <summary>
    /// The fragment that handle the file management.
    /// </summary>
    private FileManagerFragment fragment;
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_screenshot_archive);

      fragment = FragmentManager.FindFragmentById<FileManagerFragment>(Resource.Id.content);
      fragment.folder = ion.screenshotReportFolder;
      fragment.onFileClicked += OnFileClicked;
      fragment.onFolderClicked += OnFolderClicked;
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

