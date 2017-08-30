using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;

using ION.Core.Sensors;

namespace ION.Droid.Activity.Report {
  public static class SensorExtensions {
    /// <summary>
    /// Returns a tuple describing the colors associated with the given sensor. Item1 is the primary color and Item2 is
    /// the secondary (text) color.
    /// </summary>
    /// <returns>The color for sensor.</returns>
    /// <param name="sensor">Sensor.</param>
    public static Tuple<Color, Color> GetColorForSensor(this Sensor sensor, Context context) {
      switch (sensor.type) {
        case ESensorType.Pressure:
          return new Tuple<Color, Color>(Resource.Color.blue.AsResourceColor(context), Resource.Color.white.AsResourceColor(context));
        case ESensorType.Temperature:
          return new Tuple<Color, Color>(Resource.Color.orange.AsResourceColor(context), Resource.Color.black.AsResourceColor(context));
        case ESensorType.Vacuum:
          return new Tuple<Color, Color>(Resource.Color.maroon.AsResourceColor(context), Resource.Color.white.AsResourceColor(context));
        default:
          return new Tuple<Color, Color>(Resource.Color.black.AsResourceColor(context), Resource.Color.white.AsResourceColor(context));
      }
    }
  }
}
