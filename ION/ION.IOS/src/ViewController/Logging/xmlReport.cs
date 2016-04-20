using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  
  public struct sessionInfo {
    public string deviceSN;
    public int SID;
    public int JID;
  }

  public class xmlReport {

    public DateTime startDate;
    public DateTime endDate;
    public List<sessionInfo> Sensors;

    public xmlReport(DateTime begin, DateTime finish, List<deviceReadings> pressureTemperatures) {
      startDate = begin;
      endDate = finish;
      Sensors = new List<sessionInfo>();
      foreach (var item in pressureTemperatures) {
        var entry = new sessionInfo();
        entry.deviceSN = item.name;
        entry.SID = item.SID;
        if (item.frnJID != null) {
          entry.JID = item.frnJID;        
        }
        Sensors.Add(entry);
      }
    }
  }
}

