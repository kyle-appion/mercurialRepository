namespace ION.IOS.Devices {

  using System;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;

  using ION.IOS.Connections;

  public class IosConnectionFactory : IConnectionFactory {
    /// <summary>
    /// The manager that will manager the bluetooth for the factory.
    /// </summary>
    public CBCentralManager centralManager;

    public IosConnectionFactory(CBCentralManager centralManager) {
      this.centralManager = centralManager;
    }

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    /// <param name="address">Address.</param>
    /// <param name="protocolVersion">Protocol version.</param>
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new NSUuid(address))[0];
      if (peripheral == null) {
        throw new Exception("Cannot create connection to " + address + ": the address is not valid");
      }

      return new IosLeConnection(centralManager, peripheral);
    }
  }
}

