namespace ION.CoreExtensions.Net.Portal.Remote {

	using System;

	using Newtonsoft.Json;

	public class RemoteManifold {
		[JsonProperty("sn")]
		public string serialNumber;
		[JsonProperty("si")]
		public int index;
		[JsonProperty("lsn")]
		public string linkedSerialNumber;
		[JsonProperty("li")]
		public int linkedIndex;
		[JsonProperty("fl")]
		public string fluid;
		[JsonProperty("sub")]
		public int[] subviewCodes;
	}
}
