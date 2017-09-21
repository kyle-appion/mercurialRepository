namespace ION.CoreExtensions.Net.Portal.Remote {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Newtonsoft.Json;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.Sensors.Properties;
  using ION.Core.Sensors;

  public class RemoteManifold {
		private const int CODE_SP_PT = 1;
		private const int CODE_SP_SHSC = 2;
		private const int CODE_SP_MIN = 3;
		private const int CODE_SP_MAX = 4;
		private const int CODE_SP_HOLD = 5;
		private const int CODE_SP_ROC = 6;
		private const int CODE_SP_TIMER = 7;
		private const int CODE_SP_SECONDARY = 8;


		[JsonProperty("sn")]
		public string serialNumber;
		[JsonProperty("si")]
		public int index;
		[JsonProperty("lsn")]
		public string linkedSerialNumber;
		[JsonProperty("lsi")]
		public int linkedIndex;
		[JsonProperty("fl")]
		public string fluidName;
		[JsonProperty("fls")]
		public int fluidState;
		[JsonProperty("sub")]
		public int[] subviewCodes;

		public RemoteManifold() {
		}

		/// <summary>
		/// Creates a new Manifold using this remote manifold as a template.
		/// </summary>
		/// <returns>The manifold or throw async.</returns>
		/// <param name="ion">Ion.</param>
		//public async Task<Manifold> InflateManifoldOrThrowAsync(IION ion){
		public async Task<Sensor> InflateManifoldOrThrowAsync(IION ion) {
			// Start loading the fluid
      var fluid = await ion.fluidManager.LoadFluidAsync(fluidName);

			var sn = serialNumber.ParseSerialNumber();
			var device = ion.deviceManager[sn] as GaugeDevice;
			if (device != null) {
        //var ret = new Manifold(device[index]);
        var ret = device[index];

				if (linkedSerialNumber != null) {
					var lsn = linkedSerialNumber.ParseSerialNumber();
					var linkedDevice = ion.deviceManager[lsn];
					if (linkedDevice != null) {
						var lgs = ((GaugeDevice)linkedDevice)[linkedIndex];
						ret.SetLinkedSensor(lgs);
					}
				}

				if (subviewCodes != null) {
					foreach (int code in subviewCodes) {
						var sp = ParseSensorPropertyFromCode(ret, code);
						if (sp != null) {
							ret.AddSensorProperty(sp);
						}
					}
				}

				return ret;
			} else {
				throw new Exception("Cannot create manifold with device {" + device + "}: Expected GaugeDevice");
			}
		}

		/// <summary>
		/// The primary constructor that is used to initialize this object for serialization.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		//public static RemoteManifold CreateOrThrow(Manifold manifold){
		public static RemoteManifold CreateOrThrow(Sensor sensor) {
			var ret = new RemoteManifold();

			var gds = sensor as GaugeDeviceSensor;
			if (gds != null) {
				ret.serialNumber = gds.device.serialNumber.ToString();
				ret.index = gds.index;
				ret.fluidName = AppState.context.fluidManager.lastUsedFluid.name;
        ret.fluidState = (int)sensor.fluidState;

				// Save the manifold's subviews.
				var codes = new List<int>();
				foreach (var sp in sensor.sensorProperties) {
					var code = CodeFromSensorProperty(sp);
					if (code != -1 && !codes.Contains(code)) {
						codes.Add(code);
					}
				}
				ret.subviewCodes = codes.ToArray();

				var sgds = sensor.linkedSensor as GaugeDeviceSensor;
				// If the sgds is null, either there isn't a secondary sensor or it is not a gauge device sensor.
				// Either way, we don't need it
				if (sgds != null) {
					ret.linkedSerialNumber = sgds.device.serialNumber.ToString();
					ret.linkedIndex = sgds.index;
				}
			} else {
				throw new Exception("Cannot create a RemoteManifold with {" + sensor + "}: expected a GaugeDeviceSensor");
			}

			return ret;
		}

		//public static ISensorProperty ParseSensorPropertyFromCode(Manifold manifold, int code){
		public static ISensorProperty ParseSensorPropertyFromCode(Sensor sensor, int code) {
			switch (code) {
				case CODE_SP_PT:
					//return new PTChartSensorProperty(manifold);
					return new PTChartSensorProperty(sensor);

				case CODE_SP_SHSC:
					//return new SuperheatSubcoolSensorProperty(manifold);
					return new SuperheatSubcoolSensorProperty(sensor);

				case CODE_SP_MIN:
					//return new MinSensorProperty(manifold);
					return new MinSensorProperty(sensor);

				case CODE_SP_MAX:
					//return new MaxSensorProperty(manifold);
					return new MaxSensorProperty(sensor);

				case CODE_SP_HOLD:
					//return new MaxSensorProperty(manifold);
					return new HoldSensorProperty(sensor);

//				case CODE_SP_ROC:
//				return new RateOfChangeSensorProperty(manifold);

				case CODE_SP_TIMER:
					//return new TimerSensorProperty(manifold);
					return new TimerSensorProperty(sensor);

				case CODE_SP_SECONDARY:
					//return new SecondarySensorProperty(manifold);
					return new SecondarySensorProperty(sensor);

				default:
					Log.E(typeof(RemoteManifold).Name, "Failed to find sensor property with code {" + code + "}");
				return null;
			}
		}

		public static int CodeFromSensorProperty(ISensorProperty sp) {
			if (sp is PTChartSensorProperty) {
				return CODE_SP_PT;
			} else if (sp is SuperheatSubcoolSensorProperty) {
				return CODE_SP_SHSC;
			} else if (sp is MinSensorProperty) {
				return CODE_SP_MIN;
			} else if (sp is MaxSensorProperty) {
				return CODE_SP_MAX;
			} else if (sp is HoldSensorProperty) {
				return CODE_SP_HOLD;
//			} else if (sp is RateOfChangeSensorProperty) {
//				return CODE_SP_ROC;
			} else if (sp is TimerSensorProperty) {
				return CODE_SP_TIMER;
			} else if (sp is SecondarySensorProperty) {
				return CODE_SP_SECONDARY;
			} else {
				return -1;
			}
		}
	}
}
