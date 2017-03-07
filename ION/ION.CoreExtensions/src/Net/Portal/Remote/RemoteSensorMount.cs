namespace ION.CoreExtensions.Net.Portal {

	using System;

	using Newtonsoft.Json;

	using ION.Core.Content;
	using ION.Core.Devices;

	public class RemoteSensorMount {
		[JsonProperty("sn")]
		public string serialNumber;
		[JsonProperty("si")]
		public string sensorIndex;
		[JsonProperty("sl")]
		public string analyzerIndex;
		[JsonProperty("sv")]
		[Obsolete("Value is not needed with a proper device manager setup")]
		public string value;
		[JsonProperty("su")]
		[Obsolete("Unit is not needed with a proper device manager setup")]
		public string unit;
		[JsonProperty("sa")]
		public string sa;

		public RemoteSensorMount() {
		}
	}
}
