namespace TestBench.Droid {

	using System;

	using Android.App;
	using Android.Content;

	public class Prefs {
		private const string SETTINGS = "settings";

		private const string INSTRUMENT_MODEL = "instrumentModel";
		private const string INSTRUMENT_SERIAL_NUMBER = "instrumentSerialNumber";
		private const string INSTRUMENT_ACCURACY = "instrumentAccuracy";
		private const string CERTIFIED_BY = "certifiedBy";
		private const string SEND_TO = "sendTo";
		private const string INSTRUMENT_CALIBRATION_DATE = "instrumentCalibrationDate";

		private static Prefs PREFS;
		public static Prefs Get(Context context) { 
			if (PREFS == null) {
				PREFS = new Prefs(context);
			}
			return PREFS;
		}

		public string intrumentModel {
			get {
				return sp.GetString(INSTRUMENT_MODEL, "Not Set");
			}
			set {
				var e = sp.Edit();

				e.PutString(INSTRUMENT_MODEL, value);

				e.Commit();
			}
		}

		public string intrumentSerialNumber {
			get {
				return sp.GetString(INSTRUMENT_SERIAL_NUMBER, "Not Set");
			}
			set {
				var e = sp.Edit();

				e.PutString(INSTRUMENT_SERIAL_NUMBER, value);

				e.Commit();
			}
		}

		public string intrumentAccuracy {
			get {
				return sp.GetString(INSTRUMENT_ACCURACY, "0.0");
			}
			set {
				var e = sp.Edit();

				e.PutString(INSTRUMENT_ACCURACY, value);

				e.Commit();
			}
		}

		public DateTime instrumentCalibrationDate {
			get {
				return DateTime.Parse(sp.GetString(INSTRUMENT_CALIBRATION_DATE, "01/01/2014"));
			}
			set {
				var e = sp.Edit();

				e.PutString(INSTRUMENT_CALIBRATION_DATE, value.ToShortDateString());

				e.Commit();
			}
		}

		public string certifiedBy {
			get {
				return sp.GetString(CERTIFIED_BY, "Not Set");
			}
			set {
				var e = sp.Edit();

				e.PutString(CERTIFIED_BY, value);

				e.Commit();
			}
		}

		public string sendTo {
			get {
				return sp.GetString(SEND_TO, "Not Set");
			}
			set {
				var e = sp.Edit();

				e.PutString(SEND_TO, value);

				e.Commit();
			}
		}

		private Context context;
		private ISharedPreferences sp;

		public Prefs(Context context) {
			sp = context.GetSharedPreferences(SETTINGS, FileCreationMode.Private);
		}
	}
}
