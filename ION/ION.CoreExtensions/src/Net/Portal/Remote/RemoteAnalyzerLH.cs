namespace ION.CoreExtensions.Net.Portal {

	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Appion.Commons.Util;

	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Sensors.Properties;

	public class RemoteAnalyzerLH {
		[JsonProperty("low")]
		public string lowAnalyzerIndex;
		[JsonProperty("las")]
		public string lowLinkedSerialNumber;
		[JsonProperty("lsi")]
		public string lowLinkedIndex;
		[JsonProperty("lsub")]
		public string[] lowSubviews;

		[JsonProperty("high")]
		public string highAnalyzerIndex;
		[JsonProperty("has")]
		public string highLinkedSerialNumber;
		[JsonProperty("hsi")]
		public string highLinkedIndex;
		[JsonProperty("hsub")]
		public string[] highSubviews;


		public RemoteAnalyzerLH() {
		}

		public RemoteAnalyzerLH(Analyzer analyzer) {
			lowAnalyzerIndex = "low";
			lowLinkedSerialNumber = "null";
			lowLinkedIndex = 0 + "";
			lowSubviews = new string[0];

			highAnalyzerIndex = "high";
			highLinkedSerialNumber = "null";
			highLinkedIndex = 0 + "";
			highSubviews = new string[0];

			// Commit low side manifold
			if (analyzer.lowSideManifold != null) {
				var m = analyzer.lowSideManifold;
				var gds = m.primarySensor as GaugeDeviceSensor;

				if (gds != null) {
					var i = analyzer.IndexOfSensor(gds);
					lowAnalyzerIndex = i + "";

					var sgds = m.secondarySensor as GaugeDeviceSensor;
					if (sgds != null) {
						lowLinkedSerialNumber = sgds.device.serialNumber.ToString();
						lowLinkedIndex = sgds.index + "";
					}
				}

				var ussp = new List<string>();
				foreach (var sp in m.sensorProperties) {
					try {
						ussp.Add(GetCodeFromSensorProperty(sp));
					} catch (Exception e) {
						Log.E(this, "Failed to get code for sensor property", e);
					}
				}
				lowSubviews = ussp.ToArray();
			}

			// Commit high side manifold
			if (analyzer.highSideManifold != null) {
				var m = analyzer.highSideManifold;
				var gds = m.primarySensor as GaugeDeviceSensor;

				if (gds != null) {
					var i = analyzer.IndexOfSensor(gds);
					highAnalyzerIndex = i + "";

					var sgds = m.secondarySensor as GaugeDeviceSensor;
					if (sgds != null) {
						highLinkedSerialNumber = sgds.device.serialNumber.ToString();
						highLinkedIndex = sgds.index + "";
					}
				}

				var ussp = new List<string>();
				foreach (var sp in m.sensorProperties) {
					try {
						ussp.Add(GetCodeFromSensorProperty(sp));
					} catch (Exception e) {
						Log.E(this, "Failed to get code for sensor property", e);
					}
				}
				highSubviews = ussp.ToArray();
			}
		}

		public static string GetCodeFromSensorProperty(ISensorProperty sp) {
			var type = sp.GetType();
			if (typeof(AlternateUnitSensorProperty).Equals(type)) {
				return "Alternate";
			} else if (typeof(PTChartSensorProperty).Equals(type)) {
				return "Pressure";
			} else if (typeof(MinSensorProperty).Equals(type)) {
				return "Minimum";
			} else if (typeof(MaxSensorProperty).Equals(type)) {
				return "Maximum";
			} else if (typeof(HoldSensorProperty).Equals(type)) {
				return "Hold";
			} else if (typeof(RateOfChangeSensorProperty).Equals(type)) {
				return "Rate";
			} else if (typeof(SuperheatSubcoolSensorProperty).Equals(type)) {
				return "Superheat";
			} else {
				throw new Exception("Cannot create code for sensor property {" + type.Name + "}");
			}
		}

		public static ISensorProperty ParseSensorPropertyFromCode(Manifold manifold, string code) {
			switch (code) {
				case "Alternate":
					return new AlternateUnitSensorProperty(manifold);
				case "Pressure":
					return new PTChartSensorProperty(manifold);
				case "Minimum":
					return new MinSensorProperty(manifold);
				case "Maximum":
					return new MaxSensorProperty(manifold);
				case "Hold":
					return new HoldSensorProperty(manifold);
				case "Rate":
					return new RateOfChangeSensorProperty(manifold);
				case "Superheat":
					return new SuperheatSubcoolSensorProperty(manifold);

				default:
					throw new Exception("Cannot create sensor property: " + code);
			}
		}
	}
}
