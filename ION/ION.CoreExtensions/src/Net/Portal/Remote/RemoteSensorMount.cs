namespace ION.CoreExtensions.Net.Portal {

	using System;

	using Newtonsoft.Json;

	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Sensors;

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

		public RemoteSensorMount() {
		}

		public RemoteSensorMount(Analyzer analyzer, Sensor sensor) {
			var gds = sensor as GaugeDeviceSensor;
			if (gds == null) {
				throw new Exception("Cannot create sensor mount for {" + sensor + "}: sensor must not be null");
			} else if (!analyzer.HasSensor(sensor)) {
				throw new Exception("Cannot create sensor mount for {" + sensor +"}: sensor must be in analyzer");
			}

			serialNumber = gds.device.serialNumber.ToString();
			sensorIndex = gds.index + "";
			analyzerIndex = analyzer.IndexOfSensor(sensor) + "";
		}
	}
}
