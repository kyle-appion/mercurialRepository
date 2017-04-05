namespace ION.Core.Content.Parsers {

	using System;
	using System.IO;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.IO;
	using ION.Core.Sensors;

	public class AnalyzerParser : IParser<Analyzer> {
		public int version { get { return 1; } }


		public void WriteToStream(IION ion, Analyzer analyzer, Stream stream) {
			using (var writer = new BinaryWriter(stream)) {
				writer.Write(version);
				// Write the length per side of the analyzer
				writer.Write(analyzer.sensorsPerSide);
				// Write all of the sensors that are present in the analyzer
				for (int i = 0; i < analyzer.sensorsPerSide * 2; i++) {
					SensorParser.WriteSensor(ion, analyzer[i], writer);
				}

				// Write the low side manifold
				if (analyzer.lowSideManifold != null) {
					if (!(analyzer.lowSideManifold.primarySensor is ManualSensor)) {
						writer.Write(true);
						ManifoldParser.WriteManifold(ion, analyzer.lowSideManifold, writer);						
					}
				} else {
					writer.Write(false);
				}

				// Write the high side manifold
				if (analyzer.highSideManifold != null) {
					if (!(analyzer.highSideManifold.primarySensor is ManualSensor)) {
						writer.Write(true);
						ManifoldParser.WriteManifold(ion, analyzer.highSideManifold, writer);
					}
				} else {
					writer.Write(false);
				}
			}
		}

		public Analyzer ReadFromStream(IION ion, Stream stream) {
			var buffer = new byte[1024];
			var len = stream.Read(buffer, 0, buffer.Length);

			var b = new byte[len];
			Array.Copy(buffer, b, len);
			var ms = new MemoryStream(b);

			using (var reader = new BinaryReader(ms)) {
				Analyzer ret = null;

				// Read the version of the parser
				var v = reader.ReadInt32();
				if (version != v) {
					throw new Exception("Cannot read analyzer from stream: expected version " + this.version + " received " + v);
				}
				// Read the number of sensor per side
				var sensorsPerSide = reader.ReadInt32();
				// Create the analyzer that we are inflating
				ret = new Analyzer(ion, sensorsPerSide);
				for (int i = 0; i < sensorsPerSide * 2; i++) {
					var sensor = SensorParser.ReadSensor(ion, reader);
					if (sensor != null) {
						ret.PutSensor(i, sensor, true);
          }
				}

				// Read the low side manifold
				if (reader.ReadBoolean()) {
					var manifold = ManifoldParser.ReadManifold(ion, reader);
					ret.SetManifold(Analyzer.ESide.Low, manifold.primarySensor);

					ret.lowSideManifold.SetSecondarySensor(manifold.secondarySensor);
					foreach (var sp in manifold.sensorProperties) {
						ret.lowSideManifold.AddSensorProperty(sp);
					}
				}

				// Read the high side manifold
				if (reader.ReadBoolean()) {
					var manifold = ManifoldParser.ReadManifold(ion, reader);
					ret.SetManifold(Analyzer.ESide.High, manifold.primarySensor);

					ret.highSideManifold.SetSecondarySensor(manifold.secondarySensor);
					foreach (var sp in manifold.sensorProperties) {
						ret.highSideManifold.AddSensorProperty(sp);
					}
				}

				return ret;
			}
		}
	}
}

