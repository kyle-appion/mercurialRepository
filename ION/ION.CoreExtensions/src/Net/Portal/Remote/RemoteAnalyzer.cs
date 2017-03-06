namespace ION.CoreExtensions.Net.Portal.Remote {

	using System;

	using Newtonsoft.Json;

	public class RemoteAnalyzer {
		[JsonProperty("sn")]
		public string serialNumber;
		[JsonProperty("si")]
		public string index;
		[JsonProperty("sl")]
		public string sensorMountIndex;
		[JsonProperty("sv")]
		public string value;
		[JsonProperty("su")]
		public string unit;
		[JsonProperty("sa")]
		public string stuff;
	}
}
