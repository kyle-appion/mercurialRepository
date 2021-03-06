﻿namespace ION.Droid.Activity {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;

	using Appion.Commons.Util;

  using ION.Core.IO;

  using ION.Droid.Dialog;
  using ION.Droid.Fragments;

  [Activity(Label = "@string/report_screenshot_archive", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
  public class ScreenshotArchiveActivity : IONActivity {

    /// <summary>
    /// The fragment that handle the file management.
    /// </summary>
    private FileBrowserFragment fragment;
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_screenshot_archive);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_screenshot, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      fragment = FragmentManager.FindFragmentById<FileBrowserFragment>(Resource.Id.content);
      fragment.folder = ion.screenshotReportFolder;
      fragment.onFileClicked = OnFileClicked;
      fragment.onFolderClicked = OnFolderClicked;
			fragment.emptyString = GetString(Resource.String.report_screenshot_empty);
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Starts an activity that will allow the user to view the given pdf.
    /// </summary>
    private void StartPdfActivity(IFile file) {
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
				adb.Show();
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

