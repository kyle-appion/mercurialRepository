using System;
using System.IO;
using SQLite;

namespace ION.IOS {
  public class Job {

    [PrimaryKey, AutoIncrement]
    public int JID { get; set;}
    public string jobName { get; set; }

    public Job() {
    }
  }
}

