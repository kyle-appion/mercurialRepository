namespace TestBench.Droid {

	using System;

	public enum ERigType {
		Vacuum,
		Pressure,
		Unknown,
	}

	public interface IRig {
		event Action<IRig> onConnectionStateChanged;

		ERigType rigType { get; }
		bool isConnected { get; }
		string address { get; }

		void Connect();
		void Disconnect();
	}
}