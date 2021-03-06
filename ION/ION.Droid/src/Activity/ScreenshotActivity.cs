﻿using Java.Lang;

namespace ION.Droid.Activity {

  using System;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
	using Android.Support.Design.Widget;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.IO;
  using ION.Core.Location;
  using ION.Core.Pdf;
  using ION.Core.Report;

  using ION.Droid.Dialog;
  using ION.Droid.Views;

  [Activity(Label="@string/report_screenshot", Theme="@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
  public class ScreenshotActivity : IONActivity {

    /// <summary>
    /// The extra key that is used to push/pull png bytes from the activity's starting intent.
    /// </summary>
    public const string EXTRA_PNG_BYTES = "ion.droid.extra.PNG_BYTES";

    /// <summary>
    /// The image view that will show the screenshot that will be screenshot.
    /// </summary>
    private ImageView imageView;
    private EditText nameView;
		private EditText dateView;
		private EditText versionView;
		private EditText addressView1;
		private EditText addressView2;
		private EditText cityView;
		private EditText stateView;
		private EditText countryView;
		private EditText zipView;
		private EditText notesView;

    private byte[] screenshot;
    private DateTime createdDate;

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected async override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_screenshot, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      try {
        if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) {
          SetContentView(Resource.Layout.activity_screenshot_4_4);
        } else {
          SetContentView(Resource.Layout.activity_screenshot);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to set layout. Defaulting to old version", e);
        SetContentView(Resource.Layout.activity_screenshot_4_4);
      }

      imageView = FindViewById<ImageView>(Resource.Id.content);
			nameView = FindViewById<EditText>(Resource.Id.name);
			dateView = FindViewById<EditText>(Resource.Id.date);
			versionView = FindViewById<EditText>(Resource.Id.version);
			addressView1 = FindViewById<EditText>(Resource.Id.address1);
			addressView2 = FindViewById<EditText>(Resource.Id.address2);
			cityView = FindViewById<EditText>(Resource.Id.city);
			stateView = FindViewById<EditText>(Resource.Id.state);
			countryView = FindViewById<EditText>(Resource.Id.country);
			zipView = FindViewById<EditText>(Resource.Id.zip);
			notesView = FindViewById<EditText>(Resource.Id.notes);

      createdDate = DateTime.Now;

      dateView.Text = createdDate.ToShortDateString();
      versionView.Text = ion.version;

      if (Intent.HasExtra(EXTRA_PNG_BYTES)) {
        screenshot = Intent.GetByteArrayExtra(EXTRA_PNG_BYTES);
        var b = BitmapFactory.DecodeByteArray(screenshot, 0, screenshot.Length);
        imageView.SetImageBitmap(b);
      } else {
        Error(GetString(Resource.String.report_screenshot_error_screenshot_missing));
      }

      var loc = ion.locationManager.lastKnownLocation;
      try {
				if (ion.locationManager.isEnabled) {
        	FillOutFieldsUsingAddress(await ion.locationManager.GetAddressFromLocationAsync(loc));
				}
      } catch (Exception e) {
        Error(GetString(Resource.String.error_location_failed_to_get_address), e);
      }
    }

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.ok_done, menu);
      var mi = menu.FindItem(Resource.Id.ok_done);
      var v = mi.ActionView;
      v.SetBackgroundResource(Resource.Drawable.selector_button_gold_black);
      v.SetOnClickListener(new ViewClickAction((view) => {
        OnMenuItemSelected(0, mi);
      }));

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
        case Android.Resource.Id.Home:
          Finish();
          return true;
        case Resource.Id.ok_done:
          string errorMessage = "";
          if (ValidateForm(out errorMessage)) {
            SaveScreenshotReport();
          } else {
            Alert(errorMessage);
          }
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    /// <summary>
    /// Checks all of the form entries to ensure that the user hasn't forgotten anything.
    /// </summary>
    private bool ValidateForm(out string errorMessage) {
      if (string.IsNullOrEmpty(nameView.Text)) {
        errorMessage = GetString(Resource.String.report_screenshot_error_name_missing);
        return false;
      } else {
        errorMessage = "";
        return true;
      }
    }

    /// <summary>
    /// Saves the screentshot report to the application's screenshot archive.
    /// </summary>
    private async void SaveScreenshotReport() {
      var pd = new ProgressDialog(this);
      pd.SetTitle(GetString(Resource.String.saving));
      pd.SetMessage(GetString(Resource.String.please_wait));
      pd.Indeterminate = true;
      pd.Show();

      var task = Task.Factory.StartNew(() => {
        var report = new ScreenshotReport();

        report.screenshot = screenshot;
        report.title = GetString(Resource.String.app_name) + " " + GetString(Resource.String.report_screenshot);
        report.subtitle = nameView.Text;
//        report.created = createdDate;
        report.notes = notesView.Text;

        report.tableData = new string[,] {
          { GetString(Resource.String.date), createdDate.ToShortDateString() + " " + createdDate.ToShortTimeString() },
          { GetString(Resource.String.app_version), ion.version },
					{ GetString(Resource.String.location_address_1), addressView1.Text },
					{ GetString(Resource.String.location_address_2), addressView2.Text },
          { GetString(Resource.String.location_city), cityView.Text },
					{ GetString(Resource.String.location_state_province_region), stateView.Text },
					{ GetString(Resource.String.location_country), countryView.Text },
          { GetString(Resource.String.zip), zipView.Text },
        };

        try {
          // TODO ahodder@appioninc.com: throw error if already exists
          var dir = ion.screenshotReportFolder;
          var file = dir.GetFile(report.subtitle + ".pdf", EFileAccessResponse.CreateIfMissing);
          using (var stream = file.OpenForWriting()) {
            ScreenshotReportPdfExporter.Export(report, stream);
          }

          return new Result();
        } catch (Exception e) {
          Log.E(this, "Failed to create screenshot pdf report", e);
          return new Result(GetString(Resource.String.report_screenshot_error_export_failed));
        }
      });

			var result = await task;
      pd.Dismiss();

      if (result.success) {
				Alert(Resource.String.report_screenshot_saved);
      } else {
        var err = new IONAlertDialog(this, "ERROR");
        err.SetMessage(result.errorReason);
        err.SetNegativeButton(Resource.String.cancel, (obj, args) => {
          var d = obj as Dialog;
          if (d != null) {
            d.Dismiss();
          }
        });
        err.Show();
      }

      Finish();
    }

    /// <summary>
    /// Fills out the appropriate fields for the activity using the given address.
    /// </summary>
    /// <param name="address">Address.</param>
    private void FillOutFieldsUsingAddress(Address address) {
      addressView1.Text = address.address1;
			addressView2.Text = address.address2;
      cityView.Text = address.city;
			stateView.Text = address.state;
			countryView.Text = address.country;
      zipView.Text = address.zip;
    }

    class Result {
      public bool success { get; private set; }
      public string errorReason { get; private set; }

      public Result() {
        success = true;
      }

      public Result(string errorReason) {
        success = false;
        this.errorReason = errorReason;
      }
    }
  }
}

