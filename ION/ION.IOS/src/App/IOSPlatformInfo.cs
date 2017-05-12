namespace ION.IOS.App {

using System;
using System.Net;
using SystemConfiguration;
using CoreFoundation;

  using UIKit;
  using Foundation;

  using ION.Core.App;

  public class IOSPlatformInfo : IPlatformInfo {  
    public string manufacturer { get; set; }
    public string deviceName { get; set; }
    public string model { get; set; }
    public string version { get; set; }
    public string api { get; set; }
    public string chipset { get; set; }
    public bool wifiConnected { get; set; }
    public int batteryPercentage { get; set; }
    public long freeMemory { get; set; }
    public bool loggingStatus { get; set; }

    public IOSPlatformInfo() {
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
      
			long freeSpace = (long)NSFileManager.DefaultManager.GetFileSystemAttributes (Environment.GetFolderPath (Environment.SpecialFolder.Personal)).FreeSize;
			//freeSpace /= 1024;
			//freeSpace /= 1024;
			//freeSpace /= 1000;

    	freeMemory = freeSpace;
    }
    
    public string GetDeviceName(){
      return UIDevice.CurrentDevice.Model;
    }     
  }
  
	public enum NetworkStatus {
		NotReachable,
		ReachableViaCarrierDataNetwork,
		ReachableViaWiFiNetwork
	}
  


	public static class Reachability {
		public static string HostName = "www.google.com";

		public static bool IsReachableWithoutRequiringConnection (NetworkReachabilityFlags flags)
		{
			// Is it reachable with the current network configuration?
			bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

			// Do we need a connection to reach it?
			bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0
				|| (flags & NetworkReachabilityFlags.IsWWAN) != 0;

			return isReachable && noConnectionRequired;
		}

		// Is the host reachable with the current network configuration
		public static bool IsHostReachable (string host)
		{
			if (string.IsNullOrEmpty (host))
				return false;

			using (var r = new NetworkReachability (host)) {
				NetworkReachabilityFlags flags;

				if (r.TryGetFlags (out flags))
					return IsReachableWithoutRequiringConnection (flags);
			}
			return false;
		}

		//
		// Raised every time there is an interesting reachable event,
		// we do not even pass the info as to what changed, and
		// we lump all three status we probe into one
		//
		public static event EventHandler ReachabilityChanged;

		static void OnChange (NetworkReachabilityFlags flags)
		{
			ReachabilityChanged?.Invoke (null, EventArgs.Empty);
		}

		//
		// Returns true if it is possible to reach the AdHoc WiFi network
		// and optionally provides extra network reachability flags as the
		// out parameter
		//
		static NetworkReachability adHocWiFiNetworkReachability;

		public static bool IsAdHocWiFiNetworkAvailable (out NetworkReachabilityFlags flags)
		{
			if (adHocWiFiNetworkReachability == null) {
				var ipAddress = new IPAddress (new byte[] { 169, 254, 0, 0 });
				adHocWiFiNetworkReachability = new NetworkReachability (ipAddress.MapToIPv6 ());
				adHocWiFiNetworkReachability.SetNotification (OnChange);
				adHocWiFiNetworkReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			return adHocWiFiNetworkReachability.TryGetFlags (out flags) && IsReachableWithoutRequiringConnection (flags);
		}

		static NetworkReachability defaultRouteReachability;

		static bool IsNetworkAvailable (out NetworkReachabilityFlags flags)
		{
			if (defaultRouteReachability == null) {
				var ipAddress = new IPAddress (0);
				defaultRouteReachability = new NetworkReachability (ipAddress.MapToIPv6 ());
				defaultRouteReachability.SetNotification (OnChange);
				defaultRouteReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
			}
			return defaultRouteReachability.TryGetFlags (out flags) && IsReachableWithoutRequiringConnection (flags);
		}

		static NetworkReachability remoteHostReachability;


		public static NetworkStatus InternetConnectionStatus ()
		{
			NetworkReachabilityFlags flags;
			bool defaultNetworkAvailable = IsNetworkAvailable (out flags);

			if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
				return NetworkStatus.NotReachable;

			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
				return NetworkStatus.ReachableViaCarrierDataNetwork;

			if (flags == 0)
				return NetworkStatus.NotReachable;

			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		public static NetworkStatus LocalWifiConnectionStatus ()
		{
			NetworkReachabilityFlags flags;
			if (IsAdHocWiFiNetworkAvailable (out flags))
			if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
				return NetworkStatus.ReachableViaWiFiNetwork;

			return NetworkStatus.NotReachable;
		}
	}
  
  
}

