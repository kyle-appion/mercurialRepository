namespace ION.Droid.Devices {

  using System;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.App;


  /// <summary>
  /// A util class that will provide some localized data for devices.
  /// </summary>
  public static class DeviceExtensions {
    /// <summary>
    /// Queries the Android drawable resource id for the given device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public static int GetDeviceIcon(this IDevice device) {
      switch (device.type) {
        case EDeviceType.Gauge: {
          GaugeSerialNumber serialNumber = (GaugeSerialNumber)device.serialNumber;
          return GetGaugeIcon(serialNumber.deviceModel);
        }
        default: {
          return Resource.Drawable.ic_logo_appiondefault;
        }
      }
    }

    /// <summary>
    /// Queries the vertical icon used to represent that device's current battery level.
    /// </summary>
    /// <returns>The battery icon vert.</returns>
    /// <param name="device">Device.</param>
    public static int GetBatteryIconVert(this IDevice device) {
      if (device.battery > 75) {
        return Resource.Drawable.ic_battery_vert_100;
			} else if (device.battery > 50) {
				return Resource.Drawable.ic_battery_vert_75;
			} else if (device.battery > 25) {
				return Resource.Drawable.ic_battery_vert_50;
			} else if (device.battery > 5) {
				return Resource.Drawable.ic_battery_vert_25;
      } else {
        return Resource.Drawable.ic_battery_vert_empty;
			}
    }

		/// <summary>
		/// Queries the horizontal icon used to represent that device's current battery level.
		/// </summary>
		/// <returns>The battery icon vert.</returns>
		/// <param name="device">Device.</param>
		public static int GetBatteryIconHoriz(this IDevice device) {
			if (device.battery > 75) {
				return Resource.Drawable.ic_battery_horiz_100;
			} else if (device.battery > 50) {
				return Resource.Drawable.ic_battery_horiz_75;
			} else if (device.battery > 25) {
				return Resource.Drawable.ic_battery_horiz_50;
			} else if (device.battery > 5) {
				return Resource.Drawable.ic_battery_horiz_25;
			} else {
				return Resource.Drawable.ic_battery_horiz_empty;
			}
		}

    /// <summary>
    /// Queries the Android drawable resource icon for the given gauge type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetGaugeIcon(EDeviceModel type) {
      switch (type) {
        case EDeviceModel.P300:
          return Resource.Drawable.ic_render_gaugep300;
        case EDeviceModel.P500:
          return Resource.Drawable.ic_render_gaugep500;
        case EDeviceModel.P800:
          return Resource.Drawable.ic_render_gaugep800;
        case EDeviceModel.AV760:
          return Resource.Drawable.ic_render_gaugeav760;
        case EDeviceModel.PT500:
          return Resource.Drawable.ic_render_gaugept500;
        case EDeviceModel.PT800:
          return Resource.Drawable.ic_render_gaugept800;
        default:
          return Resource.Drawable.ic_logo_appiondefault;
      }
    }

    /// <summary>
    /// Queries the Android string resource name for the given device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public static string GetDeviceProductName(this IDevice device) {
      var ion = (BaseAndroidION)AppState.context;
      switch (device.type) {
        case EDeviceType.Gauge:
          GaugeSerialNumber serialNumber = (GaugeSerialNumber)device.serialNumber;
          return GetTypeString(serialNumber.deviceModel);
        default:
          return ion.context.GetString(Resource.String.device_unknown);
      }
    }

    /// <summary>
    /// Queries the Android string resource name for the given gauge.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetTypeString(this EDeviceModel type) {
			var ion = (BaseAndroidION)AppState.context;
      switch (type) {
        case EDeviceModel.P300:
          return ion.context.GetString(Resource.String.device_p300);
        case EDeviceModel.P500:
          return ion.context.GetString(Resource.String.device_p500);
        case EDeviceModel.P800:
          return ion.context.GetString(Resource.String.device_p800);
        case EDeviceModel.AV760:
          return ion.context.GetString(Resource.String.device_av760);
        case EDeviceModel.PT300:
          return ion.context.GetString(Resource.String.device_pt300);
        case EDeviceModel.PT500:
          return ion.context.GetString(Resource.String.device_pt500);
        case EDeviceModel.PT800:
          return ion.context.GetString(Resource.String.device_pt800);
				case EDeviceModel.WL:
					return ion.context.GetString(Resource.String.device_wllibra);
        default:
          return ion.context.GetString(Resource.String.device_unknown);
      }
    }

    /// <summary>
    /// Queries the user friendly name of the sensor type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetSensorTypeName(this ESensorType type) {
			var ion = (BaseAndroidION)AppState.context;
      switch (type) {
        case ESensorType.Humidity:
          return ion.context.GetString(Resource.String.humidity);
        case ESensorType.Pressure:
          return ion.context.GetString(Resource.String.pressure);
        case ESensorType.Temperature:
          return ion.context.GetString(Resource.String.temperature);
        case ESensorType.Vacuum:
          return ion.context.GetString(Resource.String.vacuum);
				case ESensorType.Weight:
					return ion.context.GetString(Resource.String.weight);
        default:
          return ion.context.GetString(Resource.String.unknown);
      }
    }
  }
}