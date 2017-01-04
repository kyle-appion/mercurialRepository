namespace ION.Droid.Activity {

  using System;
  using System.IO;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Preferences;

  using Newtonsoft.Json;

	using Appion.Commons.Util;

  using ION.Core.App;

	using ION.Droid.Activity.Tutorial;
  using ION.Droid.Dialog;
	using ION.Droid.Preferences;

  [Activity(Label="@string/help")]      
  public class HelpPreferenceActivity : IONPreferenceActivity, Preference.IOnPreferenceClickListener {
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_help, Resource.Color.gray));

      PreferenceManager.SharedPreferencesName = AppPrefs.PREFERENCES_GENERAL;
      AddPreferencesFromResource(Resource.Xml.preferences_help);

      FindPreference(GetString(Resource.String.pkey_send_feedback)).OnPreferenceClickListener = this;
			FindPreference(GetString(Resource.String.pkey_help_walkthrough)).OnPreferenceClickListener = this;

      var preference = FindPreference(GetString(Resource.String.pkey_app_version));
      preference.Summary = ion.preferences.appVersion;
    }

    public bool OnPreferenceClick(Preference preference) {
      if (GetString(Resource.String.pkey_send_feedback).Equals(preference.Key)) {
        ion.SendAppSupportEmail(this);
        return true;
			} else if (GetString(Resource.String.pkey_help_walkthrough).Equals(preference.Key)) {
				LaunchWalkthrough();
				return true;
      } else {
        return false;
      }
    }
		/*
    /// <summary>
    /// Sends a support email to appion.
    /// </summary>
    /// <returns>The app suppory email.</returns>
    private void SendAppSupportEmail() {
      var dump = ion.CreateApplicationDump();

      var i = new Intent(Intent.ActionSend);

      try {
        var file = ion.fileManager.CreateTemporaryFile("HostDetails.json");
        var s = file.OpenForWriting();
        var w = new StreamWriter(s);
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(dump, Formatting.Indented);
        w.Write(json);
        w.Dispose();
        s.Dispose();
        i.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)));
      } catch (Exception e) {
        Log.E(this, "Failed to create the application dump", e);
      }

      i.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
      i.PutExtra(Intent.ExtraEmail, new string[] { AppionConstants.EMAIL_SUPPORT });
      i.SetType(Constants.MIME_RFC822);

      try {
        var chooser = Intent.CreateChooser(i, GetString(Resource.String.preferences_send_feedback));
        chooser.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
        StartActivity(chooser);
      } catch (Exception e) {
        Log.E(this, "Failed to start e-mail activity for support message", e);
        var adb = new IONAlertDialog(this, Resource.String.preferences_send_feedback);
        adb.SetMessage(Resource.String.preferences_send_feedback_failed);
        adb.SetNegativeButton(Resource.String.cancel, (obj, args) => {
          var dialog = obj as Dialog;
          dialog.Dismiss();
        });
        adb.SetPositiveButton(Resource.String.ok, (obj, args) => {
          var dialog = obj as Dialog;
          dialog.Dismiss();

          try {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + PackageName)));
          } catch (Exception ee) {
            Log.E(this, "Failed to start activity for maket with package name.", e);
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + PackageName)));
          }
        });

        adb.Show();
      }
		}
		*/

		/// <summary>
		/// Launches the app walkthrough activity.
		/// </summary>
		private void LaunchWalkthrough() {
			var i = new Intent(this, typeof(TutorialActivity));
			StartActivity(i);
		}
  }
}

