namespace ION.Droid.Preferences {

  using System;

  using Android.Content;
  using Android.Preferences;
  using Android.OS;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Measure;
  using ION.Core.Sensors;

  using ION.Droid.Activity;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Views;

  public class BoundedAlarmPreference : Preference {

    private IION ion;
    private Sensor sensor;

    private Switch active;
    private EditText entry;
    private Button button;

    private Unit unit;

    public BoundedAlarmPreference(Context context, IAttributeSet attrs) : base(context, attrs) {
      var a = context as SensorPreferenceActivity;

      ion = AppState.context;

      if (a != null) {
        var sp = a.Intent.GetParcelableExtra(SensorPreferenceActivity.EXTRA_SENSOR) as SensorParcelable;
        sensor = sp.Get(ion);
      }
    }

    /// <Docs>Hook allowing a Preference to generate a representation of its internal
    ///  state that can later be used to create a new instance with that same
    ///  state.</Docs>
    /// <remarks>Hook allowing a Preference to generate a representation of its internal
    ///  state that can later be used to create a new instance with that same
    ///  state. This state should only contain information that is not persistent
    ///  or can be reconstructed later.</remarks>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 1"></since>
    /// <altmember cref="M:Android.Preferences.Preference.SaveHierarchyState(Android.OS.Bundle)"></altmember>
    /// <summary>
    /// Raises the save instance state event.
    /// </summary>
    protected override IParcelable OnSaveInstanceState() {
      var state = base.OnSaveInstanceState();

      if (Persistent) {
        return state;
      }

      return new SavedState(state) {
        sensor = sensor.ToParcelable(),
        active = this.active.Checked,
        amount = entry.Text,
        unitCode = UnitLookup.GetCode(this.unit),
      };
    }

    /// <summary>
    /// Raises the restore instance state event.
    /// </summary>
    /// <param name="state">State.</param>
    protected override void OnRestoreInstanceState(IParcelable state) {
      base.OnRestoreInstanceState(state);

      var ss = state as SavedState;
      if (ss != null) {
        sensor = ss.sensor.Get(ion);
        active.Checked = ss.active;
        entry.Text = ss.amount;
        unit = UnitLookup.GetUnit(ss.unitCode);
      }
    }

    /// <Docs>The parent that this View will eventually be attached to.</Docs>
    /// <summary>
    /// Raises the create view event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    protected override View OnCreateView(ViewGroup parent) {
      var ret = LayoutInflater.From(Context).Inflate(Resource.Layout.preference_bounded_alarm, parent, false);

      active = ret.FindViewById<Switch>(Resource.Id.toggle);
      entry = ret.FindViewById<EditText>(Resource.Id.measurement);
      button = ret.FindViewById<Button>(Resource.Id.button);
      button.SetOnClickListener(new ViewClickAction((v) => {
        if (sensor != null) {
          UnitDialog.Create(Context, sensor.supportedUnits, (obj, unit) => {
            button.Text = unit.ToString();
          });
        }
      }));

      return ret;
    }

    /// <summary>
    /// A simple object that will save the state of the preference across view destructions.
    /// </summary>
    private class SavedState : BaseSavedState {
      public SensorParcelable sensor;
      public bool active;
      public string amount;
      public int unitCode;

      public SavedState(IParcelable state) : base(state){
      }

      public SavedState(Parcel source) : base(source) {
        sensor = source.ReadParcelable(Java.Lang.ClassLoader.SystemClassLoader) as SensorParcelable;
        active = source.ReadInt() == 1;
        amount = source.ReadString();
        unitCode = source.ReadInt();
      }

      public void WriteToParcel(Parcel dest, int flags) {
        dest.WriteParcelable(sensor, ParcelableWriteFlags.None);
        dest.WriteInt(active ? 1 : 0);
        dest.WriteString(amount);
        dest.WriteInt(unitCode);
      }

      private class Creator : Java.Lang.Object, IParcelableCreator {
        public Java.Lang.Object CreateFromParcel(Parcel source) {
          return new SavedState(source);
        }

        public Java.Lang.Object[] NewArray(int size) {
          return new SavedState[size];
        }
      }
    }
  }
}

