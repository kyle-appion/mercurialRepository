namespace ION.Droid.Preferences {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Preferences;
  using Android.Runtime;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  /// <summary>
  /// Lets the list preference use integers.
  /// </summary>
  public class IntListPreference : ListPreference {
    public IntListPreference(Context context) : base(context) {
    }

    public IntListPreference(Context context, IAttributeSet attrs) : base(context, attrs) {
    }

    public IntListPreference(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) {
    }

    // Overridden from ListPreference
    protected override bool PersistString(string value) {
      if (value == null) {
        return false;
      } else {
        return PersistInt(int.Parse(value));
      }
    }

    // Overridden from ListPreference
    protected override string GetPersistedString(string defaultReturnValue) {
      if (SharedPreferences.Contains(Key)) {
        var value = GetPersistedInt(0);
        return value + "";
      } else {
        return defaultReturnValue;
      }
    }
  }
}

