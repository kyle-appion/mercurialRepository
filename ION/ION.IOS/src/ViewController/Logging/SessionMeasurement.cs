using System;
using System.IO;
using SQLite;

namespace ION.IOS {
  public class SessionMeasurement {

    [PrimaryKey, AutoIncrement]
    public int MID { get; set; }
    public int frnSID { get; set; }
    public string deviceSN { get; set;}
    public string deviceMeasurement { get; set;}

    public SessionMeasurement() {
    }
  }
}

