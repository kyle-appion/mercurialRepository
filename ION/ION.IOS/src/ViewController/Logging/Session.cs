using System;
using System.IO;
using SQLite;

namespace ION.IOS {


  public class Session {
    
    [PrimaryKey, AutoIncrement]
    public int SID { get; set;}
    public int frnJID{ get; set; }
    public DateTime sessionStart { get; set;}
    public DateTime sessionEnd { get; set;}

    public Session() {
    }
  }
}

