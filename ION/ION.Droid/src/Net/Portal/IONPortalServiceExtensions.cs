namespace ION.Droid.Net.Portal {

	using System;
	using System.IO;

	using Android.App;
	using Android.Content;

	using Newtonsoft.Json;

	using Appion.Commons.Util;

	using ION.Core.App;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Dialog;

	public static class IONPortalServiceExtensions {

		private static string THIS = typeof(IONPortalServiceExtensions).Name;

		/// <summary>
		/// Sends a support email to appion.
		/// </summary>
		/// <returns>The app suppory email.</returns>
		public static void SendAppSupportEmail(this IONPortalService service, IION ion, Activity activity) {
			var dump = ion.CreateApplicationDump();

			var i = new Intent(Intent.ActionSend);

			try {
				var file = ion.fileManager.CreateTemporaryFile("HostDetails.json");
				var s = file.OpenForWriting();
				var w = new StreamWriter(s);
				var json = Newtonsoft.Json.JsonConvert.SerializeObject(dump, Formatting.Indented);
				w.Write(json);
				w.Dispose();
				s.Dispose();
				i.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)));
			} catch (Exception e) {
				Log.E(THIS, "Failed to create the application dump", e);
			}

			i.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
			i.PutExtra(Intent.ExtraEmail, new string[] { AppionConstants.EMAIL_SUPPORT });
			i.SetType(AppionConstants.MIME_RFC822);

			try {
				var chooser = Intent.CreateChooser(i, activity.GetString(Resource.String.preferences_send_feedback));
				chooser.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
				activity.StartActivity(chooser);
			} catch (Exception e) {
				Log.E(THIS, "Failed to start e-mail activity for support message", e);
				var adb = new IONAlertDialog(activity, Resource.String.preferences_send_feedback);
				adb.SetMessage(Resource.String.preferences_send_feedback_failed);
				adb.SetNegativeButton(Resource.String.cancel, (obj, args) => {
					var dialog = obj as Dialog;
					dialog.Dismiss();
				});
				adb.SetPositiveButton(Resource.String.ok, (obj, args) => {
					var dialog = obj as Dialog;
					dialog.Dismiss();

					try {
						activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + activity.PackageName)));
					} catch (Exception ee) {
						Log.E(THIS, "Failed to start activity for maket with package name.", ee);
						activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + activity.PackageName)));
					}
				});

				adb.Show();
			}
		}
	}
}
