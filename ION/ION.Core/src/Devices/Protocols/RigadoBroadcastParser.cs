namespace ION.Core.Devices.Protocols {

	using Appion.Commons.Util;

	using ION.Core.App;

	/// <summary>
	/// A simple class that is used to parse advertisement packets for BLE.
	/// </summary>
	public class RigadoBroadcastParser {
		/// <summary>
		/// Parses the Rigado gauge broadcast packet.
		/// </summary>
		/// <returns><c>true</c>, if broadcast packet was parsed, <c>false</c> otherwise.</returns>
		/// <param name="source">Source.</param>
		/// <param name="serialNumber">Serial number.</param>
		/// <param name="packet">Packet.</param>
		public static bool ParseBroadcastPacket(byte[] source, out ISerialNumber serialNumber, out byte[] packet) {
			if (source == null || source.Length < 10) {
				serialNumber = null;
				packet = null;
				return false;
			}

			//Log.D(typeof(RigadoBroadcastParser).Name, "Resolving advertising packet for: " + source.ToByteString());

			var rawId = source.Subset(2, 4);
			var manfacId = rawId[0] | rawId[1] << 8;

			if (manfacId == AppionConstants.MANFAC_ID) {
				var rawSerialNumber = source.Subset(4, 12);
				var sn = System.Text.Encoding.UTF8.GetString(rawSerialNumber, 0, rawSerialNumber.Length);
				if (sn.IsValidSerialNumber()) {
					serialNumber = sn.ParseSerialNumber();
					packet = source.Subset(12);
					return true;
				} else {
					serialNumber = null;
					packet = null;
					return false;
				}
			} else {
				serialNumber = null;
				packet = null;
				return false;
			}
		}
	}
}
