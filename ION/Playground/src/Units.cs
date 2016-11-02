namespace Playground {

	using System;

	public class Units {
		public static class Dimensionless {
			public static readonly Unit<Quantity.Dimensionless> NONE = new Unit<Quantity.Dimensionless>();
		}

		public static class Temperature {
			public static readonly Unit<Quantity.Temperature> KELVIN = new Unit<Quantity.Temperature>() { kelvin = 1, absolute = true };
		}
	}
}
