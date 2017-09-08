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

  [BroadcastReceiver]
  public class RssBroadcastReceiver : BroadcastReceiver {
    public override void OnReceive(Context context, Intent intent) {
      switch (intent.Action) {
        case Intent.ActionBootCompleted: {
          ResolveBootCompleted();
          break;
        } // Intent.ActionBootCompleted
      }
      Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
    }

    /// <summary>
    /// Called after the device successfully boots up.
    /// </summary>
    private void ResolveBootCompleted() {
      
    }
  }
}
