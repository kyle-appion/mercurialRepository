namespace ION.Droid.Report {

  using System;
  using System.IO;

  using Android.Content;

  using ION.Core.Report.DataLogs;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Sensors;

  public class DataLogReportLocalization : DataLogReport.ILocalization {

    // Implemented for DataLogReport.ILocalization
    public string serialNumber { get { return GetString(Resource.String.device_serial_number); } }
		// Implemented for DataLogReport.ILocalization
		public string name { get { return GetString(Resource.String.name); } }
    // Implemented for DataLogReport.ILocalization
    public string certificationDate { get { return GetString(Resource.String.report_nist_date); } }
		// Implemented for DataLogReport.ILocalization
		public string deviceModel { get { return GetString(Resource.String.device_model); } }
		// Implemented for DataLogReport.ILocalization
    public string reportCreated { get { return GetString(Resource.String.report_created); } }
		// Implemented for DataLogReport.ILocalization
    public string reportDates { get { return GetString(Resource.String.report_dates); } }
		// Implemented for DataLogReport.ILocalization
    public string minimum { get { return GetString(Resource.String.minimum); } }
		// Implemented for DataLogReport.ILocalization
    public string maximum { get { return GetString(Resource.String.maximum); } }
		// Implemented for DataLogReport.ILocalization
    public string average { get { return GetString(Resource.String.average); } }

	  private Context context;

    public DataLogReportLocalization(Context context) {
      this.context = context;
    }

		// Implemented for DataLogReport.ILocalization
    public string GetDeviceModelString(EDeviceModel deviceModel) {
      return deviceModel.GetModelCode();
    }

		// Implemented for DataLogReport.ILocalization
		public string GetSensorTypeString(ESensorType sensorType) {
      return sensorType.GetSensorTypeName();
    }

    // Implemented for DataLogReport.ILocalization
    public Stream GetFontStream() {
			return context.Assets.Open("fonts/RobotoCondensed.ttf");
		}

		/// <summary>
		/// A wrapper to return system strings.
		/// </summary>
		/// <param name="resource">Resource.</param>
		private string GetString(int resource) {
      return context.GetString(resource);
    }
  }
}
