namespace ION.Core.Internal.Testing {

	using System;
	using System.Collections.Generic;

	using ION.Core.Devices;
	public class TestResults {
		/// <summary>
		/// The test that the results are with regard to.
		/// </summary>
		/// <value>The test.</value>
		public TestProcedure test { get; set; }
		/// <summary>
		/// The actual results of the test.
		/// </summary>
		/// <value>The results.</value>
		public Dictionary<GaugeDeviceSensor, int[]> results { get; set; }
		/// <summary>
		/// The instrument that was used to test the calibration of the devices.
		/// </summary>
		/// <value>The instrument.</value>
		public CalibrationInstrument instrument { get; set; }
		/// <summary>
		/// The employee who completed the testing.
		/// </summary>
		/// <value>The tester.</value>
		public string tester { get; set; }

		/// <summary>
		/// The date that the results were completed.
		/// </summary>
		/// <value>The results.</value>
		public DateTime testDate { get; set; }

		public TestResults(TestProcedure test, Dictionary<GaugeDeviceSensor, int[]> results, CalibrationInstrument instrument, string tester) {
			this.test = test;
			this.results = results;
			this.instrument = instrument;
			this.tester = tester;

			this.testDate = DateTime.Now;
		}
	}

	/// <summary>
	/// The instrument that is used to calibrate the gauges.
	/// </summary>
	public class CalibrationInstrument {
		public string model { get; set; }
		public string serialNumber { get; set; }
		public string accuracy { get; set; }
		public DateTime lastCalibrationDate { get; set; }
	}
}

