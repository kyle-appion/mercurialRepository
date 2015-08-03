using System;

using SQLite.Net;
using SQLite.Net.Interop;

using ION.Core.App;
using ION.Core.Devices;

namespace ION.Core.Database {
  public class IONDatabase : SQLiteConnection {

    /// <summary>
    /// The ion context that this database is running within.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }
    /// <summary>
    /// The dao that is used to query devices.
    /// </summary>
    /// <value>The device DAO.</value>
    public IDao<IDevice> deviceDao { get; private set; }
    

    public IONDatabase(ISQLitePlatform platform, string path, IION ion) : base(platform, path)  {
      this.ion = ion;
      // Create the database
      deviceDao = new DeviceDao(this);
    }
  }
}

