namespace ION.Core.Database.Deprecated {
  
  using System;

  using SQLite.Net;
  using SQLite.Net.Attributes;

  /// <summary>
  /// THIS CLASS IS DEPRECATED AND ONLY USED FOR OLDER VERSIONS OF THE APPLICATION.
  /// </summary>
  public class Device {
    [PrimaryKey, AutoIncrement]
    public long id { get; set; }
    [Indexed]
    public string serialNumber { get; set; }
    public string name { get; set; }
    public DateTime lastConnected { get; set; }
    public string connectionAddress { get; set; }
    public int protocol { get; set; }
  }
}

