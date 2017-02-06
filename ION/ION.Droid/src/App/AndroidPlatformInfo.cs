namespace ION.Droid.App {

  using System;
	using System.Text;

	using Android.App;
	using Android.Content;
  using Android.OS;
	using Android.Views;
	using Android.Util;

  using ION.Core.App;

  public class AndroidPlatformInfo : IPlatformInfo {
    public string manufacturer { get; set; }
    public string deviceName { get; set; }
    public string model { get; set; }
    public string version { get; set; }
    public string api { get; set; }
    public string chipset { get; set; }

		public float density { get; set; }
		public float dpWidth { get; set; }
		public float dpHeight { get; set; }

    public AndroidPlatformInfo(Context context) {
      manufacturer = Build.Manufacturer;
      deviceName = Build.Product;
      model = Build.Model;
      version = Build.VERSION.Release;
      api = Build.VERSION.SdkInt + "";
      chipset = Build.Board;
		}

		public AndroidPlatformInfo(Activity activity) {
			manufacturer = Build.Manufacturer;
			deviceName = Build.Product;
			model = Build.Model;
			version = Build.VERSION.Release;
			api = Build.VERSION.SdkInt + "";
			chipset = Build.Board;

			GetScreenDetails(activity);
    }

		public override string ToString() {
			var sb = new StringBuilder();

			sb.Append("Manufacturer: ").Append(manufacturer).Append("\n");
			sb.Append("deviceName: ").Append(deviceName).Append("\n");
			sb.Append("model: ").Append(model).Append("\n");
			sb.Append("version: ").Append(version).Append("\n");
			sb.Append("Api: ").Append(api).Append("\n");
			sb.Append("chipset: ").Append(chipset).Append("\n");

			sb.Append("Density: ").Append(density).Append("\n");
			sb.Append("DPWidth: ").Append(dpWidth).Append("\n");
			sb.Append("DPHeight: ").Append(dpHeight).Append("\n");

			return sb.ToString();
		}

		private void GetScreenDetails(Activity activity) {
			var dm = new DisplayMetrics();

			var wm = activity.WindowManager;
			wm.DefaultDisplay.GetMetrics(dm);

			density = dm.Density;
			dpWidth = dm.WidthPixels / density;
			dpHeight = dm.HeightPixels / density;
		}
  }
}

