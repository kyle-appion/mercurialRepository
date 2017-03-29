namespace ION.Core.Devices {
  
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

	using Appion.Commons.Util;

  using ION.Core.Database;
  using ION.Core.Devices.Protocols;

  /// <summary>
  /// Utility extensions that will assist in pulling specifically devices from the database.
  /// </summary>
  public static class DeviceDatabaseExtensions {
    /// <summary>
    /// Queries the database for all the devices.
    /// </summary>
    /// <returns>The for all devices async.</returns>
    public static async Task<List<IDevice>> QueryForAllDevicesAsync(this IONDatabase db) {
      var ret = new List<IDevice>();

	      var devices = await db.QueryForAllAsync<DeviceRow>();
	      Log.D("DeviceDatabaseExtensions", "got all devices");
	      foreach (var d in devices) {
	        ret.Add(await db.ReconstructDevice(d));
	      }

      return ret;
    }    

    /// <summary>
    /// Queries for the device with the given serial number.
    /// </summary>
    /// <returns>The for using serial number async.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public static Task<DeviceRow> QueryForUsingSerialNumberAsync(this IONDatabase db, ISerialNumber serialNumber) {
      Log.D("DeviceDatabaseExtensions", "QueryFor: " + serialNumber);   
      try {
        var serial = serialNumber.ToString();
        if (db.Table<DeviceRow>().Count() > 0) {
          return Task.FromResult(db.Table<DeviceRow>().Where(x => x.serialNumber == serial).First());
        } else {
          return Task.FromResult(default(DeviceRow));
        }
      } catch (Exception e) {
        Log.E(typeof(DeviceDatabaseExtensions).Name, "Failed to query for using serial number.", e);
        return Task.FromResult(default(DeviceRow));
      }
    }

    /// <summary>
    /// Deconstructs the application's standard device into an ORM friendly object.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="device">Device.</param>
    public static async Task<DeviceRow> DeconstructDevice(this IONDatabase db, IDevice device) {
      var ret = await db.QueryForUsingSerialNumberAsync(device.serialNumber);

      if (ret == null) {
        ret = new DeviceRow() {
          serialNumber = device.serialNumber.ToString(),
          protocol = (int)device.protocol.version,
          connectionAddress = device.connection.address,
        };
      }

      ret.name = device.name;
      ret.lastConnected = device.connection.lastSeen;

      return ret;
    }

    /// <summary>
    /// Reconstruct the database device into an app friendly device object.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="db">Db.</param>
    /// <param name="device">Device.</param>
    public static Task<IDevice> ReconstructDevice(this IONDatabase db, DeviceRow device) {
      if (!device.serialNumber.IsValidSerialNumber()) {
        throw new Exception("Failed to parse " + device.serialNumber + " into a serial number");
      }

      var serialNumber = device.serialNumber.ParseSerialNumber();
			var ret = db.ion.deviceManager.CreateDevice(serialNumber, device.connectionAddress, (EProtocolVersion)device.protocol);
			ret.name = device.name;
      return Task.FromResult(ret);
    }
  }
}

