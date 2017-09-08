namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;

	using Appion.Commons.Measure;

	using ION.Core.Devices;

	public class TestResults {
		public string instrumentModel { get; set; }
		public string instrumentSerialNumber { get; set; }
		public DateTime instrumentCalibrationDate { get; set; }
		public double instrumentAccuracy { get; set; }
		public string certifiedBy { get; set; }
		public Scalar temperature;
		public Scalar humidity;
		public double errorBand;

		public TimeSpan testDuration;

		public Dictionary<ISerialNumber, Dictionary<TestParameters.TargetPoint, Result>> resultsTable { get; set; }

		public TestResults() {
			temperature = Units.Temperature.CELSIUS.OfScalar(0);
			humidity = Units.Humidity.RELATIVE_HUMIDITY.OfScalar(0);
			resultsTable = new Dictionary<ISerialNumber, Dictionary<TestParameters.TargetPoint, Result>>();
		}

		public class Result {
			/// <summary>
			/// The value of the result.
			/// </summary>
			public Scalar result { get; private set; }
			/// <summary>
			/// The error of the result with regard to the target point. This should almost always be zero as we should almost
			/// always hit the target point.
			/// </summary>
			public double error { get; private set; }

			public Result(Scalar measurement, Scalar targetPoint) {
				result = measurement;
				error = measurement.ConvertTo(targetPoint.unit).amount / targetPoint.amount;
			}

			public bool IsBetterThan(Result other) {
				return Math.Abs(error) < Math.Abs(other.error);
			}
		}
	}
}

