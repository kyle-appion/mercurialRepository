using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;

using ION.Core.Devices;
using ION.Core.Util;

namespace ION.Core.Database {
  public class DeviceDao : IDao<IDevice> {

    //database.BeginTransaction();

    //database.Commit();

    /// <summary>
    /// The database that is backing this dao.
    /// </summary>
    /// <value>The database.</value>
    private IONDatabase database { get; set; }

    public DeviceDao(IONDatabase database) {
      this.database = database;
      database.CreateTable<Device>();
      //Log.D(this, "Created table with result " + );
    }

    // Overridden from IDao
    public async Task<IDevice> QueryForAsync(long primaryKey) {
      Device device = database.Table<Device>().Where(x => x.id == primaryKey).First();
      return await InflateAsync(device);
    }

    // Overridden from IDao
    public async Task<List<IDevice>> QueryForAllAsync() {
      var ret = new List<IDevice>();

      foreach (Device device in database.Table<Device>().AsEnumerable()) {
        var d = await InflateAsync(device);
        if (d != null) {
          ret.Add(d);
        }
      }

      return ret;
    }

    // Overridden from IDao
    public async Task<long> CountAsync() {
      return (long)database.Table<Device>().Count();
    }

    // Overridden from IDao
    public async Task<bool> SaveAsync(IDevice item) {
      Log.D(this, "Saving...");
      var device = await DeconstructAsync(item);
      database.BeginTransaction();
      int affected = 0;
      if (device.id == -1) {
        affected = database.Insert(device);
      } else {
        affected = database.Update(device);
      }
      database.Commit();
      Log.D(this, "affected " + affected + " rows");
      return affected > 0;
    }

    // Overridden from IDao
    public async Task<bool> DeleteAsync(IDevice item) {
      var device = await DeconstructAsync(item);
      database.BeginTransaction();
      var affected = database.Delete(device.id);
      database.Commit();
      Log.D(this, "Deleted " + affected + " device when deleteing " + item.serialNumber);
      return affected > 0;
    }

    /// <summary>
    /// Queries the device in the database with the given serial number.
    /// </summary>
    /// <returns>The database id of the device with the given serial number
    /// or null if the serial number is not present within the database.</returns>
    /// <param name="serialNumber">Serial number.</param>
    private async Task<Device> QueryForUsingSerialNumberAsync(ISerialNumber serialNumber) {
      try {
        var serial = serialNumber.ToString();
        return database.Table<Device>().Where(x => x.serialNumber == serial).First();
      } catch (Exception e) {
        Log.E(this, "Failed to query for serial number async", e);
        return null;
      }
    }

    /// <summary>
    /// Inflates the device into a fully realized IDevice.
    /// </summary>
    /// <returns>The I device.</returns>
    /// <param name="device">Device.</param>
    private async Task<IDevice> InflateAsync(Device device) {
      Log.D(this, "Inflated: " + device.ToString());
      try {
        var serialNumber = GaugeSerialNumber.Parse(device.serialNumber);
        return database.ion.deviceManager.CreateDevice(serialNumber, device.connectionAddress, device.protocol);
      } catch (Exception e) {
        Log.E(this, "Cannot inflate device", e);
        return null;
      }
    }

    /// <summary>
    /// Deconstructs the device into the sql friendly ORM object.
    /// </summary>
    /// <param name="device">Device.</param>
    private async Task<Device> DeconstructAsync(IDevice device) {
      Device ret = await QueryForUsingSerialNumberAsync(device.serialNumber);

      if (ret == null) {
        ret = new Device();
        ret.serialNumber = device.serialNumber.ToString();
        ret.protocol = device.protocol.version;
        ret.connectionAddress = device.connection.address;
      }

      ret.name = device.name;
      ret.lastConnected = device.connection.lastSeen;

      Log.D(this, "Deconstructed: " + ret);

      return ret;
    }
  }

  /// <summary>
  /// A wrapper object that will hold the content of an IDevice.
  /// </summary>
  internal class Device { 
    [PrimaryKey, AutoIncrement]
    public long id { get; set; }
    [Indexed]
    public string serialNumber { get; set; }
    public string name { get; set; }
    public DateTime lastConnected { get; set; }
    public string connectionAddress { get; set; }
    public int protocol { get; set; }

    public Device() {
      id = -1;
    }

    // Overridden from Object
    public override string ToString() {
      return "Device {" +
        "id: " + id +
        ", serialNumber: " + serialNumber +
        ", name: " + name +
        ", lastConnected: " + lastConnected.ToUTCMilliseconds() + 
        ", connectionAddress: " + connectionAddress + 
        ", protocol " + protocol +
        "}";
    }
  }
}

