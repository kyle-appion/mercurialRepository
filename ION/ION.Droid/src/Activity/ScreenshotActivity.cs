namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.IO;
  using ION.Core.Location;
  using ION.Core.Pdf;
  using ION.Core.Report;
  using ION.Core.Util;

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
    private TextView dateView;
    private TextView versionView;
    private EditText addressView;
    private EditText cityView;
    private Spinner stateView;
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

      SetContentView(Resource.Layout.activity_screenshot);

      imageView = FindViewById<ImageView>(Resource.Id.content);
      nameView = FindViewById<EditText>(Resource.Id.name);
      dateView = FindViewById<TextView>(Resource.Id.date);
      versionView = FindViewById<TextView>(Resource.Id.version);
      addressView = FindViewById<EditText>(Resource.Id.address);
      cityView = FindViewById<EditText>(Resource.Id.city);
      stateView = FindViewById<Spinner>(Resource.Id.state);
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
        FillOutFieldsUsingAddress(await ion.locationManager.GetAddressFromLocationAsync(loc));
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
          { GetString(Resource.String.address), addressView.Text },
          { GetString(Resource.String.city), cityView.Text },
          { GetString(Resource.String.state), ((Java.Lang.String)stateView.SelectedItem).ToString() },
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

      task.Wait();
      var result = task.Result;
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
      addressView.Text = address.address;
      cityView.Text = address.city;
      if (!string.IsNullOrEmpty(address.state)) {
        string[] states = Resources.GetStringArray(Resource.Array.us_states);
        var index = Array.FindIndex(states, (s) => {
          return s.Equals(address.state);
        });
        if (index >= 0 && index < states.Length) {
          stateView.SetSelection(index);
        }
      }
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

