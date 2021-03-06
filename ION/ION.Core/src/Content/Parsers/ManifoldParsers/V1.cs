﻿namespace ION.Core.Content.Parsers.ManifoldParsers {

	using System;
	using System.IO;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	public class V1 : ManifoldParser {
		public override int version { get { return 1; } }

		//public override void DoWriteManifold(IION ion, Manifold manifold, BinaryWriter writer){
	  public override void DoWriteManifold(IION ion, Sensor sensor, BinaryWriter writer) {
			// Write primary sensor
			SensorParser.WriteSensor(ion, sensor, writer);
			// Write secondary sensor
			SensorParser.WriteSensor(ion, sensor.linkedSensor, writer);

			// Write fluid name
			//if (sensor.ptChart == null) {
			//	writer.Write(false); // Doens't have a pt chart.
			//} else {
			//	writer.Write(true); // Does have a pt chart
			//	writer.Write(Enum.GetName(typeof(Fluid.EState), sensor.ptChart.state));
			//	if (sensor.ptChart.fluid == null) {
			//		writer.Write("");
			//	} else {
			//		writer.Write(sensor.ptChart.fluid.name);
			//	}
			//}

			// The number of sensor properties that the manifold ha
			writer.Write(sensor.sensorProperties.Count);
			// Write sensor properties
			foreach (var sp in sensor.sensorProperties) {
				WriteSensorProperty(sp, writer);
			}
		}

		//public override Manifold DoReadManifold(IION ion, BinaryReader reader){
		public override Sensor DoReadManifold(IION ion, BinaryReader reader) {
			// Read the primary sensor
			Sensor primary = SensorParser.ReadSensor(ion, reader);
			// Assert that the primary sensor is not null;
			if (primary == null) {
				throw new IOException("Cannot load manifold: primary sensor not read");
			}

			// Read the secondary sensor
			Sensor secondary = SensorParser.ReadSensor(ion, reader);

			// Create the inflated manifold
			//var ret = new Manifold(primary);
			var ret = primary;
			ret.SetLinkedSensor(secondary);

			if (reader.ReadBoolean()) {
				var state = (Fluid.EState)Enum.Parse(typeof(Fluid.EState), reader.ReadString());
				// Read the fluid name for the manifold
				var fluidName = reader.ReadString();
				Fluid fluid = null;

				if (fluidName != null && !fluidName.Equals("")) {
          fluid = ion.fluidManager.LoadFluidAsync(fluidName).Result;
				}

        //ret.ptChart = fluid.GetPtChart(state);
			}

			// Read sensor properties
			var propCount = reader.ReadInt32();

			for(int i = 0; i < propCount; i++) {
				ReadSensorProperty(ion, ret, reader);
			}

			return ret;
		}

		private void WriteSensorProperty(ISensorProperty property, BinaryWriter writer) {      
			var name = property.GetType();
			writer.Write(name.Name);

			if (property is AlternateUnitSensorProperty) {
				var sp = property as AlternateUnitSensorProperty;
				writer.Write(UnitLookup.GetCode(sp.unit));
			}
		}

		//private void ReadSensorProperty(IION ion, Manifold manifold, BinaryReader reader)	{
			private void ReadSensorProperty(IION ion, Sensor sensor, BinaryReader reader) {
			var name = reader.ReadString();

			if (name != null) {
				if (name.Equals(typeof(MinSensorProperty).Name)) {
					//manifold.AddSensorProperty(new MinSensorProperty(manifold));
					sensor.AddSensorProperty(new MinSensorProperty(sensor));
				} else if (name.Equals(typeof(MaxSensorProperty).Name)) {
					//manifold.AddSensorProperty(new MaxSensorProperty(manifold));
					sensor.AddSensorProperty(new MaxSensorProperty(sensor));
				} else if (name.Equals(typeof(PTChartSensorProperty).Name)) {
					//manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
					sensor.AddSensorProperty(new PTChartSensorProperty(sensor));
				} else if (name.Equals(typeof(RateOfChangeSensorProperty).Name)) {
					//manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold, ion.preferences.device.trendInterval));
					sensor.AddSensorProperty(new RateOfChangeSensorProperty(sensor, ion.preferences.device.trendInterval));
				} else if (name.Equals(typeof(SecondarySensorProperty).Name)) {
					//manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
					sensor.AddSensorProperty(new SecondarySensorProperty(sensor));
				} else if (name.Equals(typeof(SuperheatSubcoolSensorProperty).Name)) {
					//manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
					sensor.AddSensorProperty(new SuperheatSubcoolSensorProperty(sensor));
				} else if (name.Equals(typeof(TimerSensorProperty).Name)) {
					//manifold.AddSensorProperty(new TimerSensorProperty(manifold));
					sensor.AddSensorProperty(new TimerSensorProperty(sensor));
				} else if (name.Equals(typeof(HoldSensorProperty).Name)) {
					//manifold.AddSensorProperty(new HoldSensorProperty(manifold));
					sensor.AddSensorProperty(new HoldSensorProperty(sensor));
				} else if (name.Equals(typeof(AlternateUnitSensorProperty).Name)) {
					var sp = new AlternateUnitSensorProperty(sensor);
					//manifold.AddSensorProperty(sp);
					sensor.AddSensorProperty(sp);
					var usunit = reader.ReadInt32();
					sp.unit = UnitLookup.GetUnit(usunit);
				} else {
					Log.E(this, "Name for sensor property doesn't match an ISensorProperty");
				}
			} else {
				Log.E(this, "Couldn't read the name from the binary file for the sensor property");
			}
		}
	}
}
