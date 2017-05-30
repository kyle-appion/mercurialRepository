namespace ION.Droid.Dialog {

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Android.Content;
	using Android.Support.Design.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Sensors;

	using ION.Droid.Sensors;
	using ION.Droid.Views;

	public class ManualSensorCreateDialog {

		private Context context;
		private Dictionary<ESensorType, IEnumerable<Unit>> options;

		private View content;

		private TextInputEditText name;
		private TextInputEditText measurement;

		private Button type;
		private Button unit;

		private ESensorType sensorType;
		private Unit sensorUnit;

		public ManualSensorCreateDialog(Context context, Dictionary<ESensorType, IEnumerable<Unit>> options) {
			this.context = context;
			this.options = options;

			content = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_manual_sensor_create, null, false);

			name = content.FindViewById<TextInputEditText>(Resource.Id.name);
			measurement = content.FindViewById<TextInputEditText>(Resource.Id.measurement);

			type = content.FindViewById<Button>(Resource.Id.type);
			unit = content.FindViewById<Button>(Resource.Id.unit);

			sensorType = options.Keys.First(); 
			sensorUnit = options[sensorType].First();
			type.Text = sensorType.GetTypeString();
			unit.Text = sensorUnit.ToString();

			type.Click += (sensor, obj) => {
				var dialog = new ListDialogBuilder(context);
				dialog.SetTitle(Resource.String.sensor_select_type);

				foreach (var t in options.Keys) {
					dialog.AddItem(t.GetTypeString(), () => {
						sensorType = t;
						type.Text = t.GetTypeString();

						sensorUnit = options[sensorType].First();
						unit.Text = sensorUnit.ToString();
					});
				}

				dialog.Show();
			};

			unit.Click += (sender, e) => {
				UnitDialog.Create(context, options[sensorType], (s1, e1) => {
					sensorUnit = e1;
					unit.Text = sensorUnit.ToString();
				}).Show();
			};
		}

		public Android.App.Dialog Show(Action<Sensor> onSensorCreated) {
			var adb = new IONAlertDialog(context);
			adb.SetTitle(Resource.String.sensor_create_manual_entry);
			adb.SetView(content);

			var ret = adb.Show();

			content.FindViewById(Resource.Id.cancel).Click += (sender, e) => {
				ret.Dismiss();
			};

			content.FindViewById(Resource.Id.ok_done).Click += (sender, e) => {
				double d;

				if (!double.TryParse(measurement.Text, out d)) {
					Toast.MakeText(context, Resource.String.please_enter_valid_number, ToastLength.Long).Show();
					return;
				}

				var sensor = new ManualSensor(sensorType, sensorType == ESensorType.Vacuum);
				sensor.measurement = sensorUnit.OfScalar(d);
				sensor.name = name.Text;
				onSensorCreated(sensor);

				ret.Dismiss();
			};

			return ret;
		}
	}
}
