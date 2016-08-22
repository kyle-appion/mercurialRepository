namespace ION.Droid.Connections {
	/// <summary>
	/// A simple delegate that wraps the various ways that android wants people to do scanning operations.
	/// </summary>
	internal interface IScanDelegate {
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

