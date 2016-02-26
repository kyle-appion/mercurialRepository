using System;
using System.IO;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
namespace ION.Core.Database {
  public class SessionMeasurement {

    [PrimaryKey, AutoIncrement]
    public int MID { get; set; }
    public int frnSID { get; set; }
    public string deviceSN { get; set;}
    public string deviceMeasurement { get; set;}
    public DateTime measurementDate { get; set;}

    public SessionMeasurement() {
      
    }
  }
}

