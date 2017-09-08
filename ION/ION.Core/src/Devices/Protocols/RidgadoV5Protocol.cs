namespace ION.Core.Devices.Protocols {

	using System;
	using System.Collections.Generic;
	using System.IO;

  using Appion.Commons.Collections;
	using Appion.Commons.Measure;

	using ION.Core.Fluids;
	using ION.Core.IO;
  using ION.Core.Math;
	using ION.Core.Sensors;

	/// <summary>
	/// The protocol that is used to communicate with the Rigado bluetooth chipset based devices. This protocol allows
	/// for variable sized gauge packets.
	/// </summary>
	public class RidgadoV5Protocol : BaseBinaryProtocol {
		/// <summary>
		/// The mask that is used to retrieve a unit code from a definition byte.
		/// </summary>
		private const int MASK_UNIT = 0x3f;
		/// <summary>
		/// The mask that is used to retrieve whether or not a sensor is connected from a definition byte. 0 is diconnected,
		/// 1 is connected.
		/// </summary>
		private const int MASK_CONNECTED = 0x40;
		/// <summary>
		/// The mask that is used to retrieve the measurement mode from of a sensor from a definition byte.
		/// </summary>
		private const int MASK_MODE = 0x80;

		/// <summary>
		/// Queries the version of the protocol.
		/// </summary>
		/// <value>The version.</value>
		public override EProtocolVersion version { get { return EProtocolVersion.V4; } }
		/// <summary>
		/// The value that indicates that a sensor is not attached to a device.
		/// </summary>
		/// <value>The removed gauge value.</value>
		public override int removedGaugeValue { get { return 0; } }
		/// <summary>
		/// Queries whether or not the protocol supports broadcasting.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public override bool supportsBroadcasting { get { return true; } }

		/// <summary>
		/// Parses the provided packet. If the packet cannot be parsed, this method will
		/// throw an argument exception.
		/// </summary>
		/// <param name="packet">The packet to parse.</param>
		/// <returns></returns>
		public override GaugePacket ParsePacket(byte[] packet) {
			using (BinaryReader r = new BinaryReader(new MemoryStream(packet))) {
				var version = (EProtocolVersion)r.ReadByte();
				var battery = (int)r.ReadByte();

				var readings = new List<GaugeReading>();

				// According to the rigado protocol specification, the packet length should be no more than 19 bytes. 
				var count = 17; // 19 - version and battery
				SensorPayload sp;
				while (count >= (sp = new SensorPayload(r.ReadByte())).length) { // While the count still has another potential packet, pull the next packet.
					count -= sp.length;
					if (sp.unitCode == 0) {
						continue;
					}
					sp.Parse(r);
					readings.Add(new GaugeReading() {
						removed = !sp.connected,
						sensorType = UnitLookup.GetSensorTypeFromCode(sp.unitCode),
						reading = sp.unit.OfScalar(sp.measurement),
					});
				}

				return new GaugePacket(version, battery, readings.ToArray());
			}
		}

		/// <summary>
		/// Creates a packet that, when received by a remote terminus, will set the unit
		/// for a given sensor.
		/// </summary>
		/// <param name="sensorIndex">The 1-based index of the sensor to set the unit for.</param>
		/// <param name="sensorType">The sensor type. This is necessary to deduce the proper
		/// unit code for the packet.</param>
		/// <param name="unit">The unit that the terminus will be set to.</param>
		/// <returns></returns>
		// Overridden from IGaugeProtocol
		public override byte[] CreateSetUnitCommand(int sensorIndex, Sensors.ESensorType sensorType, Unit unit) {
			var ret = new byte[] { 0x02, (byte)UnitLookup.GetCode(unit), (byte)sensorIndex };
			return ret;
		}

		/// <summary>
		/// Creates a packet that, when received by a remote terminus, will zero the 
		/// terminus' reading. This will have no effect on termini that do not support
		/// this command.
		/// </summary>
		/// <param name="sensorIndex">The 1-based index of the sensor to zero</param>
		/// <returns></returns>
		public override byte[] CreateZeroSensorCommand(int sensorIndex) {
			return new byte[] { 0x03, (byte)sensorIndex };
		}

		/// <summary>
		/// Creates a packet that, when received by a remote terminus, will set the
		/// altitude that is retained within the terminus' memory.
		/// </summary>
		/// <param name="altitude">A scalar whose base unit is METER.</param>
		/// <returns></returns>
		public override byte[] CreateSetAltitudeCommand(Scalar altitude) {
			var v = (int)altitude.amount;
			var msb = (byte)((v >> 8) & 0xff);
			var lsb = (byte)(v & 0xff);
			return new byte[] { 0x04, msb, lsb, (byte)UnitLookup.GetCode(altitude.unit) };
		}

		/// <summary>
		/// Serializes the given fluid into an enumerable set of 20 bytes chunks.
    /// Note: The elevation assumed for this procedure is sea level or 0m.
		/// </summary>
		/// <returns>The fluid.</returns>
		/// <param name="fluid">Fluid.</param>
    public FluidUploadContainer SerializeFluid(Fluid fluid, SensorDefinition sensorDefinition) {
      Func<Scalar, Scalar> ToAbs = (Scalar s) => {
        return Physics.ConvertRelativePressureToAbsolute(s, Units.Length.METER.OfScalar(0));
      };

      var ms = new MemoryStream(64);

      var fluidMin = fluid.GetMinimumPressure();
      var fluidMax = fluid.GetMaximumPressure();

      var kpa = Units.Pressure.KILOPASCAL;
      var seaLevel = ToAbs(Units.Pressure.PSIG.OfScalar(0));
      var minPressure = Scalar.Max(ToAbs(sensorDefinition.minimumMeasurement), fluidMin, seaLevel).ConvertTo(kpa);
      var maxPressure = Scalar.Min(ToAbs(sensorDefinition.maximumMeasurement), fluidMax, seaLevel).ConvertTo(kpa);

      var interval = Units.Pressure.PSIG.OfSpan(0.5).ConvertTo(kpa);

      var steps = (int)((maxPressure.amount - minPressure.amount) / interval.magnitude);
      maxPressure = kpa.OfScalar(minPressure.amount + interval.magnitude * steps);

      using (var w = new BinaryWriter(ms)) {
        // Commit the fluid data to the stream
        w.Write((byte)1); // The file version
        w.Write((int)(minPressure.amount * 10)); // Write the minimum pressure
        w.Write((int)(maxPressure.amount * 10)); // Write the maximum pressure

        for (int i = 0; i < steps; i++) {
          var bub = fluid.GetTemperatureFromAbsolutePressure(Fluid.EState.Bubble, kpa.OfScalar(minPressure.amount + i * interval.magnitude));
          var dew = fluid.GetTemperatureFromAbsolutePressure(Fluid.EState.Dew, kpa.OfScalar(minPressure.amount + i * interval.magnitude));
          w.Write((int)(bub.amount * 10)); // Write the bubble temperature
          w.Write((int)(dew.amount * 10)); // Write the dew temperature
        }
      }

      return new FluidUploadContainer(ms.ToArray());
		}

    public class FluidUploadContainer {
      public byte[] buffer;

      public FluidUploadContainer(byte[] buffer) {
        this.buffer = buffer;
      }

      public IEnumerable<FluidFragmentPacket> GetFragmentPackets() {
        var i = 0;
        var pid = 1;

        while (i < buffer.Length) {
          var cnt = Math.Min(18, buffer.Length - i);

          var packet = new FluidFragmentPacket((short)pid++, new Slice<byte>(buffer, i, i + cnt));
          yield return packet;
        }
      }
    }

		/// <summary>
		/// A structure that contains parts of a fluid's lookup table for transfer to a PT Gauge.
		/// </summary>
		public struct FluidFragmentPacket {
			/// <summary>
			/// The id for the packet.
			/// </summary>
			public short pid { get; private set; }
			/// <summary>
			/// The payload contained within the packet.
			/// </summary>
			/// <value>The payload.</value>
			public Slice<byte> payload { get; private set; }

			public FluidFragmentPacket(short pid, Slice<byte> payload) {
				this.pid = pid;
				this.payload = payload;
			}
		}

		/// <summary>
		/// The data presented from a sensor.
		/// </summary>
		internal struct SensorPayload {
			/// <summary>
			/// Queries the unit that is described by the definition byte.
			/// </summary>
			/// <value>The unit.</value>
			public Unit unit { get { return UnitLookup.GetUnit(unitCode); } }
			/// <summary>
			/// Queries whether or not a sensor is connected, according to the byte.
			/// </summary>
			/// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
			public bool connected { get { return (definitionByte & MASK_CONNECTED) == MASK_CONNECTED; } }
			/// <summary>
			/// Queries whether or not the definition byte is in short mode. If not, then the byte is in long mode.
			/// </summary>
			/// <value><c>true</c> if is long mode; otherwise, <c>false</c>.</value>
			public bool isShortMode { get { return (definitionByte & MASK_MODE) == MASK_MODE; } }

			/// <summary>
			/// The raw definition byte that represents the some meta data about the payload.
			/// </summary>
			public byte definitionByte { get; private set; }
			/// <summary>
			/// The measurement presented by the sensor.
			/// </summary>
			/// <value>The measurement.</value>
			public float measurement { get; private set; }
			/// <summary>
			/// The unit code for the sensor payload.
			/// </summary>
			/// <value>The unit code.</value>
			public byte unitCode { get { return (byte)(definitionByte & MASK_UNIT); } }
			/// <summary>
			/// The length of the sensor payload.
			/// </summary>
			/// <value>The length.</value>
			public int length { get { return 2 + (isShortMode ? 2 : 4); } }

			public SensorPayload(byte definitionByte) : this() {
				this.definitionByte = definitionByte;
			}

			internal void Parse(BinaryReader reader) {
				float m = 0;
				if (isShortMode) {
					m = reader.ReadInt16BE();
				} else {
					m = reader.ReadInt32BE();
				}
				var measOff = reader.ReadByte();
				if (measOff != 0) {
					m = m / (float)Math.Pow(10, measOff);
				}

				measurement = m;
			}
		}
	}
}

