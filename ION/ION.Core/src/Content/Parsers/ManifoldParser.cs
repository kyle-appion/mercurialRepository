namespace ION.Core.Content.Parsers {

  using System;
  using System.IO;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  /// <summary>
  /// A ManifoldParser is not a parser in that it is not an IParser<T>. However, is does have versioning and does export
	/// sensors out in a binary format.
  /// </summary>
	public abstract class ManifoldParser {

		private static ManifoldParser CURRENT_PARSER = new V1();

		public abstract int version { get; }
		public abstract void DoWriteManifold(IION ion, Manifold manifold, BinaryWriter writer);
		public abstract Manifold DoReadManifold(IION ion, BinaryReader reader);


		public static void WriteManifold(IION ion, Manifold manifold, BinaryWriter writer) {
			writer.Write(CURRENT_PARSER.version);
			CURRENT_PARSER.DoWriteManifold(ion, manifold, writer);
		}

		/// <summary>
		/// Attempts to read a manifold from the reader. If the manifold cannot or is not read from the reader, we will
		/// return null.
		/// Note: exceptions are NOT caught.
		/// </summary>
		/// <returns>The sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="reader">Reader.</param>
		public static Manifold ReadManifold(IION ion, BinaryReader reader) {
			var v = reader.ReadInt32();
			switch (v) {
				case 1:
					return new V1().DoReadManifold(ion, reader);
				default:
					return null;
			}
		}

		private class V1 : ManifoldParser {
			public override int version { get { return 1; } }

			public override void DoWriteManifold(IION ion, Manifold manifold, BinaryWriter writer) {
				// Write primary sensor
				SensorParser.WriteSensor(ion, manifold.primarySensor, writer);
				// Write secondary sensor
				SensorParser.WriteSensor(ion, manifold.secondarySensor, writer);

				// Write fluid name
				if (manifold.ptChart == null) {
					writer.Write(false); // Doens't have a pt chart.
				} else {
					writer.Write(true); // Does have a pt chart
					writer.Write(Enum.GetName(typeof(Fluid.EState), manifold.ptChart.state));
					if (manifold.ptChart.fluid == null) {
						writer.Write("");
					} else {
						writer.Write(manifold.ptChart.fluid.name);
					}
				}

				// The number of sensor properties that the manifold ha
				writer.Write(manifold.sensorProperties.Count);
				// Write sensor properties
				foreach (var sp in manifold.sensorProperties) {
					WriteSensorProperty(sp, writer);
				}
			}

			public override Manifold DoReadManifold(IION ion, BinaryReader reader) {
				// Read the primary sensor
				Sensor primary = SensorParser.ReadSensor(ion, reader);
				// Assert that the primary sensor is not null;
				if (primary == null) {
					throw new IOException("Cannot load manifold: primary sensor not read");
				}

				// Read the secondary sensor
				Sensor secondary = SensorParser.ReadSensor(ion, reader);

				// Create the inflated manifold
				var ret = new Manifold(primary);
				ret.SetSecondarySensor(secondary);

				if (reader.ReadBoolean()) {
					var state = (Fluid.EState)Enum.Parse(typeof(Fluid.EState), reader.ReadString());
					// Read the fluid name for the manifold
					var fluidName = reader.ReadString();
					Fluid fluid = null;

					if (fluidName != null && !fluidName.Equals("")) {
						fluid = ion.fluidManager.GetFluidAsync(fluidName).Result;
					}

					ret.ptChart = PTChart.New(ion, state, fluid);
				}

				// Read sensor properties
				var propCount = reader.ReadInt32();

				for(int i = 0; i < propCount; i++){
					ReadSensorProperty(ret, reader);
				}

				return ret;
			}

	    private void WriteSensorProperty(ISensorProperty property, BinaryWriter writer) {      
	      var name = property.GetType();
	      writer.Write(name.Name);
	    }

	    private void ReadSensorProperty(Manifold manifold, BinaryReader reader) {
	      var name = reader.ReadString();
	     
	      if (name != null) {
	        if (name.Equals("MinSensorProperty")) {
	          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
	        } else if (name.Equals("MaxSensorProperty")) {
	          manifold.AddSensorProperty(new MaxSensorProperty(manifold.primarySensor));
	        } else if (name.Equals("PTChartSensorProperty")) {
	          manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
	        } else if (name.Equals("RateOfChangeSensorProperty")) {
	          manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold.primarySensor));
	        } else if (name.Equals("SecondarySensorProperty")) {
	          manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
	        } else if (name.Equals("SuperheatSubcoolSensorProperty")) {
	          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
	          //Log.D(this,"");
	        } else if (name.Equals("TimerSensorProperty")) {
	          manifold.AddSensorProperty(new TimerSensorProperty(manifold.primarySensor));
	        } else if (name.Equals("HoldSensorProperty")) {
	          manifold.AddSensorProperty(new HoldSensorProperty(manifold.primarySensor));
	        } else if (name.Equals("AlternateUnitSensorProperty")) {
	          //TODO setup alternate sensor writing with name and unit code 
	          //This will be reader.ReadString() with reader.ReadInt32() right after
	          Log.E(this, "Trying to add alternate sensor property. Not implemented yet");
	        } else {
	          Log.E(this, "Name for sensor property doesn't match an ISensorProperty");
	        }
	      } else {
	        Log.E(this, "Couldn't read the name from the binary file for the sensor property");
	      }
			}
    }
  }
}

