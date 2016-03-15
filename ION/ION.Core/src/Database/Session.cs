using System;
using System.IO;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
namespace ION.Core.Database {

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

