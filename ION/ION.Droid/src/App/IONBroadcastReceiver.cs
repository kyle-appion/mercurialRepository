namespace ION.Droid {

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

  [BroadcastReceiver(Enabled=true)]
  [IntentFilter(new[] { Android.Content.Intent.ActionDeviceStorageLow })]
  public class IONBroadcastReceiver : BroadcastReceiver {
    public override void OnReceive(Context context, Intent intent) {
//      Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
    }
  }
}
