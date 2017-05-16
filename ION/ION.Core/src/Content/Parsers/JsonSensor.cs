namespace ION.Core.Content.Parsers {

	using System;

	using Newtonsoft.Json;

	using ION.Core.Sensors;

	public class JsonSensor {
		[JsonProperty("macroType")]
		public EType macroType;

		public JsonSensor() {
		}

		public enum EType {
			Null,
			Manual,
			GaugeDevice,
		}
	}
}
