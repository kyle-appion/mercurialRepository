namespace ION.Core.Internal.Testing {

	using System;
	using System.Collections.Generic;
	using System.IO;

	using ION.Core.Devices;
	using ION.Core.IO.Exporter;

	public class CSVAv760NistTraceExporter : IAv760NistTraceExporter {
		/// <summary>
		/// The header for the csv document.
		/// </summary>
		private const string DOCUMENT_HEADER = "AV760: NIST Tracable Vacuum Gauge Calibration Certificate";

		public CSVAv760NistTraceExporter() {
		}

		/// <summary>
		/// Export the specified stream and results.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="results">Results.</param>
		public bool Export(Stream stream, TestResults results) {
			var csv = new Csv();

			csv.AddRow(new Csv.Row().AddCol(DOCUMENT_HEADER));

      var header = new Csv.Row();
      csv.AddRow(header);
      header.AddCol("Part Number").AddCol("Serial Number").AddCol("Test Date").AddCol("Tester").AddCol("Instrument Model").AddCol("Instrument Serial Number").AddCol("Accuracy").AddCol("Calibration Date");

      foreach (var j in results.test.targetPoints) {
        header.AddCol("Calibration Point " + j.target);
        header.AddCol("Calibration Error");
      }
      header.AddCol("Total Points Failed").AddCol("Overall Grade");

			foreach (var sensor in results.results.Keys) {
				var row = new Csv.Row();
        csv.AddRow(row);
				// The columns are as follows:
				// PartNumber, SensorSerial, TestDate, Tester, InstrumentModel, InstrumentSerialNumber, Accuracy, CalibrationDate, | PointResult, PointError, ... | PointsFailed, Overall Grade
        row.AddCol(sensor.device.serialNumber.deviceModel.GetUnlocalizedPartNumber()); // Part Number
				row.AddCol(sensor.device.serialNumber.ToString());		// Sensor Serial
				row.AddCol(results.testDate);													// Test Date
				row.AddCol(results.tester);														// Tester
				row.AddCol(results.instrument.model);									// Instrument Model
				row.AddCol(results.instrument.serialNumber);					// Instrument Serial Number
				row.AddCol(results.instrument.accuracy);							// Accuracy
				row.AddCol(results.instrument.lastCalibrationDate);		// Calibration Date


				var res = results.results[sensor];
				var dres = Math.Min(results.test.count - res.Length, 0); // Account for missed columns

				var failures = 0;
				var highestError = 0.0f;

				// Target points
				for (int i = 0; i < res.Length; i++) {
					var tp = results.test.targetPoints[i];
          var err = Math.Abs(1 - (res[i] / (float)tp.target.amount));

					row.AddCol(res[i].ToString());											// Point Result		
          row.AddCol((err * 100).ToString("#.00") + "%");			// Point Error

					highestError = Math.Max(err, highestError);
					if (!results.test.GetGradeForError(err).passable) {
						failures++;
					}
				}

				if (dres > 1) {
					highestError = 1;
				}

				failures += dres;
				row.AddEmptyCols(dres);																// Fill out empty cols
				row.AddCol(failures.ToString());											// Points Failed
				row.AddCol(results.test.GetGradeForError(highestError).grade); // Overall Grade
			}

			var ret = csv.Export(stream);

			return ret;
		}
	}
}

