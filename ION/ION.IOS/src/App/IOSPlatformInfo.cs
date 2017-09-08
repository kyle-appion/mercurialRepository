using System;
using SystemConfiguration;

using UIKit;
using Foundation;

using ION.Core.App;

using ION.IOS.Net;

namespace ION.IOS.App {
  public class IOSPlatformInfo : BasePlatformInfo {  
    public IOSPlatformInfo(IION ion) {
    	UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
      	
      manufacturer = "Apple Inc.";
      deviceName = UIDevice.CurrentDevice.Model;
      model = UIDevice.CurrentDevice.LocalizedModel;
      version = UIDevice.CurrentDevice.SystemVersion;
      api = UIDevice.CurrentDevice.SystemName;
      chipset = "N/A";
      
			NetworkReachabilityFlags flag;      
      Reachability.IsAdHocWiFiNetworkAvailable(out flag);
      
      if(flag == NetworkReachabilityFlags.Reachable){
      	wifiConnected = true;
      } else {
      	wifiConnected = false;
			}
						
      batteryPercentage = (int)(UIDevice.CurrentDevice.BatteryLevel * 100);
      
			int freeSpace = (int)NSFileManager.DefaultManager.GetFileSystemAttributes(Environment.GetFolderPath (Environment.SpecialFolder.Personal)).FreeSize;

    	freeMemory = freeSpace;
      loggingStatus = ion.dataLogManager.isRecording;
    }
  }
}

