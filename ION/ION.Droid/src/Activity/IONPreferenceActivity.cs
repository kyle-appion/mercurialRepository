namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Preferences;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Fragment;

  [Activity(Label = "IONPreferenceActivity")]      
  public class IONPreferenceActivity : IONActivity {

    private PreferenceFragment preferences { get; set; }

    // Overridden form IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_settings, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);
      SetContentView(Resource.Layout.activity_ion_preference);
    }

    // Overridden from IONActivity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }
  }
}

