namespace ION.Core.Connections {

  using ION.Core.Devices.Protocols;

	public class RemoteConnectionManager : IConnectionManager {

		// Implemented from IConnectionHelper
		public event OnScanStateChanged onScanStateChanged;
		// Implemented from IConnectionHelper
		public event OnDeviceFound onDeviceFound;

		// Implemented from IConnectionHelper
		public bool isEnabled { get { return true; } }

		// Implemented from IConnectionHelper
		public bool isScanning { get { return false; } }
    // Implemented for IConnectionManager
    public bool isBroadcastScanning { get { return false; } }


		public RemoteConnectionManager() {
		}

		// Implemented from IConnectionHelper
		public void Dispose() {
		}

    // Implemented for IConnectionHelper
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      return null;
    }


		// Implemented from IConnectionHelper
		public bool StartScan() {
			return false;
		}

    // Implemented for IConnectionManager
    public bool StartBroadcastScan() {
      return false;
    }

		// Implemented from IConnectionHelper
		public void StopScan() {
		}
	}
}
