namespace ION.Droid.Activity {

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Preferences;

  using ION.Droid.Activity.Rss;
	using ION.Droid.Activity.Tutorial;
	using ION.Droid.Net.Portal;
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
      FindPreference(GetString(Resource.String.pkey_rss_list)).OnPreferenceClickListener = this;

      var preference = FindPreference(GetString(Resource.String.pkey_app_version));
      preference.Summary = ion.preferences.lastKnownAppVersion;
    }

    public bool OnPreferenceClick(Preference preference) {
      if (GetString(Resource.String.pkey_send_feedback).Equals(preference.Key)) {
				ion.portal.SendAppSupportEmail(ion, this);
        return true;
			} else if (GetString(Resource.String.pkey_help_walkthrough).Equals(preference.Key)) {
				LaunchWalkthrough();
				return true;
      } else if (GetString(Resource.String.pkey_rss_list).Equals(preference.Key)) {
        LaunchRssList();
        return true;
      } else {
        return false;
      }
    }

		/// <summary>
		/// Launches the app walkthrough activity.
		/// </summary>
		private void LaunchWalkthrough() {
			var i = new Intent(this, typeof(TutorialActivity));
			StartActivity(i);
		}

    /// <summary>
    /// Launches the app rss feed.
    /// </summary>
    private void LaunchRssList() {
      var i = new Intent(this, typeof(RssActivity));
      StartActivity(i);
    }
  }
}

