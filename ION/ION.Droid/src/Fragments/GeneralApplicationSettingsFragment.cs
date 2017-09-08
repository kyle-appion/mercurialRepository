namespace ION.Droid.Fragments {

  using Android.OS;
  using Android.Preferences;

  using ION.Droid.Preferences;

  public class GeneralApplicationSettingsFragment : PreferenceFragment {
    // Overridden from PreferenceFragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      PreferenceManager.SharedPreferencesName = AppPrefs.PREFERENCES_GENERAL;
      AddPreferencesFromResource(Resource.Xml.preferences_application);
    }
  }
}

