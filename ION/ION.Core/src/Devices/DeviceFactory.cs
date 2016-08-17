namespace ION.Core.Devices {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Xml;
  using System.Xml.Linq;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  /// <summary>
  /// DeviceFactory is a construct is used to provide pre-constructed device meta data. Before a device enters the
  /// device manager for use in the rest of the application, its data and constraints have to be loaded from the
  /// devices.xml file to know what a device can and cannot do.
  /// </summary>
  public class DeviceFactory {
    private const string DEVICE_CODE = "deviceCode";
    private const string GAUGE_DEVICE = "GaugeDevice";
    private const string GAUGE_DEVICE_SENSOR = "GaugeDeviceSensor";
    private const string MIN = "min";
    private const string MIN_UNIT = "minUnit";
    private const string MAX = "max";
    private const string MAX_UNIT = "maxUnit";
    private const string RELATIVE = "relative";
    private const string REMOVABLE = "removeable";
    private const string TYPE = "type";
    private const string SUPPORTED_UNIT = "SupportedUnit";
    private const string INTERNAL_DEVICE = "InternalDevice";

    /// <summary>
    /// The dictionary of device definitions.
    /// </summary>
    private Dictionary<string, IDeviceDefinition> definitions = new Dictionary<string, IDeviceDefinition>();

    private DeviceFactory(Dictionary<string, IDeviceDefinition> definitions) {
      this.definitions = definitions;
    }

    /// <summary>
    /// Queries the device definition from the given serial number.
    /// </summary>
    /// <returns>The device definition.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public IDeviceDefinition GetDeviceDefinition(ISerialNumber serialNumber) {
      return definitions[serialNumber.deviceCode];
    }

    /// <summary>
    /// Creates a new DeviceFactory from the given stream. The stream must point to a valid devices.xml data file.
    /// </summary>
    /// <exception cref="IOException">If the device factory could not be created from the given stream.</exception>
    /// <param name="stream">Stream.</param>
    public static DeviceFactory CreateFromStream(Stream stream) {
      try {
        var root = XElement.Load(XmlReader.Create(stream));
        var devices = root.Elements();

        var defs = new Dictionary<string, IDeviceDefinition>();
        foreach (var element in devices) {
          if (GAUGE_DEVICE.Equals(element.Name.LocalName)) {
            var def = ParseGaugeDeviceDefinition(element);
            defs.Add(def.deviceCode, def);
          }
        }

        return new DeviceFactory(defs);
      } catch (Exception e) {
        throw new IOException("Failed to create DeviceFactory from stream", e);
      }
    }

    /// <summary>
    /// Parses a gauge device definition from the xml element.
    /// </summary>
    /// <returns>The gauge device definition.</returns>
    /// <param name="element">Element.</param>
    private static GaugeDeviceDefinition ParseGaugeDeviceDefinition(XElement element) {
      return new GaugeDeviceDefinition() {
        deviceCode = element.Attribute(DEVICE_CODE).Value,
        sensorDefinitions = ParseGaugeDeviceSensors(element.Elements().ToArray())
      };
    }

    /// <summary>
    /// Parses an array of gauge device sensor definitions from the xml elements.
    /// </summary>
    /// <returns>The gauge device sensors.</returns>
    /// <param name="elements">Elements.</param>
    private static List<SensorDefinition> ParseGaugeDeviceSensors(XElement[] elements) {
      var ret = new List<SensorDefinition>();

      foreach (var element in elements) {
        var st = (ESensorType)Enum.Parse(typeof(ESensorType), element.Attribute(TYPE).Value, true);
        var minUnit = UnitLookup.GetUnit(st, element.Attribute(MIN_UNIT).Value);
        var maxUnit = UnitLookup.GetUnit(st, element.Attribute(MAX_UNIT).Value);
        var relative = true;
        var removable = false;

        var relatt = element.Attribute(RELATIVE);
        if (relatt != null) {
          if (!bool.TryParse(relatt.Value, out relative)) {
            relative = true;
          }
        }

        var rematt = element.Attribute(REMOVABLE);
        if (rematt != null) {
          if (!bool.TryParse(rematt.Value, out removable)) {
            removable = false;
          }
        }

        var minatt = element.Attribute(MIN).Value;
        var maxatt = element.Attribute(MAX).Value;
        var min = minUnit.OfScalar(double.Parse(minatt));
        var max = maxUnit.OfScalar(double.Parse(maxatt));

        var units = new List<Unit>();
        foreach (var e in element.Elements()) {
          if (SUPPORTED_UNIT.Equals(e.Name.LocalName)) {
            units.Add(UnitLookup.GetUnit(st, e.Value));
          }
        }

        ret.Add(new SensorDefinition() {
          sensorType = st,
          minimumMeasurement = min,
          maximumMeasurement = max,
          isRelative = relative,
          isRemovable = removable,
          supportedUnits = units,
        });
      }

      return ret;
    }
  }

  /// <summary>
  /// An interface that describes a simple device definition.
  /// </summary>
  public interface IDeviceDefinition {
    /// <summary>
    /// The type of device this definition is describing.
    /// </summary>
    /// <value>The type of the device.</value>
    EDeviceType deviceType { get; }
    /// <summary>
    /// The device code that is used by the serial number to identify the device.
    /// </summary>
    /// <value>The device code.</value>
    string deviceCode { get; }

    /// <summary>
    /// Creates an IDevice from this device definition.
    /// </summary>
    /// <returns>The device.</returns>
    /// <exception cref="ArgumentException">If the IDevice could not be created with the given arguments.</exception>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="connection">Connection.</param>
    /// <param name="protocol">Protocol.</param>
    IDevice CreateDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol);
  }

  /// <summary>
  /// A simple device definition that devices what a specific type of gauge device is.
  /// </summary>
  public class GaugeDeviceDefinition : IDeviceDefinition {
    /// <summary>
    /// Gets the type of the device.
    /// </summary>
    /// <value>The type of the device.</value>
    public EDeviceType deviceType {
      get {
        return EDeviceType.Gauge;
      }
    }
    /// <summary>
    /// The device code that is used to identify the gauge device definition.
    /// </summary>
    /// <value>The device code.</value>
    public string deviceCode { get; internal set; }
    /// <summary>
    /// A list of the gauge device sensors that the gauge contains.
    /// </summary>
    public List<SensorDefinition> sensorDefinitions { get; internal set; }

    /// <summary>
    /// Gets the sensor defintion at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    public SensorDefinition this[int index] {
      get {
        return sensorDefinitions[index];
      }
    }

    /// <summary>
    /// Creates an IDevice from this device definition.
    /// </summary>
    /// <returns>The device.</returns>
    /// <exception cref="ArgumentException">If the IDevice could not be created with the given arguments.</exception>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="connection">Connection.</param>
    /// <param name="protocol">Protocol.</param>
    public IDevice CreateDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      if (!(serialNumber is GaugeSerialNumber)) {
        throw new ArgumentException("Cannot create device: expected GaugeSerialNumber, received: " + serialNumber.GetType().Name);
      }

      if (!(protocol is IGaugeProtocol)) {
        throw new ArgumentException("Cannot create device: expected an IGaugeProtocol, received: " + protocol.GetType().Name);
      }

      var ret = new GaugeDevice(serialNumber as GaugeSerialNumber, connection, protocol as IGaugeProtocol);
      var s = new List<GaugeDeviceSensor>();

      int i = 0;
      foreach (var sensorDefinition in sensorDefinitions) {
        var sensor = new GaugeDeviceSensor(ret, i++, sensorDefinition.sensorType, sensorDefinition.isRelative);
        sensor.removable = sensorDefinition.isRemovable;
        sensor.minMeasurement = sensorDefinition.minimumMeasurement;
        sensor.maxMeasurement = sensorDefinition.maximumMeasurement;
        sensor.supportedUnits = sensorDefinition.supportedUnits.ToArray();
        s.Add(sensor);
      }

      ret.sensors = s.ToArray();

      return ret;
    }
  }

  /// <summary>
  /// A simple sensor definition that describes a sensor.
  /// </summary>
  public class SensorDefinition {
    /// <summary>
    /// The sensor type of the sensor definition.
    /// </summary>
    /// <value>The type of the sensor.</value>
    public ESensorType sensorType { get; internal set; }
    /// <summary>
    /// The minimum measurement of the sensor.
    /// </summary>
    /// <value>The minimum measurement.</value>
    public Scalar minimumMeasurement { get; internal set; }
    /// <summary>
    /// The maximim measurement of the sensor.
    /// </summary>
    /// <value>The maximim measurement.</value>
    public Scalar maximumMeasurement { get; internal set; }
    /// <summary>
    /// Whether or not the sensor is a relative sensor.
    /// </summary>
    /// <value><c>true</c> if is relative; otherwise, <c>false</c>.</value>
    public bool isRelative { get; internal set; }
    /// <summary>
    /// Whether or not the sensor is a removable sensor.
    /// </summary>
    /// <value><c>true</c> if is removable; otherwise, <c>false</c>.</value>
    public bool isRemovable { get; internal set; }
    /// <summary>
    /// The list of units that the sensor supports.
    /// </summary>
    /// <value>The supported units.</value>
    public List<Unit> supportedUnits { get; internal set; }
  }
}


