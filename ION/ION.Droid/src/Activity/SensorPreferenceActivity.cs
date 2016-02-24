namespace ION.Droid.Activity {

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

  [Activity(Label = "SensorPreferenceActivity")]      
  public class SensorPreferenceActivity : PreferenceActivity {

    /// <summary>
    /// The key that is used to retrieve the sensor parcelable from the intent.
    /// </summary>
    public const string EXTRA_SENSOR = "ION.Droid.Activity.extra.SENSOR";

    /// <summary>
    /// Determines whether this instance is valid fragment the specified fragmentName.
    /// </summary>
    /// <returns><c>true</c> if this instance is valid fragment the specified fragmentName; otherwise, <c>false</c>.</returns>
    /// <param name="fragmentName">Fragment name.</param>
    protected override bool IsValidFragment(string fragmentName) {
      return true;
      //return base.IsValidFragment(fragmentName);
    }

    /// <Docs>To be added.</Docs>
    /// <remarks>To be added.</remarks>
    /// <summary>
    /// Raises the build headers event.
    /// </summary>
    /// <param name="target">Target.</param>
    public override void OnBuildHeaders(IList<Header> target) {
      LoadHeadersFromResource(Resource.Xml.preferences_sensor, target);
    }
  }
}

