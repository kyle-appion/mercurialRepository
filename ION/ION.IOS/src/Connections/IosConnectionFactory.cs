namespace ION.IOS.Connections {

  using System;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.Internal;

  using ION.IOS.Connections;

  public class IosConnectionFactory : IConnectionFactory {
    /// <summary>
    /// The manager that will manager the bluetooth for the factory.
    /// </summary>
		public LeConnectionHelper connectionHelper;

		public IosConnectionFactory(LeConnectionHelper connectionHelper) {
			this.connectionHelper = connectionHelper;
    }

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    /// <param name="address">Address.</param>
    /// <param name="protocolVersion">Protocol version.</param>
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      var peripheral = connectionHelper.centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
      if (peripheral == null) {
        throw new Exception("Cannot create connection to " + address + ": the address is not valid");
      }

      if (!SerialNumberExtensions.IsValidSerialNumber(peripheral.Name)) {
        throw new Exception("Cannot create connection: peripheral does not have a valid serial number.");
      }

			var ion = ION.Core.App.AppState.context;
      var serialNumber = SerialNumberExtensions.ParseSerialNumber(peripheral.Name);

			if (serialNumber.rawSerial.StartsWith("S")) {
				var h = (LeConnectionHelper)ion.deviceManager.connectionHelper;
				return new IosRigadoConnection(h.centralManager, peripheral);
			}

      if (serialNumber is BluefruitSerialNumber) {
        return new BluefruitLeConnection(connectionHelper, peripheral);
      } else {
				return new IosLeConnection(ion.deviceManager.connectionHelper as LeConnectionHelper, peripheral);
      }
    }
  }
}

