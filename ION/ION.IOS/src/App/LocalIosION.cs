namespace ION.IOS.App {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using CoreFoundation;
	using Foundation;
	using UIKit;

	using Appion.Commons.Util;

	using ION.Core.Alarms;
	using ION.Core.Alarms.Alerts;
	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Content.Parsers;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Report.DataLogs;
	
	using ION.CoreExtensions.Net.Portal;

	using ION.IOS.Alarms.Alerts;
	using ION.IOS.IO;
	using ION.IOS.Location;
	using ION.IOS.Connections;

	/// <summary>
	/// The iOS ION implementation.
	/// </summary>
	public class LocalIosION : IosION {

		public LocalIosION(IONPortalService portal) : base(portal) {
		}
		
    protected override void OnCreateManagers() {
      var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      
      fileManager = new IosFileManager();
      database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this);
      locationManager = new IosLocationManager(this);
      deviceManager = new BaseDeviceManager(this, new IonCBCentralManagerDelegate(this)); 
      alarmManager = new BaseAlarmManager(this);
      dataLogManager = new DataLogManager(this);
      fluidManager = new BaseFluidManager(this);
      
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, new PopupWindowAlarmAlert(alarm), new VibrateAlarmAlert(alarm, this), new SoundAlarmAlert(alarm, this));
      };
    }
    
    public void onWorkbenchEvent(WorkbenchEvent e){
			if(e.type != WorkbenchEvent.EType.ManifoldEvent || (e.manifoldEvent.type != ManifoldEvent.EType.Invalidated)){
				Log.D(this,"Workbench event saving workbench layout");
				SaveWorkbenchAsync();
			}
		}
  } // End IosION
}
