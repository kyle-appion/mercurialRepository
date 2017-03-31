namespace ION.Droid.Connections {

	using System;

	public abstract class BaseScanHelper : Java.Lang.Object, IScanDelegate {
		// Implemented from IScanHelper
		public bool isScanning { get; private set; }

		// Implemented from IScanHelper
		public bool StartScan() {
			if (isScanning) {
				return false;
			}

			if (DoStartScan()) {
				isScanning = true;
				return true;
			} else {
				return false;
			}
		}

		// Imlemented from IScanHelper
		public void StopScan() {
			if (isScanning) {
				DoStopScan();
			}

			isScanning = false;
		}


		/// <summary>
		/// Implementation specific scan start procedure. Returns true if the scan started successfully, false otherwise.
		/// </summary>
		protected abstract bool DoStartScan();

		/// <summary>
		/// Implementation specific scan stop procedure.
		/// </summary>
		protected abstract void DoStopScan();
	}
}
