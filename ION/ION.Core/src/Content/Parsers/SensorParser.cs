namespace ION.Core.Content.Parsers {

	using System;
	using System.IO;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.IO;
	using ION.Core.Sensors;
	using ION.Core.Util;

	/// <summary>
	/// A Sensor parser is not a parser in that it is not an IParser<T>. However, is does have versioning and does export
	/// senors out in a binary format.
	/// </summary>
	public abstract class SensorParser {

		private static SensorParser CURRENT_PARSER = new V1();

		abstract public int version { get; }
		abstract public void DoWriteSensor(IION ion, Sensor sensor, BinaryWriter writer);
		abstract public Sensor DoReadSensor(IION ion, BinaryReader reader);

		/// <summary>
		/// Attempts to write the sensor to the writer.
		/// </summary>
		/// <param name="ion">Ion.</param>
		/// <param name="sensor">Sensor.</param>
		/// <param name="writer">Writer.</param>
		public static void WriteSensor(IION ion, Sensor sensor, BinaryWriter writer) {
			writer.Write(CURRENT_PARSER.version);
			CURRENT_PARSER.DoWriteSensor(ion, sensor, writer);
		}

		/// <summary>
		/// Attempts to read a sensor from the reader. If the sensor cannot or is not read from the reader, we will return
		/// null.
		/// Note: exceptions are NOT caught.
		/// </summary>
		/// <returns>The sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="reader">Reader.</param>
		public static Sensor ReadSensor(IION ion, BinaryReader reader) {
			var v = reader.ReadInt32();
			switch (v) {
				case 1:
					return new V1().DoReadSensor(ion, reader);
				default:
					return null;
			}
		}

		private class V1 : SensorParser {
			public override int version { get { return 1; } }

			public override void DoWriteSensor(IION ion, Sensor sensor, BinaryWriter writer) {
				var type = sensor.FindPersistentType();
				switch (type) {
					case EPersistSensorType.GaugeDeviceSensor:
						// Write the internal type of the sensor
						writer.Write((int)EPersistSensorType.GaugeDeviceSensor);
						WriteGaugeDeviceSensor((GaugeDeviceSensor)sensor, writer);
						break;
					case EPersistSensorType.ManualSensor:
						// Write the internal type of the sensor
						writer.Write((int)EPersistSensorType.ManualSensor);
						WriteManualSensor((ManualSensor)sensor, writer);
						break;
					case EPersistSensorType.NullSensor:
						// Write the internal type of the sensor.
						writer.Write((int)EPersistSensorType.NullSensor);
						WriteNullSensor(writer);
						break;
					default:
						throw new Exception("Failed to write sensor of uknown persistent type: " + type);
				}
			}

			public override Sensor DoReadSensor(IION ion, BinaryReader reader) {
				var type = (EPersistSensorType)reader.ReadInt32();
				switch (type) {
					case EPersistSensorType.GaugeDeviceSensor:
						return ReadGaugeDeviceSensor(ion.deviceManager, reader);
					case EPersistSensorType.ManualSensor:
						return ReadManualSensor(reader);
					case EPersistSensorType.NullSensor:
						return null;
					default:
						throw new Exception("Failed to parse sensor from stream with persistent sensor type of: " + type);
				}
			}

			private void WriteGaugeDeviceSensor(GaugeDeviceSensor sensor, BinaryWriter writer) {
				// Write the serial number of the sensor's device
				writer.Write(sensor.device.serialNumber.ToString());
				// Write the sensor index of the sensor
				writer.Write(sensor.index);
			}

			private void WriteManualSensor(Sensor sensor, BinaryWriter writer) {
				// Write actual sensor type
				writer.Write((int)sensor.type);
				// Write whether or not the sensor is relative
				writer.Write(sensor.isRelative);
				// Write the unit code of the sensor
				writer.Write((byte)UnitLookup.GetCode(sensor.measurement.unit));
				// Write the double amount of the sensor
				writer.Write(sensor.measurement.amount);
				// Write the minimum measurement of the sensor
				if (sensor.minMeasurement.unit == null) {
					writer.Write(0);
				} else {
					writer.Write((byte)UnitLookup.GetCode(sensor.minMeasurement.unit));
					writer.Write(sensor.minMeasurement.amount);
				}
				// Write the maximum measurement of the sensor
				if (sensor.maxMeasurement.unit == null) {
					writer.Write(0);
				} else {
					writer.Write((byte)UnitLookup.GetCode(sensor.maxMeasurement.unit));
					writer.Write(sensor.maxMeasurement.amount);
				}

				if (string.IsNullOrEmpty(sensor.name)) {
					sensor.name = "Manual";
					writer.Write(sensor.name);
				} else {
					writer.Write(sensor.name);
				}
			}

			private void WriteNullSensor(BinaryWriter writer) {
				// Nope
			}

			private GaugeDeviceSensor ReadGaugeDeviceSensor(IDeviceManager dm, BinaryReader reader) {
				// Read the serial number of the device
				var rawSerial = reader.ReadString();
				// Read the index of the sensor
				var index = reader.ReadInt32();

				if (SerialNumberExtensions.IsValidSerialNumber(rawSerial)) {
					var sn = SerialNumberExtensions.ParseSerialNumber(rawSerial);
					var device = dm[sn] as GaugeDevice;
					if (device != null) {
						if (index >= 0 && index < device.sensorCount) {
							return device[index];
						}
					}
				}

				Log.E(this, "Failed to load sensor: " + rawSerial);
				return null;
			}

			private Sensor ReadManualSensor(BinaryReader reader) {
				// Read the actual sensor type
				var sensorType = (ESensorType)reader.ReadInt32();
				// Read whether or not the sensor is relative
				var relative = reader.ReadBoolean();
				// Read and determine the unit code
				var rawUnit = reader.ReadByte();
				// Read the amount of the sensor
				var amount = reader.ReadDouble();
				// Read the minimum measurement of the sensor
				var rawMinUnit = reader.ReadByte();
				var rawMinAmount = 0.0;
				if (rawMinUnit != 0) {
					rawMinAmount = reader.ReadDouble();
				}
				// Read the maximum measurement of the sensor
				var rawMaxUnit = reader.ReadByte();
				var rawMaxAmount = 0.0;
				if (rawMaxUnit != 0) {
					rawMaxAmount = reader.ReadDouble();
				}

				var name = reader.ReadString();

				var ret = new ManualSensor(sensorType, relative);
				ret.measurement = UnitLookup.GetUnit(rawUnit).OfScalar(amount);

				if (rawMinUnit != 0) {
					ret.minMeasurement = UnitLookup.GetUnit(rawMinUnit).OfScalar(rawMinAmount);
				}

				if (rawMaxUnit != 0) {
					ret.maxMeasurement = UnitLookup.GetUnit(rawMaxUnit).OfScalar(rawMaxAmount);
				}

				ret.name = name;

				return ret;
			}
		}
	}

	/// <summary>
	/// An enumeration of the sensor types that the parser supports.
	/// </summary>
	internal enum EPersistSensorType {
		/// <summary>
		/// Enumerates a null reference to a sensor. This is only allowed for a manifold's second sensor.
		/// </summary>
		NullSensor,
		ManualSensor,
		GaugeDeviceSensor,
	}

	internal static class SensorParserSensorExtensions {
		public static EPersistSensorType FindPersistentType(this Sensor sensor) {
			if (sensor is GaugeDeviceSensor) {
				return EPersistSensorType.GaugeDeviceSensor;
			} else if (sensor is ManualSensor) {
				return EPersistSensorType.ManualSensor;
			} else {
				if (sensor != null) {
					Log.E(typeof(SensorParserSensorExtensions).Name, "Failed to write unknown sensor of type: " + sensor.GetType().Namespace + "#" + sensor.GetType().Name);
				}
				return EPersistSensorType.NullSensor;
			}
		}
	}
}

