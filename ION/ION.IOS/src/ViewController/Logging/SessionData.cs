using System;

namespace ION.IOS.ViewController.Logging {
  public class SessionData {

    public int SID;
    public DateTime start;
    public DateTime finish;

    public SessionData(int session, DateTime begin, DateTime end) {
      SID = session;
      start = begin;
      finish = end;
    }

  }
}

