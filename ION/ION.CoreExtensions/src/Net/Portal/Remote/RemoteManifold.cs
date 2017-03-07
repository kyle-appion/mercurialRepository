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
		public string fluid;
		[JsonProperty("fls")]
		public int fluidState;
		[JsonProperty("sub")]
		public int[] subviewCodes;

		/// <summary>
		/// The constructor used in deserialization.
		/// </summary>
		public RemoteManifold() {
		}

		/// <summary>
		/// Creates a new Manifold using this remote manifold as a template.
		/// </summary>
		/// <returns>The manifold or throw async.</returns>
		/// <param name="ion">Ion.</param>
		public async Task<Manifold> InflateManifoldOrThrowAsync(IION ion) {
			// Start loading the fluid
			var fluidTask = ion.fluidManager.LoadFluidAsync(fluid);

			var sn = serialNumber.ParseSerialNumber();
			var device = ion.deviceManager[sn] as GaugeDevice;
			if (device != null) {
				var ret = new Manifold(device[index]);

				ret.ptChart = PTChart.New(ion, (Fluid.EState)fluidState, await fluidTask);

				if (linkedSerialNumber != null) {
					var lsn = linkedSerialNumber.ParseSerialNumber();
					var linkedDevice = ion.deviceManager[lsn];
					if (linkedDevice != null) {
						var lgs = ((GaugeDevice)linkedDevice)[linkedIndex];
						ret.SetSecondarySensor(lgs);
					}
				}

				foreach (int code in subviewCodes) {
					var sp = ParseSensorPropertyFromCode(ret, code);
					if (sp != null) {
						ret.AddSensorProperty(sp);
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
		public static RemoteManifold CreateOrThrow(Manifold manifold) {
			var ret = new RemoteManifold();

			var gds = manifold.primarySensor as GaugeDeviceSensor;
			if (gds != null) {
				ret.serialNumber = gds.device.serialNumber.ToString();
				ret.index = gds.index;
				ret.fluid = manifold.ptChart.fluid.name;
				ret.fluidState = (int)manifold.ptChart.state;

				// Save the manifold's subviews.
				var codes = new List<int>();
				foreach (var sp in manifold.sensorProperties) {
					var code = CodeFromSensorProperty(sp);
					if (code != -1 && !codes.Contains(code)) {
						codes.Add(code);
					}
				}

				var sgds = manifold.secondarySensor as GaugeDeviceSensor;
				// If the sgds is null, either there isn't a secondary sensor or it is not a gauge device sensor.
				// Either way, we don't need it
				if (sgds != null) {
					ret.linkedSerialNumber = sgds.device.serialNumber.ToString();
					ret.linkedIndex = sgds.index;
				}
			} else {
				throw new Exception("Cannot create a RemoteManifold with {" + manifold.primarySensor + "}: expected a GaugeDeviceSensor");
			}

			return ret;
		}

		public static ISensorProperty ParseSensorPropertyFromCode(Manifold manifold, int code) {
			switch (code) {
				case CODE_SP_PT:
				return new PTChartSensorProperty(manifold);

				case CODE_SP_SHSC:
				return new SuperheatSubcoolSensorProperty(manifold);

				case CODE_SP_MIN:
				return new MinSensorProperty(manifold.primarySensor);

				case CODE_SP_MAX:
				return new MaxSensorProperty(manifold.primarySensor);

				case CODE_SP_HOLD:
				return new HoldSensorProperty(manifold.primarySensor);

				case CODE_SP_ROC:
				return new RateOfChangeSensorProperty(manifold.primarySensor);

				case CODE_SP_TIMER:
				return new TimerSensorProperty(manifold.primarySensor);

				case CODE_SP_SECONDARY:
				return new SecondarySensorProperty(manifold);

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
			} else if (sp is RateOfChangeSensorProperty) {
				return CODE_SP_ROC;
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