/*
 * 
internal interface IDeviceDefinition {
  bool Matches(ISerialNumber serialNumber);
  IDevice ToDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol);
} // End IDeviceDefinition

internal class GaugeDeviceDefinition : IDeviceDefinition {
  public string deviceCode { get; set; }
  public GaugeDeviceSensorDefinition[] sensors { get; set; }

  // Overridden from IDeviceDefinition
  public bool Matches(ISerialNumber serialNumber) {
    var gsn = serialNumber as GaugeSerialNumber;
    if (gsn != null && gsn.deviceModel.GetModelCode().Equals(deviceCode)) {
      return true;
    } else {
      return false;
    }
  }

  // Overridden from IDeviceDefinition
  public IDevice ToDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
    var ret = new GaugeDevice(serialNumber as GaugeSerialNumber, connection, protocol as IGaugeProtocol);
    var s = new List<GaugeDeviceSensor>();

    int i = 0;
    foreach (var definition in sensors) {
      var sensor = new GaugeDeviceSensor(ret, i++, definition.type, definition.relative);
      sensor.removable = definition.removable;
      sensor.minMeasurement = definition.min;
      sensor.maxMeasurement = definition.max;
      sensor.supportedUnits = definition.supportedUnits;
      s.Add(sensor);
    }

    ret.sensors = s.ToArray();

    return ret;
  }
} // End GaugeDeviceDefinition

internal class GaugeDeviceSensorDefinition { 
  public ESensorType type { get; set; }
  public Scalar min { get; set; }
  public Scalar max { get; set; }
  public bool relative { get; set; }
  public bool removable { get; set; }
  public Unit[] supportedUnits { get; set; }
} // End GaugeDeviceSensorDefinition




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Devices.Protocols;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Devices {
  public class DeviceFactory {
    private const string DEVICE_CODE = "deviceCode";
    private const string GAUGE_DEVICE = "GaugeDevice";
    private const string GAUGE_DEVICE_SENSOR = "GaugeDeviceSensor";
    private const string MIN = "min";
    private const string MIN_UNIT = "minUnit";
    private const string MAX = "max";
    private const string MAX_UNIT = "maxUnit";
    private const string RELATIVE = "relative";
    private const string REMOVABLE = "removeable";
    private const string TYPE = "type";
    private const string SUPPORTED_UNIT = "SupportedUnit";

    private List<IDeviceDefinition> __definitions = new List<IDeviceDefinition>();

    /// <summary>
    /// Attempts to instantiate a device from a definition.
    /// </summary>
    /// <returns>The device from serial number.</returns>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="connection">Connection.</param>
    public IDevice CreateDeviceFromSerialNumber(ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      foreach (var definition in __definitions) {
        if (definition.Matches(serialNumber)) {
          return definition.ToDevice(serialNumber, connection, protocol);
        }
      }

      return null;
    }

    /// <summary>
    /// Creates a new DeviceParser from the given stream. This method will not close
    /// the stream when the device parser is loaded.
    /// </summary>
    /// <returns>The from stream.</returns>
    /// <param name="deviceDefinitionStream">Device definition stream.</param>
    public static DeviceFactory CreateFromStream(Stream deviceDefinitionStream) {
      try {
        var root = XElement.Load(XmlReader.Create(deviceDefinitionStream));

        var devices = root.Elements();

        var ret = new DeviceFactory();

        foreach (var element in devices) {
          if (GAUGE_DEVICE.Equals(element.Name.LocalName)) {
            var deviceDefinitions = ParseGaugeDeviceDefinition(element);
            ret.__definitions.Add(deviceDefinitions);
          }
        }

        return ret;
      } catch (Exception e) {
        Log.C("DeviceFactory", "Failed to parse shit", e);
        throw e;
      }
    }

    private static GaugeDeviceDefinition ParseGaugeDeviceDefinition(XElement element) {
      return new GaugeDeviceDefinition() {
        deviceCode = element.Attribute(DEVICE_CODE).Value,
        sensors = ParseGaugeDeviceSensors(element.Elements().ToArray())
      };
    }

    private static GaugeDeviceSensorDefinition[] ParseGaugeDeviceSensors(XElement[] elements) {
      var ret = new List<GaugeDeviceSensorDefinition>();

      foreach (var element in elements) {
        var sensor = new GaugeDeviceSensorDefinition();

        sensor.type = (ESensorType)Enum.Parse(typeof(ESensorType), element.Attribute(TYPE).Value, true);
        var minUnit = UnitLookup.GetUnit(sensor.type, element.Attribute(MIN_UNIT).Value);
        var maxUnit = UnitLookup.GetUnit(sensor.type, element.Attribute(MAX_UNIT).Value);

        var min = element.Attribute(MIN).Value;
        var max = element.Attribute(MAX).Value;

        try {
          var att = element.Attribute(REMOVABLE);
          if (att != null) {
            sensor.removable = bool.Parse(att?.Value);
          }
        } catch (Exception e) {
          Log.D("DeviceFactory", "Failed to parse removable", e);
        }

        sensor.min = minUnit.OfScalar(double.Parse(min));
        sensor.max = maxUnit.OfScalar(double.Parse(max));

        sensor.relative = bool.Parse(element.Attribute(RELATIVE).Value);

        var supportedUnits = new List<Unit>();
        foreach (var e in element.Elements()) {
          if (SUPPORTED_UNIT.Equals(e.Name.LocalName)) {
            supportedUnits.Add(UnitLookup.GetUnit(sensor.type, e.Value));
          }
        }
        sensor.supportedUnits = supportedUnits.ToArray();

        ret.Add(sensor);
      }

      return ret.ToArray();
    }
  }

  internal interface IDeviceDefinition {
    bool Matches(ISerialNumber serialNumber);
    IDevice ToDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol);
  } // End IDeviceDefinition

  internal class GaugeDeviceDefinition : IDeviceDefinition {
    public string deviceCode { get; set; }
    public GaugeDeviceSensorDefinition[] sensors { get; set; }

    // Overridden from IDeviceDefinition
    public bool Matches(ISerialNumber serialNumber) {
      var gsn = serialNumber as GaugeSerialNumber;
      if (gsn != null && gsn.deviceModel.GetModelCode().Equals(deviceCode)) {
        return true;
      } else {
        return false;
      }
    }

    // Overridden from IDeviceDefinition
    public IDevice ToDevice(ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var ret = new GaugeDevice(serialNumber as GaugeSerialNumber, connection, protocol as IGaugeProtocol);
      var s = new List<GaugeDeviceSensor>();

      int i = 0;
      foreach (var definition in sensors) {
        var sensor = new GaugeDeviceSensor(ret, i++, definition.type, definition.relative);
        sensor.removable = definition.removable;
        sensor.minMeasurement = definition.min;
        sensor.maxMeasurement = definition.max;
        sensor.supportedUnits = definition.supportedUnits;
        s.Add(sensor);
      }

      ret.sensors = s.ToArray();

      return ret;
    }
  } // End GaugeDeviceDefinition

  internal class GaugeDeviceSensorDefinition { 
    public ESensorType type { get; set; }
    public Scalar min { get; set; }
    public Scalar max { get; set; }
    public bool relative { get; set; }
    public bool removable { get; set; }
    public Unit[] supportedUnits { get; set; }
  } // End GaugeDeviceSensorDefinition
}

*/