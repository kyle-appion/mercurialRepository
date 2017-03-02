namespace ION.CoreExtensions.Net.Portal {

	using System;

	/// <summary>
	/// A simple class that represents the high level information about a user.
	/// </summary>
	public class ConnectionData {
		public int id { get; set; }
		public string displayName { get; set; }
		public string email { get; set; }
		public bool isUserOnline { get; set; }
	}
}
