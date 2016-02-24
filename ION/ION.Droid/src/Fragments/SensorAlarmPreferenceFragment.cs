namespace ION.Droid.Fragments {
  
  using System;

  using Android.Preferences;

  using ION.Core.App;

  /// <summary>
  /// A fragment that will present preferences for a sensor.
  /// </summary>
  public class SensorAlarmPreferenceFragment : PreferenceFragment {
    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnCreate(Android.OS.Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
    }
  }
}

