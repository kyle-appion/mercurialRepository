using System;

using ION.Core.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Devices {
  /// <summary>
  /// A factory for creating devices from serial numbers.
  /// </summary>
  public abstract class DeviceFactory {
    /// <summary>
    /// All of the factories that the application is aware of.
    /// </summary>
    private static readonly DeviceFactory[] FACTORIES = new DeviceFactory[] {
      new P300DeviceFactory(),
      new P500DeviceFactory(),
      new P800DeviceFactory(),
      new PT800DeviceFactory(),
      new AV760DeviceFactory(),
    };

    /// <summary>
    /// Finds a factory for the given serial number.
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    public static DeviceFactory FindFactoryFor(ISerialNumber serialNumber) {
      foreach (DeviceFactory factory in FACTORIES) {
        if (factory.IsSerialNumberValid(serialNumber)) {
          return factory;
        }
      }

      throw new ArgumentException("Cannot find factory: a factory does not exist for " + serialNumber);
    }

    /// <summary>
    /// Queries whether or not the given serial number is valid for this factory.
    /// </summary>
    /// <returns><c>true</c> if this instance is serial number valid the specified serialNumber; otherwise, <c>false</c>.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public abstract bool IsSerialNumberValid(ISerialNumber serialNumber);

    /// <summary>
    /// Creates a new device from the given serial number.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    public abstract IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol);
  } // End DeviceFactory

  /// <summary>
  /// Creates a new P300 device.
  /// </summary>
  internal class P300DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EDeviceModel.P300 == ((GaugeSerialNumber)serialNumber).deviceModel;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var device = new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol);

      var sensor = new GaugeDeviceSensor(device, 0, ESensorType.Pressure, true);
      sensor.maxMeasurement = Units.Pressure.PSIG.OfScalar(300);
      device.sensors = new GaugeDeviceSensor[] { sensor };

      return device;
    }
  } // End P300DeviceFactory

  /// <summary>
  /// Creates a new P300 device.
  /// </summary>
  internal class P500DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EDeviceModel.P500 == ((GaugeSerialNumber)serialNumber).deviceModel;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var device = new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol);

      var sensor = new GaugeDeviceSensor(device, 0, ESensorType.Pressure, true);
      sensor.maxMeasurement = Units.Pressure.PSIG.OfScalar(500);
      device.sensors = new GaugeDeviceSensor[] { sensor };

      return device;
    }
  } // End P500DeviceFactory

  /// <summary>
  /// Creates a new P800 device.
  /// </summary>
  internal class P800DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EDeviceModel.P800 == ((GaugeSerialNumber)serialNumber).deviceModel;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var device = new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol);

      var sensor = new GaugeDeviceSensor(device, 0, ESensorType.Pressure, true);
      sensor.maxMeasurement = Units.Pressure.PSIG.OfScalar(800);
      device.sensors = new GaugeDeviceSensor[] { sensor };

      return device;
    }
  } // End P800DeviceFactory

  /// <summary>
  /// Creates a new PT800 device.
  /// </summary>
  internal class PT800DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EDeviceModel.PT800 == ((GaugeSerialNumber)serialNumber).deviceModel;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var device = new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol);

      var ps = new GaugeDeviceSensor(device, 0, ESensorType.Pressure, true);
      ps.maxMeasurement = Units.Pressure.PSIG.OfScalar(800);

      var ts = new GaugeDeviceSensor(device, 0, ESensorType.Temperature, false);
      ps.maxMeasurement = Units.Temperature.CELSIUS.OfScalar(150);
      ps.minMeasurement = Units.Temperature.CELSIUS.OfScalar(-40);

      device.sensors = new GaugeDeviceSensor[] { ps, ts };

      return device;
    }
  } // End PT800DeviceFactory

  /// <summary>
  /// Creates a new AV760 device.
  /// </summary>
  internal class AV760DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EDeviceModel.AV760 == ((GaugeSerialNumber)serialNumber).deviceModel;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      var device = new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol);

      var sensor = new GaugeDeviceSensor(device, 0, ESensorType.Vacuum, true);
      sensor.maxMeasurement = Units.Pressure.MICRON.OfScalar(760000);
      device.sensors = new GaugeDeviceSensor[] { sensor };

      return device;
    }
  } // End AV760DeviceFactory
}

