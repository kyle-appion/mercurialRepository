namespace TestBench.Droid {

	using ION.Core.Devices;

	public static class RigExtensions {
		public const string RIG_VACUUM = "ION Test Rig";
		public const string RIG_PRESSURE = "ION Pressure Rig";

		/// <summary>
		/// Queries whether or not the given name is a valid rig identifier.
		/// </summary>
		/// <returns><c>true</c>, if valid rig identifier was ised, <c>false</c> otherwise.</returns>
		/// <param name="identifier">Name.</param>
		public static bool IsValidRigIdentifier(this string identifier) {
			ERigType type;
			return identifier.TryGetRigType(out type);
		}

		/// <summary>
		/// Attempts to get the rig type from the identifier. Type is set to uknown is the identifier is not valid.
		/// </summary>
		/// <returns><c>true</c>, if get rig type was tryed, <c>false</c> otherwise.</returns>
		/// <param name="identifier">Identifier.</param>
		/// <param name="type">Type.</param>
		public static bool TryGetRigType(this string identifier, out ERigType type) {
			if (RIG_VACUUM.Equals(identifier)) {
				type = ERigType.Vacuum;
				return true;
			} else if (RIG_PRESSURE.Equals(identifier)) {
				type = ERigType.Pressure;
				return true;
			} else {
				type = ERigType.Unknown;
				return false;
			}
		}

		public static ERigType AsRigType(this EDeviceModel deviceModel) {
			switch (deviceModel) {
				case EDeviceModel.AV760:
					return ERigType.Vacuum;
				case EDeviceModel.P300:
				case EDeviceModel.P500:
				case EDeviceModel.P800:
				case EDeviceModel.PT300:
				case EDeviceModel.PT500:
				case EDeviceModel.PT800:
					return ERigType.Pressure;
				default:
					return ERigType.Unknown;
			}
		}
	}
}
