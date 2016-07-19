using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace ION.IOS.ViewController.Logging {
  
  public class MeasurementData {
    public int MID;
    public int frnSID;
    public string deviceSN;
    public string deviceMeasurement;

    public MeasurementData(int ID, int sID, string SerialNumber, string measurement) {
      MID = ID;
      frnSID = sID;
      deviceSN = SerialNumber;
      deviceMeasurement = measurement;
    }

  }
}

