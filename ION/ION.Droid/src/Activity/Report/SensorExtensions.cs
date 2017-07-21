namespace ION.Droid.Activity.Report {

	using Android.Content;

	using ION.Core.Sensors;
	
	public static class SensorExtensions {
		/// <summary>
		/// Queries the chart color of the given sensor.
		/// </summary>
		/// <returns>The chart color.</returns>
		/// <param name="sensor">Sensor.</param>
		/// <param name="context">Context.</param>
		public static Android.Graphics.Color GetChartColor(this Sensor sensor, Context context) {
			var color = 0;

			switch (sensor.type) {
				case ESensorType.Pressure:
					color =  Resource.Color.blue;
					break;
				case ESensorType.Temperature:
					color = Resource.Color.red;
					break;
				case ESensorType.Vacuum:
					color = Resource.Color.maroon;
					break;
				default:
					color =	Resource.Color.black;
					break;
			}

			return color.AsResourceColor(context);
		}
	}
}

