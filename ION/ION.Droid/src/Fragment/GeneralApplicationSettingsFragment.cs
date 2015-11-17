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

  using ION.Droid.App;

  public class GeneralApplicationSettingsFragment : PreferenceFragment {
    // Overridden from PreferenceFragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      this.PreferenceManager.SharedPreferencesName = AndroidION.PREFERENCES_GENERAL;
      AddPreferencesFromResource(Resource.Xml.preferences_application);
    }
  }
}

