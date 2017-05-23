namespace TestBench.Droid {

	using System;

	using Android.Bluetooth;

	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;

	public delegate void OnNewPacket(IConnection connection, GaugePacket packet);
	public interface IConnection {
		event OnNewPacket onNewPacket;
		event Action<IConnection> onConnectionStateChanged;

		bool isConnected { get; }
		ProfileState state { get; }
		ISerialNumber serialNumber { get; }
		GaugePacket lastPacket { get; }

		void Connect();
		void Disconnect();

		/// <summary>
		/// Call this when the connection should receive a packet. This can be either a characteristic or advertisement
		/// packet.
		/// </summary>
		/// <param name="acket">Acket.</param>
		void ReceivePacket(byte[] packet);
	}
}
