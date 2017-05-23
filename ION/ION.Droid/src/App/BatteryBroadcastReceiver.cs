namespace ION.Droid.App {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  [BroadcastReceiver]
  public class BatteryBroadcastReceiver : BroadcastReceiver {
    public override void OnReceive(Context context, Intent intent) {
      Toast.MakeText(context, "Battery Changed!!!!!", ToastLength.Long).Show();
    }
  }
}
