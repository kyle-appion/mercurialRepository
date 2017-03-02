namespace ION.Core.Devices.Connections {

	using System;

	using ION.Core.Connections;
	using ION.Core.Devices.Protocols;

	public class RemoteConnectionFactory : IConnectionFactory {
		public RemoteConnectionFactory() {
		}

		// Implemented from IConnectionFactory
		public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
			return new RemoteConnection(address);
		}
	}
}
