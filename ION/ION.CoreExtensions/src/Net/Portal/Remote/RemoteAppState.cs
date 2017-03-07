namespace ION.CoreExtensions.Net.Portal.Remote {

	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Devices;

	public class RemoteAppState {
		[JsonProperty("known")]
		public RemoteGaugeDevice[] knownDevices;
		[JsonProperty("workB")]
		public RemoteManifold[] workbench;
		[JsonProperty("alt")]
		public string altitude;
		[JsonProperty("state")]
		public RemoteState state;
		[JsonProperty("alyzer")]
		public RemoteSensorMount[] analyzer;


		private RemoteAppState() {
		}

		/// <summary>
		/// Creates a new RemoteAppState or throws an exception.
		/// </summary>
		/// <param name="ion">Ion.</param>
		public static RemoteAppState CreateOrThrow(IION ion) {
			var ret = new RemoteAppState();

			var analyzer = ion.currentAnalyzer;
			var workbench = ion.currentWorkbench;
			var dm = ion.deviceManager;

			// Commit the collection of known devices.
			var devices = new List<RemoteGaugeDevice>();
			foreach (var device in dm.knownDevices) {
				var gd = device as GaugeDevice;
				if (gd != null) {
					var rgd = new RemoteGaugeDevice(gd);
					devices.Add(rgd);
				}
			}
			ret.knownDevices = devices.ToArray();

			// Commit the workbench
			var manifolds = new List<RemoteManifold>();
			foreach (var manifold in workbench.manifolds) {
				try {
					var rm = RemoteManifold.CreateOrThrow(manifold);
					manifolds.Add(rm);
				} catch (Exception e) {
					Log.E(typeof(RemoteAppState).Name, "Failed to create remote manifold", e);
				}
			}

			// Commit the analyzer
			var sensorMounts = new List<RemoteSensorMount>();
			foreach (var sensor in analyzer.GetSensors()) {
				var gds = sensor as GaugeDeviceSensor;
				if (gds != null) {
					var index = analyzer.IndexOfSensor(sensor);
					sensorMounts.Add(new RemoteSensorMount() {
						sensorIndex = index + "",
					});
				} else {
					Log.D(typeof(RemoteAppState).Name, "Ignoring not gauge device sensor from analyzer");
				}
			}

			// Commit the current state of the ion
			ret.altitude = ion.locationManager.lastKnownLocation.altitude.ConvertTo(Units.Length.METER).amount + "";

			var state = new RemoteState();
			state.isDataLogging = ((ion.dataLogManager.isRecording) ? 1 : 0) + "";

			return ret;
		}
	}
}
