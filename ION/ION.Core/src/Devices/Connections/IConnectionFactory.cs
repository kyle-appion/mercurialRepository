namespace ION.Core.Devices.Connections {
  
  using System;

  using ION.Core.Connections;
  using ION.Core.Devices.Protocols;

  public interface IConnectionFactory {
    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    IConnection CreateConnection(string address, EProtocolVersion protocolVersion);
  }
}

