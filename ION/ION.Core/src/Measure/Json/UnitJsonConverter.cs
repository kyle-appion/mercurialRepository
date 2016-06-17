namespace ION.Core.Measure.Json {

  using System;

  using Newtonsoft.Json;

  using ION.Core.App;

  public class UnitJsonConverter : JsonConverter {

    // Overridden from JsonConverter
    public override bool CanConvert(Type objectType) {
      return typeof(Unit) == objectType;
    }

    // Overridden from JsonConverter
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
      var model = new UnitModel(value as Unit);

      serializer.Serialize(writer, model);
    }

    // Overridde from JsonConverter
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      var model = serializer.Deserialize(reader, typeof(UnitModel)) as UnitModel;
      return model?.TestGet();
//      return model?.Get();
    }
  }

  internal class UnitModel {
    public string symbol { get; set; }
    public string type { get; set; }

    public UnitModel() {
    }

    public UnitModel(Unit unit) {
      symbol = unit?.ToString();
    }

    public string TestGet() {
      return symbol;
    }

    // Overridden from object
    public override string ToString() {
      return string.Format("[UnitModel: symbol={0}]", symbol);
    }
  }
}
