namespace ION.Droid.Connections {
	/// <summary>
	/// A simple delegate that wraps the various ways that android wants people to do scanning operations.
	/// </summary>
	internal interface IScanDelegate {
		/// <summary>
		/// Whether or not the scan delegate is currently scanning.
		/// </summary>
		/// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
		bool isScanning { get; }

		/// <summary>
		/// Start the scanner.
		/// </summary>
		/// <returns><c>true</c>, if scan was started, <c>false</c> otherwise.</returns>
		bool StartScan();
		/// <summary>
		/// Stops the scanner.
		/// </summary>
		void StopScan();
	}
}

