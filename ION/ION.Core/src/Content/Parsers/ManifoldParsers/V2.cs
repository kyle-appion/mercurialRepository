namespace ION.Core.Content.Parsers.ManifoldParsers {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	public class V2 : ManifoldParser {
		// Overridden from ManifoldParser
		public override int version { get { return 1; } }

		private Dictionary<ECode, ISensorPropertyParser> parsers = new Dictionary<ECode, ISensorPropertyParser>();
		private Dictionary<string, ECode> lookup = new Dictionary<string, ECode>();

		public V2() {
			Register<AlternateUnitSensorProperty>(ECode.AlternateUnit, new AlternateUnitParser());
			Register<MinSensorProperty>(ECode.Min);
			Register<MaxSensorProperty>(ECode.Max);
			Register<HoldSensorProperty>(ECode.Hold);

			Register<RateOfChangeSensorProperty>(ECode.RoC, new RoCParser());
			Register<SecondarySensorProperty>(ECode.Secondary);
			Register<TimerSensorProperty>(ECode.Timer);

			Register<PTChartSensorProperty>(ECode.Pt);
			Register<SuperheatSubcoolSensorProperty>(ECode.ShSc);
		}

		private void Register<T>(ECode sp) where T : ISensorProperty {
			lookup[typeof(T).Name] = sp;
			parsers[sp] = new GenericParser<T>(sp);
		}

		private void Register<T>(ECode sp, ISensorPropertyParser parser) where T : ISensorProperty {
			lookup[typeof(T).Name] = sp;
			parsers[sp] = parser;
		}

		// Overridden from ManifoldParser
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

		// Overridden from ManifoldParser
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
				ReadSensorProperty(ion, ret, reader);
			}

			return ret;
		}

		private void WriteSensorProperty(ISensorProperty sp, BinaryWriter writer) {   
			var ms = new MemoryStream();
			using (var bw = new BinaryWriter(ms)) {
				try {
					var code = lookup[sp.GetType().Name];
					var parser = parsers[code];
					bw.Write((int)code);
					if (parser.Write(sp, bw)) {
						bw.Flush();
						writer.Write(ms.ToArray());
					}
				} catch (Exception e) {
					Log.E(this, "Failed to write sensor property {" + sp.GetType().Name + "} to writer", e);
				}
			}
		}

		private void ReadSensorProperty(IION ion, Manifold manifold, BinaryReader reader) {
			var code = (ECode)reader.ReadInt32();

			var parser = parsers[code];
			var sp = parser.Read(ion, manifold, reader);
			if (sp != null) {
				manifold.AddSensorProperty(sp);
			}
		}
	}

	internal enum ECode {
		AlternateUnit,
		Min,
		Max,
		Hold,

		RoC,
		Secondary,
		Timer,

		Pt,
		ShSc,
	}

	internal interface ISensorPropertyParser {
		ECode type { get; }
		/// <summary>
		/// Reads the subview from the parser.
		/// Note: you should NOT add the sensor property the manifold. The top level parser will take care of that for you.
		/// </summary>
		/// <returns>The read.</returns>
		/// <param name="manifold">Manifold.</param>
		/// <param name="reader">Reader.</param>
		ISensorProperty Read(IION ion, Manifold manifold, BinaryReader reader);
		/// <summary>
		/// Called to let the subview parser write its own data.
		/// Note: you only need to write the subviews data, not its type information unless the subview needs if for its
		/// own uses.
		/// </summary>
		/// <returns>The write.</returns>
		/// <param name="t">T.</param>
		/// <param name="writer">Writer.</param>
		bool Write(ISensorProperty sensorProperty, BinaryWriter writer);
	}

	internal class GenericParser<T> : ISensorPropertyParser where T : ISensorProperty {
		// Implemented for ISubviewParser
		public ECode type { get; private set; }

		public GenericParser(ECode type) {
			this.type = type;
		}

		// Implemented for ISubviewParser
		public ISensorProperty Read(IION ion, Manifold manifold, BinaryReader reader) {
			return (ISensorProperty)Activator.CreateInstance(typeof(T), manifold);
		}

		// Implemented for ISubviewParser
		public bool Write(ISensorProperty sp, BinaryWriter writer) {
			return true;
		}
	}

	internal class AlternateUnitParser : ISensorPropertyParser {
		// Implemented for ISubviewParser
		public ECode type { get { return ECode.AlternateUnit; } }

		// Implemented for ISubviewParser
		public ISensorProperty Read(IION ion, Manifold manifold, BinaryReader reader) {
			var ret = new AlternateUnitSensorProperty(manifold);

			var usunit = reader.ReadInt32();

			ret.unit = UnitLookup.GetUnit(usunit);

			return ret;
		}

		// Implemented for ISubviewParser
		public bool Write(ISensorProperty sp, BinaryWriter writer) {
			try {
				var alt = (AlternateUnitSensorProperty)sp;
				writer.Write(UnitLookup.GetCode(alt.unit));
				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to write.", e);
				return false;
			}
		}
	}

	internal class RoCParser : ISensorPropertyParser {
		// Implemented for ISubviewParser
		public ECode type { get { return ECode.RoC; } }

		// Implemented for ISubviewParser
		public ISensorProperty Read(IION ion, Manifold manifold, BinaryReader reader) {
      var ret = new RateOfChangeSensorProperty(manifold, ion.preferences.device.rateOfChangeInterval);

      ret.Resize(ion.preferences.device.rateOfChangeInterval);
			ret.flags = (RateOfChangeSensorProperty.EFlags)reader.ReadInt32();

			return ret;
		}

		// Implemented for ISubviewParser
		public bool Write(ISensorProperty sp, BinaryWriter writer) {
			try {
				var roc = (RateOfChangeSensorProperty)sp;

				writer.Write(roc.window.TotalMilliseconds);
				writer.Write(roc.interval.TotalMilliseconds);
				writer.Write((int)roc.flags);

				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to write.", e);
				return false;
			}
		}
	}
}
