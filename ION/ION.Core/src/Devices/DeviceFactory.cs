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
      return serialNumber is GaugeSerialNumber && EGaugeType.P300 == ((GaugeSerialNumber)serialNumber).gaugeType;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      Log.D(this, "Creating P300...");
      Sensor[] sensors = new Sensor[] { new Sensor(ESensorType.Pressure, true, Units.Pressure.PASCAL.OfScalar(0)) };
      Log.D(this, "Created sensors...");

      return new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol, sensors);
    }
  } // End P300DeviceFactory

  /// <summary>
  /// Creates a new P300 device.
  /// </summary>
  internal class P500DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EGaugeType.P500 == ((GaugeSerialNumber)serialNumber).gaugeType;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      Sensor[] sensors = new Sensor[] { new Sensor(ESensorType.Pressure, true, Units.Pressure.PASCAL.OfScalar(0)) };

      return new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol, sensors);
    }
  } // End P500DeviceFactory

  /// <summary>
  /// Creates a new P800 device.
  /// </summary>
  internal class P800DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EGaugeType.P800 == ((GaugeSerialNumber)serialNumber).gaugeType;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      Sensor[] sensors = new Sensor[] { new Sensor(ESensorType.Pressure, true, Units.Pressure.PASCAL.OfScalar(0)) };

      return new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol, sensors);
    }
  } // End P800DeviceFactory

  /// <summary>
  /// Creates a new AV760 device.
  /// </summary>
  internal class AV760DeviceFactory : DeviceFactory {
    // Overridden from IDeviceFactory
    public override bool IsSerialNumberValid(ISerialNumber serialNumber) {
      return serialNumber is GaugeSerialNumber && EGaugeType.AV760 == ((GaugeSerialNumber)serialNumber).gaugeType;
    }

    // Overridden from IDeviceFactory
    public override IDevice Create(IDeviceManager deviceManager, ISerialNumber serialNumber, IConnection connection, IProtocol protocol) {
      Sensor[] sensors = new Sensor[] { new Sensor(ESensorType.Pressure, true, Units.Pressure.PASCAL.OfScalar(0)) };

      return new GaugeDevice(deviceManager, (GaugeSerialNumber)serialNumber, connection, (IGaugeProtocol)protocol, sensors);
    }
  } // End AV760DeviceFactory
}

