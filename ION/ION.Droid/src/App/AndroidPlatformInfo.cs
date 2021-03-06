namespace ION.Droid.App {

  using System;
	using System.Text;

	using Android.App;
  using Android.Bluetooth;
	using Android.Content;
  using Android.Net;
  using Android.OS;
	using Android.Views;
	using Android.Util;

  using ION.Core.App;

  public class AndroidPlatformInfo : IPlatformInfo {
    public string manufacturer { get; set; }
    public string deviceName { get; set; }
    public string hardwareName { get; set; }
    public string model { get; set; }
    public string version { get; set; }
    public string api { get; set; }
    public string chipset { get; set; }
    public bool loggingStatus { get; set; }

    public bool wifiConnected { get; set; }
    public int batteryPercentage { get; set; }
    public long freeMemory { get; set; }

		public float density { get; set; }
		public float dpWidth { get; set; }
		public float dpHeight { get; set; }

    private Context context;

    public AndroidPlatformInfo(Context context) {
      this.context = context;
      var bm = context.GetSystemService(Context.BluetoothService) as BluetoothManager;
      manufacturer = Build.Manufacturer;
      deviceName = bm.Adapter.Name;
      hardwareName = Build.Product;
      model = Build.Model;
      version = Build.VERSION.Release;
      api = Build.VERSION.SdkInt + "";
      chipset = Build.Board;

      var cm = context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
      if (cm != null && cm.ActiveNetworkInfo != null) {
        wifiConnected = cm.ActiveNetworkInfo.Type == ConnectivityType.Wifi;
      } else {
        wifiConnected = false;
      }

			GetScreenDetails();
		}

    public string GetDeviceName() {
      var bm = context.GetSystemService(Context.BluetoothService) as BluetoothManager;
      return bm.Adapter.Name;
    }

		public override string ToString() {
			var sb = new StringBuilder();

			sb.Append("Manufacturer: ").Append(manufacturer).Append("\n");
			sb.Append("deviceName: ").Append(hardwareName).Append("\n");
			sb.Append("model: ").Append(model).Append("\n");
			sb.Append("version: ").Append(version).Append("\n");
			sb.Append("Api: ").Append(api).Append("\n");
			sb.Append("chipset: ").Append(chipset).Append("\n");

			sb.Append("Density: ").Append(density).Append("\n");
			sb.Append("DPWidth: ").Append(dpWidth).Append("\n");
			sb.Append("DPHeight: ").Append(dpHeight).Append("\n");

			return sb.ToString();
		}

		private void GetScreenDetails() {
			var wm = context.GetSystemService(Context.WindowService) as IWindowManager;
			if (wm == null) {
				return;
			}

			var dm = new DisplayMetrics();

			wm.DefaultDisplay.GetMetrics(dm);

			density = dm.Density;
			dpWidth = dm.WidthPixels / density;
			dpHeight = dm.HeightPixels / density;
		}
/*
    private int GetBatteryLevelApi21(Context context) {
      var bm = context.GetSystemService(Context.BatteryService) as BatteryManager;
      return bm.GetIntProperty(BatteryManager.BatteryPropertyCapacity);
    }

    private int GetBatteryLevelViaBroadcast(Context context) {
//      var br = new BroadcastReceiver(
    }

    private class BatteryBroadcastReceiver : BroadcastReceiver {
      public int battery;

      public override void OnReceive(Context context, Intent intent) {
        var level = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
        if (level != -1) {
          battery = level;
          found = true;
        }
      }
    }
*/
  }
}

