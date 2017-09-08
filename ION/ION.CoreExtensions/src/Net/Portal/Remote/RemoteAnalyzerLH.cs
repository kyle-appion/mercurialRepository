﻿namespace ION.CoreExtensions.Net.Portal {

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
		[JsonProperty("lsn")]
		public string lowSerialNumber;
		[JsonProperty("lsi")]
		public int lowSensorIndex;
		[JsonProperty("las")]
		public string lowLinkedSerialNumber;
		[JsonProperty("lai")]
		public int lowLinkedSensorIndex;
		[JsonProperty("lsub")]
		public string[] lowSubviews;

		[JsonProperty("high")]
		public string highAnalyzerIndex;
		[JsonProperty("hsn")]
		public string highSerialNumber;
		[JsonProperty("hsi")]
		public int highSensorIndex;
		[JsonProperty("has")]
		public string highLinkedSerialNumber;
		[JsonProperty("hai")]
		public int highLinkedSensorIndex;
		[JsonProperty("hsub")]
		public string[] highSubviews;


		public RemoteAnalyzerLH() {
		}

		public RemoteAnalyzerLH(Analyzer analyzer) {
			lowAnalyzerIndex = "low";
      lowSerialNumber = "null";
      lowSensorIndex = 0;
      lowLinkedSerialNumber = "null";
      lowLinkedSensorIndex = -1;
			lowSubviews = new string[0];

			highAnalyzerIndex = "high";
      highSerialNumber = "null";
      highSensorIndex = 0;
      highLinkedSerialNumber = "null";
      lowLinkedSensorIndex = -1;
			highSubviews = new string[0];

			// Commit low side manifold
			if (analyzer.lowSideManifold != null && analyzer.lowSideManifold.primarySensor is GaugeDeviceSensor) {
				var m = analyzer.lowSideManifold;
				var gds = m.primarySensor as GaugeDeviceSensor;

				if (gds != null) {
          // Commit primary sensor stuff
          var ps = (GaugeDeviceSensor)m.primarySensor;
          lowSerialNumber = ps.device.serialNumber.ToString();
          lowSensorIndex = ps.index;

					var i = analyzer.IndexOfSensor(gds);
					lowAnalyzerIndex = i + "";

          // Commit primary sensor stuff
					var sgds = m.secondarySensor as GaugeDeviceSensor;
					if (sgds != null) {
						lowLinkedSerialNumber = sgds.device.serialNumber.ToString();
            lowLinkedSensorIndex = sgds.index;
					}

          // Commit subviews
          var ussp = new List<string>();
          foreach (var sp in m.sensorProperties) {
            try {
              var code = GetCodeFromSensorProperty(sp);
              if (code != null) {
								ussp.Add(code);
							}
            } catch (Exception e) {
              Log.E(this, "Failed to get code for sensor property", e);
            }
          }
          lowSubviews = ussp.ToArray();
				}
			}

      // Commit high side manifold
      if (analyzer.highSideManifold != null && analyzer.highSideManifold.primarySensor is GaugeDeviceSensor) {
        var m = analyzer.highSideManifold;
        var gds = m.primarySensor as GaugeDeviceSensor;

        if (gds != null) {
          // Commit primary sensor stuff
          var ps = (GaugeDeviceSensor)m.primarySensor;
          highSerialNumber = ps.device.serialNumber.ToString();
          highSensorIndex = ps.index;

          var i = analyzer.IndexOfSensor(gds);
          highAnalyzerIndex = i + "";

          // Commit primary sensor stuff
          var sgds = m.secondarySensor as GaugeDeviceSensor;
          if (sgds != null) {
            highLinkedSerialNumber = sgds.device.serialNumber.ToString();
            highLinkedSensorIndex = sgds.index;
          }

          // Commit subviews
          var ussp = new List<string>();
          foreach (var sp in m.sensorProperties) {
            try {
              var code = GetCodeFromSensorProperty(sp);
              if (code != null) {
                ussp.Add(code);
              }
            } catch (Exception e) {
              Log.E(this, "Failed to get code for sensor property", e);
            }
          }
          highSubviews = ussp.ToArray();
        }
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
//        Log.D(typeof(RemoteAnalyzerLH).Name, "Cannot find code for sensor property: " + sp.GetType().Name + ". Returning null.");
        return null;
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
//				case "Rate":
//					return new RateOfChangeSensorProperty(manifold);
				case "Superheat":
					return new SuperheatSubcoolSensorProperty(manifold);

				default:
//          Log.D(typeof(RemoteAnalyzerLH).Name, "Cannot parse sensor property from code: " + code + ". Returning null.");
          return null;
			}
		}
	}
}
