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
                                              out ScalarSpan minimum, out ScalarSpan maximum, out ScalarSpan average) {
			var su = sensor.unit.standardUnit;
			var min = su.OfSpan(double.MaxValue);
      var max = su.OfSpan(double.MinValue);
      double totalMagnitude = 0;
      int items = 0;

      foreach (var sr in dlr.sessionResults) {
        var dsl = sr.GetDeviceSensorLogsFor(sensor);
        if (dsl != null) {
          foreach (var log in dsl.logs) {
            var meas = su.OfSpan(log.measurement);
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

      minimum = min;
      maximum = max;
      if (items > 0) {
        average = su.OfSpan(totalMagnitude / items);
      } else {
        average = su.OfSpan(0);
      }
      return true;
		}
  }
}
