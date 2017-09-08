namespace ION.Droid.Dialog {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Devices;
  using ION.Core.Sensors;

  public class RenameDialog {

    private IRenamable renamable;

    public RenameDialog(IDevice device) {
      renamable = new DeviceRenamable(device);
    }

    public RenameDialog(Sensor sensor) {
      renamable = new SensorRenamable(sensor);
    }

    public RenameDialog(IRenamable renamable) {
      this.renamable = renamable;
    }

    public void Show(Context context) {
      var adb = new IONAlertDialog(context);

      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_rename, null, false);
      var entry = view.FindViewById<EditText>(Resource.Id.name);
      entry.Hint = renamable.defaultName;
      entry.Text = renamable.name;

      adb.SetTitle(Resource.String.rename);
      adb.SetMessage(renamable.GetRenameMessage(context));
      adb.SetView(view);

      adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
      });

      adb.SetPositiveButton(Resource.String.ok_done, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();

        if (string.IsNullOrEmpty(entry.Text.Trim())) {
          renamable.name = renamable.defaultName;
        } else {
          renamable.name = entry.Text;
        }
      });

      adb.Show();
    }

    /// <summary>
    /// A simple interface that is notified of when something is renamed.
    /// </summary>
    public interface IRenamable {
      /// <summary>
      /// The name of the renamable.
      /// </summary>
      /// <value>The name.</value>
      string name { get; set; }

      string defaultName { get; }

      /// <summary>
      /// Queries the rename message for the renamable.
      /// </summary>
      /// <returns>The rename message.</returns>
      /// <param name="context">Context.</param>
      string GetRenameMessage(Context context);
    }

    class DeviceRenamable : IRenamable {
      public string name { 
        get {
          return device.name;
        }
        set {
          device.name = value;
        }
      }

      public string defaultName { get { return device.serialNumber.ToString(); } }

      private IDevice device;

      public DeviceRenamable(IDevice device) {
        this.device = device;
      }

      public string GetRenameMessage(Context context) {
        return context.GetString(Resource.String.rename) + " " + device.name;
      }
    }

    class SensorRenamable : IRenamable {
      public string name {
        get {
          return sensor.name;
        }
        set {
          sensor.name = value;
        }
      }

			public string defaultName { get { return sensor.name; } }

			private Sensor sensor;

      public SensorRenamable(Sensor sensor) {
        this.sensor = sensor;
      }

      public string GetRenameMessage(Context context) {
        return context.GetString(Resource.String.rename) + " " + sensor.name;
      }
    }
  }
}

