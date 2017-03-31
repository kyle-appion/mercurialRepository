namespace ION.Core.Devices.Connections {

	using System;

	public class RemoteConnectionHelper : IConnectionHelper {

		// Implemented from IConnectionHelper
		public event OnScanStateChanged onScanStateChanged;
		// Implemented from IConnectionHelper
		public event OnDeviceFound onDeviceFound;

		// Implemented from IConnectionHelper
		public bool isEnabled { get { return true; } }

		// Implemented from IConnectionHelper
		public bool isScanning { get { return false; } }


		public RemoteConnectionHelper() {
		}

		// Implemented from IConnectionHelper
		public void Dispose() {
		}

		// Implemented from IConnectionHelper
		public bool StartScan(TimeSpan scanTime) {
			return false;
		}

		// Implemented from IConnectionHelper
		public void StopScan() {
		}
	}
}
