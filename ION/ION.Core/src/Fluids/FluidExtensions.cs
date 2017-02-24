namespace ION.Core.Fluids {

	using System;

	public static class FluidExtensions {

		/// <summary>
		/// Queries the ARGB color of the fluids safety.
		/// </summary>
		/// <param name="safety">Safety.</param>
		public static int Color(this Fluid.ESafety safety) {
			switch (safety) {
				case Fluid.ESafety.A1:
					return unchecked((int)0xff00ff00);
				case Fluid.ESafety.A2:
					return unchecked((int)0xfffdd351);
				case Fluid.ESafety.A2L:
					return unchecked((int)0xfffdd351);
				case Fluid.ESafety.A3:
					return unchecked((int)0xffff0000);
				case Fluid.ESafety.B1:
					return unchecked((int)0xff00ff00);
				case Fluid.ESafety.B2:
					return unchecked((int)0xfffdd351);
				case Fluid.ESafety.Unknown:
					// Fallthrough
				default:
					return 0x00000000;
			}
		}
	}
}
