namespace ION.Droid.Activity {

  using System;
  using System.IO;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Preferences;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Fragments;
  using ION.Droid.Util;

  [Activity(Label="@string/preferences")]      
  public class AppPreferenceActivity : IONPreferenceActivity {


    // Overridden form IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_settings, Resource.Color.gray));
      this.PreferenceManager.SharedPreferencesName = AndroidION.PREFERENCES_GENERAL;
      AddPreferencesFromResource(Resource.Xml.preferences_application);
    }
  }
}

