namespace ION.Droid.Activity {

  using Android.App;
  using Android.Content.PM;
  using Android.Preferences;
  using Android.OS;

  using ION.Droid.Dialog;
	using ION.Droid.Preferences;

  [Activity(Label="@string/preferences", ScreenOrientation=ScreenOrientation.Portrait)]      
  public class AppPreferenceActivity : IONPreferenceActivity {


    // Overridden form IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_settings, Resource.Color.gray));
			PreferenceManager.SharedPreferencesName = AppPrefs.PREFERENCES_GENERAL;
      AddPreferencesFromResource(Resource.Xml.preferences_application);



      var allowLocationPreference = FindPreference(GetString(Resource.String.pkey_location_gps)) as SwitchPreference;
      allowLocationPreference.PreferenceClick += (sender, e) => {
        
//        if (ion.locationManager.canGetAlititude) {
//        }
      };

      var elevationPreference = FindPreference(GetString(Resource.String.pkey_location_elevation));
      elevationPreference.PreferenceClick += (sender, e) => {
        new NumberEntryDialog() {
          title = GetString(Resource.String.preferences_location_elevation),
          message = GetString(Resource.String.preferences_location_elevation_set),
          initialValue = ion.preferences.location.customElevation,
          handler = (o, d) => {
            ion.preferences.location.customElevation = d;
          },
        }.Show(this);
      };
    }
  }
}

