namespace ION.Droid.Preferences {
  
  using System;

  using Android.Content;
  using Android.Util;
  using Android.Preferences;
  using Android.Widget;

  using ION.Droid.Dialog;

  /// <summary>
  /// A simple dialog that will allow the user to enter a number as a preference.
  /// </summary>
  public class NumberDialogPreference : Preference {

    /// <summary>
    /// The current value of the preference.
    /// </summary>
    private double value;

    public NumberDialogPreference(Context context, IAttributeSet attrs) : base(context, attrs) {
    }

    /// <Docs>Processes a click on the preference.</Docs>
    /// <summary>
    /// Raises the click event.
    /// </summary>
    protected override void OnClick() {
      new NumberEntryDialog() {
        title = Context.GetString(Resource.String.enter_number),
        hint = Title,
        initialValue = value,
        handler = (obj, args) => {
          value = args;
          PersistFloat((float)value);
          CallChangeListener(new Java.Lang.Float(value));
        },
      }.Show(Context);
    }

    /// <summary>
    /// Raises the set initial value event.
    /// </summary>
    /// <param name="restorePersistedValue">If set to <c>true</c> restore persisted value.</param>
    /// <param name="defaultValue">Default value.</param>
    protected override void OnSetInitialValue(bool restorePersistedValue, Java.Lang.Object defaultValue) {
      var num = defaultValue as Java.Lang.Number;

      if (num != null) {
        value = num.DoubleValue();
      } else {
        value = this.GetPersistedFloat(0.0f);
      }
    }

    /// <Docs>The set of attributes.</Docs>
    /// <summary>
    /// Called when a Preference is being inflated and the default value
    ///  attribute needs to be read.
    /// </summary>
    /// <remarks>Called when a Preference is being inflated and the default value
    ///  attribute needs to be read. Since different Preference types have
    ///  different value types, the subclass should get and return the default
    ///  value which will be its value type.</remarks>
    /// <param name="a">The alpha component.</param>
    /// <param name="index">Index.</param>
    protected override Java.Lang.Object OnGetDefaultValue(Android.Content.Res.TypedArray a, int index) {
      return a.GetFloat(index, 0.0f);
    }
  }
}

