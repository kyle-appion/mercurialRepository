namespace ION.Droid.Fragment {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Preferences;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  public class GeneralApplicationSettingsFragment : PreferenceFragment {
    /// <summary>
    /// The name of the general ion preferences.
    /// </summary>
    public const string PREFERENCES_NAME = "ion.preferences";

    // Overridden from PreferenceFragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      this.PreferenceManager.SharedPreferencesName = PREFERENCES_NAME;
      AddPreferencesFromResource(Resource.Xml.preferences_application);
    }
  }
}

