namespace ION.Core.Database {

  using System;

  using SQLite.Net.Attributes;

  using ION.Core.Util;

  public class DeviceRow : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    /// <value>The serial number.</value>
    [Indexed]
    public string serialNumber { get; set; }
    /// <summary>
    /// Gets or sets the user defined name of the device.
    /// </summary>
    /// <value>The name.</value>
    public string name { get; set; }
    /// <summary>
    /// Gets or sets the last date the device was connected.
    /// </summary>
    /// <value>The last connected.</value>
    public DateTime lastConnected { get; set; }
    /// <summary>
    /// Gets or sets the connection address.
    /// </summary>
    /// <value>The connection address.</value>
    public string connectionAddress { get; set; }
    /// <summary>
    /// Gets or sets the protocol version.
    /// </summary>
    /// <value>The protocol.</value>
    public int protocol { get; set; }

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

