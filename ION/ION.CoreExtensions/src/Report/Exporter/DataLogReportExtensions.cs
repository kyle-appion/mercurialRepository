namespace ION.Core.Report.DataLogs.Exporter {

	using System;
	using System.IO;
	using System.Threading.Tasks;

	using FlexCel.Core;
	using FlexCel.Render;
	using FlexCel.XlsAdapter;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Devices.Certificates;

  public static class DataLogReportExtensions {
		/// <summary>
		/// Calculates the minimum, maximum and average values for the given device.
		/// </summary>
		/// <returns>The device metrics.</returns>
    public static bool CalculateDeviceMetrics(this DataLogReport dlr, GaugeDeviceSensor sensor,
                                              out Scalar minimum, out Scalar maximum, out Scalar average) {
			var su = sensor.unit.standardUnit;
			var min = double.MaxValue;
      var max = double.MinValue;
      double totalMagnitude = 0;
      int items = 0;

      foreach (var sr in dlr.sessionResults) {
        var dsl = sr.GetDeviceSensorLogsFor(sensor);
        if (dsl != null) {
          foreach (var log in dsl.logs) {
            var meas = log.measurement;
            if (meas > max) {
              max = meas;
            }
            if (meas < min) {
              min = meas;
            }
            totalMagnitude += log.measurement;
            items++;
          }
        }
      }

      minimum = su.OfScalar(min);
      maximum = su.OfScalar(max);
      if (items > 0) {
        average = su.OfScalar(totalMagnitude / items);
      } else {
        average = su.OfScalar(0);
      }
      return true;
		}
  }
}
