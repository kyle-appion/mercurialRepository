using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ION.Core.Devices;

using ION.Droid.Devices;

namespace ION.Droid.Activities
{
	[Activity (Label = "ION.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity {

    /// <summary>
    /// The device manager that will allow the activity to perform bluetooth actions.
    /// </summary>
    private IDeviceManager __deviceManager;
    /// <summary>
    /// The lsit view that will display the devices that the device manager has found.
    /// </summary>
    private ListView __listView;
		int count = 1;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

      __deviceManager = new AndroidDeviceManager();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.scan);
			
			button.Click += delegate {
        Task<bool> didScan = __deviceManager.DoActiveScan();
			};

      __listView = FindViewById<ListView>(Android.Resource.Id.List);
		}
	}

  /// <summary>
  /// The adapter that will store the devices within the adapter.
  /// </summary>
  internal class DeviceAdapter : BaseAdapter<IDevice> {
    private List<IDevice> __devices = new List<IDevice>();

    public DeviceAdapter(List<IDevice> devices) {
      __devices = devices;
    }

    public override int Count { get { return __devices.Count; } }

    public override IDevice this[int position] { get { return __devices[position];  } }

    public override long GetItemId(int position) {
      return position;
    }

    public override View GetView(int position, View convert, ViewGroup mommy) {
      Context c = mommy.Context;

      ViewHolder vh;

      if (convert == null) {
        convert = LayoutInflater.From(c).Inflate(Resource.Layout.list_item_device_test, mommy, false);
        vh = new ViewHolder();
        convert.Tag = vh;

        vh.title = convert.FindViewById<TextView>(Resource.Id.title);
        vh.content = convert.FindViewById<TextView>(Resource.Id.content);
      } else {
        vh = (ViewHolder)convert.Tag;
      }

      IDevice device = this[position];

      vh.title.Text = device.name;
      vh.content.Text = "[" + device.connection.connectionState + "]";
    }

    class ViewHolder : Java.Lang.Object {
      public TextView title, content;
    }
  }
}


