namespace ION.CoreExtensions.Net.Portal.Remote {

	using Newtonsoft.Json;

	using ION.Core.Devices;

//	[Preserve(AllMembers = true)]
	public class RemoteGaugeDevice {
		[JsonProperty("sn")]
		public string serialNumber { get; set; }
		[JsonProperty("sa")]
		public RemoteGaugeDeviceSensor[] sensors { get; set; }
		[JsonProperty("on")]
		public int connected { get; set; }
		[JsonProperty("bat")]
		public int battery { get; set; }


		public RemoteGaugeDevice() {
		}

		public RemoteGaugeDevice(GaugeDevice device) {
			serialNumber = device.serialNumber.ToString();
			sensors = new RemoteGaugeDeviceSensor[device.sensorCount];
			for (int i = 0; i < sensors.Length; i++) {
				sensors[i] = new RemoteGaugeDeviceSensor(device[i]);
			}
		}
	}
}
