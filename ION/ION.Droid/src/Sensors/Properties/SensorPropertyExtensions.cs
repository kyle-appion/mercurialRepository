namespace ION.Droid.Sensors.Properties {

  using System;

  using Android.Content;

  using ION.Core.Sensors.Properties;

  public static class SensorPropertyExtensions {

    /// <summary>
    /// Queries the localized string for the given sensor property.
    /// </summary>
    /// <returns>The localized string.</returns>
    /// <param name="sensorPropety">Sensor propety.</param>
    /// <param name="context">Context.</param>
    public static string GetLocalizedString(this ISensorProperty sensorProperty, Context context) {
      if (sensorProperty is AlternateUnitSensorProperty) {
        return context.GetString(Resource.String.workbench_alt);
      } else if (sensorProperty is HoldSensorProperty) {
        return context.GetString(Resource.String.workbench_hold);
      } else if (sensorProperty is MinSensorProperty) {
        return context.GetString(Resource.String.workbench_min);
      } else if (sensorProperty is MaxSensorProperty) {
        return context.GetString(Resource.String.workbench_max);
      } else if (sensorProperty is PTChartSensorProperty) {
        return context.GetString(Resource.String.workbench_ptchart);
      } else if (sensorProperty is RateOfChangeSensorProperty) {
        return context.GetString(Resource.String.workbench_roc);
      } else if (sensorProperty is SuperheatSubcoolSensorProperty) {
        return context.GetString(Resource.String.workbench_shsc);
      } else {
        return context.GetString(Resource.String.na);
      }
    }

    /// <summary>
    /// Queries the localizes string abreviation for the given sensor property.
    /// </summary>
    /// <returns>The localized string abreviation.</returns>
    /// <param name="sensorProperty">Sensor property.</param>
    /// <param name="context">Context.</param>
    public static string GetLocalizedStringAbreviation(this ISensorProperty sensorProperty, Context context) {
      if (sensorProperty is AlternateUnitSensorProperty) {
        return context.GetString(Resource.String.workbench_alt_abrv);
      } else if (sensorProperty is HoldSensorProperty) {
        return context.GetString(Resource.String.workbench_hold_abrv);
      } else if (sensorProperty is MinSensorProperty) {
        return context.GetString(Resource.String.workbench_min_abrv);
      } else if (sensorProperty is MaxSensorProperty) {
        return context.GetString(Resource.String.workbench_max_abrv);
      } else if (sensorProperty is PTChartSensorProperty) {
        return context.GetString(Resource.String.fluid_pt_abrv);
      } else if (sensorProperty is RateOfChangeSensorProperty) {
        return context.GetString(Resource.String.workbench_roc_abrv);
      } else if (sensorProperty is SuperheatSubcoolSensorProperty) {
        return context.GetString(Resource.String.workbench_shsc_abrv);
      } else if (sensorProperty is TimerSensorProperty) {
        return context.GetString(Resource.String.workbench_timer_abrv);
      } else {
        return context.GetString(Resource.String.na);
      }
    }
  }
}

