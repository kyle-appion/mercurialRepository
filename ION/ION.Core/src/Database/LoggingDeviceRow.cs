namespace ION.Core.Database {

	using SQLite.Net.Attributes;

	[Preserve (AllMembers = true)]
  public class LoggingDeviceRow : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int LDID { get; set; }
    [Ignore]
    public int _id {get { return LDID;} set { LDID = value;}}

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
    /// Gets or sets the date of the NIST certification for the device
    /// </summary>
    /// <value>The nist date.</value>
    public string nistDate { get; set;}

    public override bool Equals(object obj) {
      var dr = obj as LoggingDeviceRow;
      return dr != null && LDID == dr.LDID;
    }

    // Overridden from Object
    public override string ToString() {
      return "Device {" +
        "id: " + LDID +
        ", serialNumber: " + serialNumber +
        ", name: " + name + 
        ", nistDate:" + nistDate + 
        "}";
    }
  }
}

