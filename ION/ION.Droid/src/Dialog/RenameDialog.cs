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

        renamable.Rename(entry.Text);
      });

      adb.Show();
    }

    /// <summary>
    /// A simple interface that is notified of when something is renamed.
    /// </summary>
    public interface IRenamable {
      /// <summary>
      /// Queries the rename message for the renamable.
      /// </summary>
      /// <returns>The rename message.</returns>
      /// <param name="context">Context.</param>
      string GetRenameMessage(Context context);
      /// <summary>
      /// Called when the rename is commited.
      /// </summary>
      /// <param name="newName">New name.</param>
      void Rename(string newName);
    }

    class DeviceRenamable : IRenamable {
      private IDevice device;

      public DeviceRenamable(IDevice device) {
        this.device = device;
      }

      public string GetRenameMessage(Context context) {
        return context.GetString(Resource.String.rename) + " " + device.name;
      }

      public void Rename(string newName) {
        device.name = newName;
      }
    }

    class SensorRenamable : IRenamable {
      private Sensor sensor;

      public SensorRenamable(Sensor sensor) {
        this.sensor = sensor;
      }

      public string GetRenameMessage(Context context) {
        return context.GetString(Resource.String.rename) + " " + sensor.name;
      }

      public void Rename(string newName) {
        sensor.name = newName;
      }
    }
  }
}

