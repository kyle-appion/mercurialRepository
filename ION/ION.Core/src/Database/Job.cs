using System;
using System.IO;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
namespace ION.Core.Database {
  public class Job {

    [PrimaryKey, AutoIncrement]
    public int JID { get; set;}
    public string jobName { get; set; }

    public Job() {
    }
  }
}

