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
    private const string TYPE = "type";

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
        var minUnit = UnitLookup.GetUnit(element.Attribute(MIN_UNIT).Value);
        var maxUnit = UnitLookup.GetUnit(element.Attribute(MAX_UNIT).Value);

        var min = element.Attribute(MIN).Value;
        var max = element.Attribute(MAX).Value;

        sensor.min = minUnit.OfScalar(double.Parse(min));
        sensor.max = maxUnit.OfScalar(double.Parse(max));

        sensor.relative = bool.Parse(element.Attribute(RELATIVE).Value);

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
        sensor.minMeasurement = definition.min;
        sensor.maxMeasurement = definition.max;
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
  } // End GaugeDeviceSensorDefinition
}

