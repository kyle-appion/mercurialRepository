using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using ION.Core.App;
using ION.Core.Sensors;
using ION.Core.Sensors.Serialization;

namespace ION.Core.Content.Serialization {
  public class ManifoldJsonConverter : JsonConverter {

    // Overridde from JsonConverter
    public override bool CanConvert(Type objectType) {
      return typeof(Manifold) == objectType;
    }

    // Overridden from JsonConverter
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
      writer.WriteStartObject();

      serializer.Serialize(writer, value, null);

      writer.WriteEndObject();
    }

    // Overridden from JsonConverter
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      var ion = AppState.context;
      var model = serializer.Deserialize(reader) as ManifoldModel;
      return model.ToManifold(ion);
    }

    internal class ManifoldModel {
      [JsonConverter(typeof(DefaultSensorJsonConverter))]
      public Sensor primarySensor { get; set; }
      [JsonConverter(typeof(DefaultSensorJsonConverter))]
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      public Sensor secondarySensor { get; set; }

      public string fluidName { get; set; }

      public ManifoldModel(Manifold manifold) {
        primarySensor = manifold.primarySensor;
        secondarySensor = manifold.secondarySensor;
        fluidName = manifold.ptChart.fluid.name;
      }

      public Manifold ToManifold(IION ion) {
        var ret = new Manifold(primarySensor);
        ret.secondarySensor = secondarySensor;
//        ret.ptChart.fluid = ion.fluidManager.GetFluidAsync(fluidName).Result;
        return ret;
      }
    } // End ManifoldModel
  } // End ManifoldJsonConverter
}

