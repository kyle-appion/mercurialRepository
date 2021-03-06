﻿namespace TestBench.Droid.Activity {

	using System;
	using System.IO;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Support.Design.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	[Activity(Label="Export Test")]
	public class ExportTestActivity : BaseActivity {

		private const string SAVE_PATH = "AppionTestRig";

		private TextInputEditText instrumentModel;
		private TextInputEditText instrumentSerialNumber;
		private TextInputEditText instrumentAccuracy;
		private TextInputEditText certifiedBy;
		private TextInputEditText sendTo;

		private DatePicker datePicker;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

			SetContentView(Resource.Layout.activity_export_test);

			instrumentModel = FindViewById<TextInputEditText>(Resource.Id.instrumentModel);
			instrumentSerialNumber = FindViewById<TextInputEditText>(Resource.Id.instrumentSerialNumber);
			instrumentAccuracy = FindViewById<TextInputEditText>(Resource.Id.instrumentAccuracy);
			certifiedBy = FindViewById<TextInputEditText>(Resource.Id.certifiedBy);
			sendTo = FindViewById<TextInputEditText>(Resource.Id.sendTo);

			datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
		}

		protected override void OnResume() {
			base.OnResume();

			LoadSettings();
		}

		protected override void OnPause() {
			base.OnPause();

			SaveSettings();
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			MenuInflater.Inflate(Resource.Menu.export, menu);

			return true;
		}

		// Overridden from Activity
		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Resource.Id.export:
					ExportTest();
					return true;
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		private void LoadSettings() {
			instrumentModel.Text = prefs.intrumentModel;
			instrumentSerialNumber.Text = prefs.intrumentSerialNumber;
			instrumentAccuracy.Text = prefs.intrumentAccuracy.ToString();
			certifiedBy.Text = prefs.certifiedBy;
			sendTo.Text = prefs.sendTo;

			datePicker.DateTime = prefs.instrumentCalibrationDate;
		}

		private void SaveSettings() {
			prefs.intrumentModel = instrumentModel.Text;
			prefs.intrumentSerialNumber = instrumentSerialNumber.Text;
			prefs.intrumentAccuracy = instrumentAccuracy.Text;
			prefs.certifiedBy = certifiedBy.Text;
			prefs.sendTo = sendTo.Text;
			prefs.instrumentCalibrationDate = datePicker.DateTime;
		}

		/// <summary>
		/// Determines whether or not the fields are valid.
		/// </summary>
		/// <returns><c>true</c>, if fields was validated, <c>false</c> otherwise.</returns>
		private bool ValidateFields() {
			var tr = service.currentTest.testResults;

			tr.instrumentModel = instrumentModel.Text;
			tr.instrumentSerialNumber = instrumentSerialNumber.Text;
			var acc = 0.0;
			if (!double.TryParse(instrumentAccuracy.Text, out acc) || acc >= 1 || acc <= 0) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Failed to validate fields");
				adb.SetMessage("Please enter a valid accuracy as a number between 0 and 1, where 1 is 100% and 0 is 0%.");
				adb.SetNegativeButton("Ok, I'll fix my ways", (sender, e) => {
				});
				adb.Show();
				return false;
			} else {
				tr.instrumentAccuracy = acc;
			}
			tr.certifiedBy = certifiedBy.Text;

			tr.instrumentCalibrationDate = datePicker.DateTime;

			return true;
		}

		private bool IsExternalStorageWritable() {
			return Android.OS.Environment.MediaMounted.Equals(Android.OS.Environment.ExternalStorageState);
		}

		private void ExportTest() {
			if (!ValidateFields()) {
				return;
			}

			if (!IsExternalStorageWritable()) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Export Failed");
				adb.SetMessage("We cannot detect a writable external storage medium for the test. Please ensure the tablet is not plugged into a computer or that the storage is not full");
				adb.SetNegativeButton("Ok.", (sender, e) => {
				});
				adb.Show();
				return;
			}

			Stream stream = null;
			try {
				var now = DateTime.Now;
				var fn = service.currentTest.testName + " (" + now.ToShortDateString() + " " + now.ToShortTimeString() + ").csv";
				fn = fn.Replace("/", "-");
				var root = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
				var dirPath = System.IO.Path.Combine(root.AbsolutePath, SAVE_PATH);
				if (!Directory.Exists(dirPath)) {
					Directory.CreateDirectory(dirPath);
				}
				var file = new FileInfo(System.IO.Path.Combine(dirPath, fn));
				stream = file.Open(FileMode.OpenOrCreate);

				if (service.currentTest.Export(stream)) {
				} else {
					var adb = new AlertDialog.Builder(this);
					adb.SetTitle("Export Failed");
					adb.SetMessage("Test failed to export");
					adb.SetNegativeButton("Ok.", (sender, e) => {
					});
					adb.Show();
					return;
				}

				var i = new Intent(Intent.ActionSend);
				i.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(new Java.IO.File(file.FullName)));
				i.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
				i.PutExtra(Intent.ExtraEmail, new string[] { sendTo.Text });
				i.PutExtra(Intent.ExtraSubject, "Appion Test Rig Results" + DateTime.Now.ToUTCMilliseconds());
				i.PutExtra(Intent.ExtraText, "The attached CSV is a new ION intial/recalibration test. Please document the attached devices.\n\nRegards,\n" + certifiedBy.Text);
				i.SetType("message/rfc822");

				var chooser = Intent.CreateChooser(i, "Select Send Method");
				chooser.SetFlags(ActivityFlags.NewTask | ActivityFlags.NoHistory);
				StartActivity(chooser);
			} catch (Exception e) {
				var adb = new AlertDialog.Builder(this);
				adb.SetCancelable(false);
				adb.SetTitle("Export Failed");
				adb.SetMessage("Test failed to export:\n" + e.Message + "\n" + e.StackTrace);
				adb.SetNegativeButton("Ok.", (sender, ee) => {
				});
				adb.Show();
			} finally {
				if (stream != null) {
					stream.Close();
				}
			}
		}
	}
}
