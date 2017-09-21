namespace ION.Core.Sensors.Serialization {

	using System;

	using Newtonsoft.Json;

	using ION.Core.App;
	using ION.Core.Devices;

  public class DefaultSensorJsonConverter : JsonConverter {

    // Overridden from JsonConverter
    public override bool CanConvert(Type objectType) {
      return typeof(GaugeDeviceSensor) == objectType || typeof(Sensor) == objectType;
    }

    // Overridden from JsonConverter
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
      writer.WriteStartObject();

      serializer.Serialize(writer, ToSensorModel(value as Sensor), null);

      writer.WriteEndObject();
    }

    // Overridden from JsonConverter
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      var ion = AppState.context;
      var model = serializer.Deserialize(reader) as ISensorModel;
      return model.ToSensor(ion);
    }

    private ISensorModel ToSensorModel(Sensor sensor) {
      if (sensor is GaugeDeviceSensor) {
        return new GaugeDeviceSensorModel(sensor as GaugeDeviceSensor);
      } else {
        return new DefaultSensorModel(sensor);
      }
    }

    internal interface ISensorModel {
      /// <summary>
      /// Attempts to convert this model to an validatate sensor. Throws an exception
      /// if the model could not be converted into a sensor.
      /// </summary>
      /// <returns>The sensor.</returns>
      Sensor ToSensor(IION ion);
    } // End ISensorModel

    internal class DefaultSensorModel : ISensorModel{
      public string type { get; set; }
      public bool isRelative { get; set; }
      public bool isEditable { get; set; }
      public string name { get; set; }
      public int unit { get; set; }
      public double measurement { get; set; }
      public bool hasMinMeasurement { get; set; }
      public int minUnit { get; set; }
      public double minMeasurement { get; set; }
      public bool hasMaxMeasurement { get; set; }
      public int maxUnit { get; set; }
      public double maxMeasurement { get; set; }

      public DefaultSensorModel(Sensor sensor) {
        type = sensor.type.ToString();
        isRelative = sensor.isRelative;
        isEditable = sensor.isEditable;
        name = sensor.name;
        unit = UnitLookup.GetCode(sensor.unit);
        measurement = sensor.measurement.amount;

        if (sensor.minMeasurement != null) {
          var min = sensor.minMeasurement;
          hasMinMeasurement = true;
          minUnit = UnitLookup.GetCode(min.unit);
          minMeasurement = min.amount;
        }

        if (sensor.maxMeasurement != null) {
          var max = sensor.maxMeasurement;
          hasMaxMeasurement = true;
          maxUnit = UnitLookup.GetCode(max.unit);
          maxMeasurement = max.amount;
        }
      }

      // Overridden from ISensorModel
      public Sensor ToSensor(IION ion) {
        var sensorType = (ESensorType)Enum.Parse(typeof(ESensorType), type);
        //var ret = new ManualSensor(sensorType, AppState.context.preferences.units.DefaultUnitFor(sensorType).OfScalar(0.0), isRelative);
        var ret = new ManualSensor(ion.manualSensorContainer, sensorType, AppState.context.preferences.units.DefaultUnitFor(sensorType).OfScalar(0.0),isRelative);
        ret.name = name;
        ret.SetMeasurement(UnitLookup.GetUnit(unit).OfScalar(measurement));

        if (hasMinMeasurement) {
          ret.minMeasurement = UnitLookup.GetUnit(minUnit).OfScalar(minMeasurement);
        }

        if (hasMaxMeasurement) {
          ret.maxMeasurement = UnitLookup.GetUnit(maxUnit).OfScalar(maxMeasurement);
        }

        return ret;
      }
    } // End DefaultSensorModel

    internal class GaugeDeviceSensorModel : ISensorModel {
      string rawSerial { get; set; }
      int index { get; set; }

      public GaugeDeviceSensorModel(GaugeDeviceSensor sensor) {
        rawSerial = sensor.device.serialNumber.ToString();
        index = sensor.index;
      }

      // Overridden from ISensorModel
      public Sensor ToSensor(IION ion) {
        var serial = GaugeSerialNumber.Parse(rawSerial);
        var device = ion.deviceManager[serial] as GaugeDevice;
        if (device != null) {
          return device[index];
        } else {
        	return null;
          //throw new Exception("Failed to convert GaugeDeviceSensorModel to GaugeDeviceSensor");
        }
      }
    } // End GaugeDeviceSensorModel
  } // End DefaultSensorJsonConverter
}

