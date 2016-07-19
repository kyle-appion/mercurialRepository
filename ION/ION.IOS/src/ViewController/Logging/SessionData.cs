using System;
using System.Collections.Generic;

namespace ION.IOS.ViewController.Logging {
  public class SessionData {

    public int SID;
    public int frnJID;
    public DateTime start;
    public DateTime finish;
    public List<MeasurementData> sessionMeasurements;

    public SessionData(int session, DateTime begin, DateTime end, int JID = 0) {
      SID = session;
      frnJID = JID;
      start = begin;
      finish = end;
      sessionMeasurements = new List<MeasurementData>();
    }

  }
}

