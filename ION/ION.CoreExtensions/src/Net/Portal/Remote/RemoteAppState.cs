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
		[JsonProperty("alyzer")]
		public RemoteSensorMount[] analyzer;
		[JsonProperty("LH")]
		public RemoteAnalyzerLH lh;
		[JsonProperty("version")]
		public int version;
		[JsonProperty("setup")]
		public Setup setup;


		public RemoteAppState() {
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
			//foreach (var manifold in workbench.manifolds) {
			//	try {
			//		var rm = RemoteManifold.CreateOrThrow(manifold);
			//		manifolds.Add(rm);
			//	} catch (Exception e) {
			//		Log.E(typeof(RemoteAppState).Name, "Failed to create remote manifold", e);
			//	}
			//}
			ret.workbench = manifolds.ToArray();

			// Commit the analyzer
			var sensorMounts = new List<RemoteSensorMount>();
			foreach (var sensor in analyzer.GetSensors()) {
				var gds = sensor as GaugeDeviceSensor;
				if (gds != null) {
					// TODO
					var index = analyzer.IndexOfSensor(sensor);
					sensorMounts.Add(new RemoteSensorMount(analyzer, sensor));
				} else {
					Log.D(typeof(RemoteAppState).Name, "Ignoring not gauge device sensor from analyzer");
				}
			}
			ret.analyzer = sensorMounts.ToArray();

			// Commit analyzer manifolds
			ret.lh = new RemoteAnalyzerLH(analyzer);

			// Commit the current state of the ion
			ret.altitude = ion.locationManager.lastKnownLocation.altitude.ConvertTo(Units.Length.METER).amount + "";

			// Commit the legacy setup stuff
			ret.setup = new Setup();
			ret.setup.positions = ion.currentAnalyzer.__legacySwaps;
			string lf = null, hf = null;
			if (ion.currentAnalyzer.lowSideSensor != null) {
				lf = ion.fluidManager.lastUsedFluid.name;
			}
			if (ion.currentAnalyzer.highSideSensor!= null) {
				hf = ion.fluidManager.lastUsedFluid.name;
			}
			ret.setup.lowFluid = lf;
			ret.setup.highFluid = hf;

			return ret;
		}
	}

	public class Setup {
		[JsonProperty("positions")]
		public int[] positions;
		[JsonProperty("lfluid")]
		public string lowFluid;
		[JsonProperty("hfluid")]
		public string highFluid;
	}
}
