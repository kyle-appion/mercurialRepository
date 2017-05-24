namespace ION.CoreExtensions.Net.Portal.Remote {

	using Newtonsoft.Json;

	using ION.Core.Devices;
	using ION.Core.Sensors;

	public class RemoteGaugeDeviceSensor {
		[JsonProperty("sv")]
		public double measurement { get; set; }
		[JsonProperty("su")]
		public int unit { get; set; }
		[JsonProperty("si")]
		public int sensorIndex { get; set; }

		public RemoteGaugeDeviceSensor() {
		}

		public RemoteGaugeDeviceSensor(GaugeDeviceSensor sensor) {
			measurement = sensor.measurement.amount;
			unit = UnitLookup.GetCode(sensor.unit);
			sensorIndex = sensor.index;
		}
	}
}
