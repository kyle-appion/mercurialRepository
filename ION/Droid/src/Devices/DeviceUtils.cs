using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.Droid.Devices {
  /// <summary>
  /// A util class that will provide some localized data for devices.
  /// </summary>
  public class DeviceUtils {

    /// <summary>
    /// Queries the Android drawable resource id for the given device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public static int GetDeviceIcon(IDevice device) {
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
    /// Queries the Android drawable resource icon for the given gauge type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetGaugeIcon(EDeviceModel type) {
      switch (type) {
        case EDeviceModel.P300: {
          return Resource.Drawable.ic_render_gaugep300;
        }
        case EDeviceModel.P500: {
          return Resource.Drawable.ic_render_gaugep500;
        }
        case EDeviceModel.P800: {
          return Resource.Drawable.ic_render_gaugep800;
        }
        case EDeviceModel.AV760: {
          return Resource.Drawable.ic_render_gaugeav760;
        }
        default: {
          return Resource.Drawable.ic_logo_appiondefault;
        }
      }
    }

    /// <summary>
    /// Queries the Android string resource name for the given device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public static int GetDeviceProductName(IDevice device) {
      switch (device.type) {
        case EDeviceType.Gauge: {
          GaugeSerialNumber serialNumber = (GaugeSerialNumber)device.serialNumber;
          return GetGaugeProductName(serialNumber.deviceModel);
        }
        default: {
          return Resource.String.device_unknown;
        }
      }
    }

    /// <summary>
    /// Queries the Android string resource name for the given gauge.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetGaugeProductName(EDeviceModel type) {
      switch (type) {
        case EDeviceModel.P300: {
          return Resource.String.device_p300;
        }
        case EDeviceModel.P500: {
          return Resource.String.device_p500;
        }
        case EDeviceModel.P800: {
          return Resource.String.device_p800;
        }
        case EDeviceModel.AV760: {
          return Resource.String.device_av760;
        }
        default: {
          return Resource.String.device_unknown;
        }
      }
    }

    /// <summary>
    /// Queries the user friendly name of the sensor type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetSensorTypeName(ESensorType type) {
      switch (type) {
        case ESensorType.Humidity: {
          return Resource.String.measurement_humidity;
        }
        case ESensorType.Pressure: {
          return Resource.String.measurement_pressure;
        }
        case ESensorType.Temperature: {
          return Resource.String.measurement_temperature;
        }
        case ESensorType.Vacuum: {
          return Resource.String.measurement_vacuum;
        }
        default: {
          return Resource.String.measurement_unknown;
        }
      }
    }
  }
}