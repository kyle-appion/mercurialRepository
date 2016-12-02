using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ION.Core.App;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ION.IOS.App;
using UIKit;
using ION.IOS.ViewController.RemoteAccess;
using ION.IOS.ViewController.AccessRequest;
using ION.Core.Devices;
using Foundation;
using System.Net.Http;
using ION.IOS.ViewController;
using ION.Core.Sensors;
using ION.Core.Content;
using ION.Core.Connections;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using System.Collections.ObjectModel;

namespace ION.Core.Net {
	public sealed class PreserveAttribute : System.Attribute 
	{
	    public bool AllMembers;
	    public bool Conditional;
	}
	public delegate void webTimeoutEvent(bool timeout);
	public delegate void webPauseEvent(bool paused);
	public class WebPayload {
		public IION ion;
		public WebClient webClient;
		HttpClient client;  
		public bool remoteViewing = false;   
		public bool uploading = false;
		public bool downloading 
		{
			get{
				return __downloading;
			} 
			set{
				__downloading = value;
				if(paused != null){
					if(value == false){
						paused(true);
					} else {
						paused(false);
					}
				}
			}
		} bool __downloading;
		
		public DateTime startedViewing;   
		public const string uploadSessionUrl = "http://portal.appioninc.com/App/uploadSession.php";
		public const string downloadSessionUrl = "http://portal.appioninc.com/App/downloadSession.php";
		public const string registerUserUrl = "http://portal.appioninc.com/App/registerUser.php";
		public const string submitCodeUrl = "http://portal.appioninc.com/App/submitAccessCode.php";
		public const string confirmAccessUrl = "http://portal.appioninc.com/App/confirmAccess.php";
		public const string retrieveAccessUrl = "http://portal.appioninc.com/App/retrieveAccess.php";
		public const string createAccessUrl = "http://portal.appioninc.com/App/requestAccess.php";
		public const string getRequestsUrl = "http://portal.appioninc.com/App/getRequests.php";
		public const string changeOnlineUrl = "http://portal.appioninc.com/App/changeOnlineStatus.php";
		public const string uploadLayoutsUrl = "http://portal.appioninc.com/App/uploadLayouts.php";
		public const string downloadLayoutsUrl = "http://portal.appioninc.com/App/downloadLayouts.php";
		public const string forgotAccountUrl = "http://portal.appioninc.com/App/forgotUserPass.php";
		public const string updateAccountUrl = "http://portal.appioninc.com/App/updateAccount.php";
		public webTimeoutEvent timedOut;
		public webPauseEvent paused;
		
		public WebPayload() {
			ion = AppState.context;
			webClient = new WebClient();
			webClient.Proxy = null;
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}
		/// <summary>
		/// Packages the session information for any session chosen by the user to be uploaded
		/// </summary>
		/// <param name="sessionList">Session list.</param>
		public void getSession(ObservableCollection<int> sessionList){
			
		  var paramList = new List<string>();

      foreach (var num in sessionList) {
        paramList.Add('"' + num.ToString() + '"');
      }
      
			var sessionResult = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow WHERE SID in (" + string.Join(",",paramList.ToArray()) + ") ORDER BY SID ASC");
			var totalSessions = sessionResult.Count;
			var count = 1;

			string jsonPayload = "{";    
			
			foreach (var session in sessionResult){
				var measurementResult = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT MID,serialNumber, sensorIndex, recordedDate, measurement FROM SensorMeasurementRow where frn_SID = ? ORDER BY recordedDate ASC",session.SID);
				
				jsonPayload += "\"session"+count+"\":{";
				jsonPayload += "\"start\":\""+session.sessionStart.ToLocalTime().ToString("yy-MM-dd HH:mm:ss")+"\",";
				jsonPayload += "\"end\":\""+session.sessionEnd.ToLocalTime().ToString("yy-MM-dd HH:mm:ss")+"\",";
				jsonPayload += "\"measurements\":[";
				var count2 = 1;
				if(measurementResult.Count > 0){					
					foreach(var entry in measurementResult){
						jsonPayload += "{\"measurement\":\""+entry.measurement + "\",";
						jsonPayload += "\"recorded\":\"" +entry.recordedDate.ToLocalTime().ToString("yy-MM-dd HH:mm:ss") + "\",";
						jsonPayload += "\"sindex\":\"" +entry.sensorIndex + "\",";
						jsonPayload += "\"sn\":\"" +entry.serialNumber + "\"}";
						if(count2 < measurementResult.Count){ 
							jsonPayload += ",";
						}
						count2++;
					}
				}
				jsonPayload += "]}";
				if(count < totalSessions){
					jsonPayload += ",";
				}
				count++;
			}
			
			jsonPayload += "}";
			//Console.WriteLine(jsonPayload);
			UploadSession(jsonPayload,true);
		}
		/// <summary>
		/// Uploads a session chosen by the user
		/// </summary>
		/// <param name="json">Json.</param>
		/// <param name="isJson">If set to <c>true</c> is json.</param>
		public async void UploadSession(string json, bool isJson = true){
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			var userID = KeychainAccess.ValueForKey("userID");

			try{		
				//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("uploadSession", "true"),          		
	              new KeyValuePair<string, string>("sessionData", json), 
	              new KeyValuePair<string, string>("userID", userID),  
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(uploadSessionUrl,formContent);
	
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				//JObject response = JObject.Parse(textResponse);
				
				//List<sessionData> deserializedSessions = new List<sessionData>();
				
				////Go through each token in the json object and serialize the data for each session
				////to be added to the list of deserialized sessions
				//for(int i = 1; i <= response.Count; i++){
				//	var token = "session"+i;
				//	try{
				//		var jsonToken = response.GetValue(token).ToString();
				//		var deserializedToken = JsonConvert.DeserializeObject<sessionData>(jsonToken);
				//		deserializedSessions.Add(deserializedToken);
				//	} catch (Exception e){ 
				//		Console.WriteLine("Exception: " + e);
				//	}
				//}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Session Upload", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
    /// <summary>
    /// Allows the user to register themselves for remote viewing
    /// </summary>
    /// <returns>post response</returns>
    public async Task<bool> RegisterUser(string firstName, string password, string lastName, string email){
    	await Task.Delay(TimeSpan.FromMilliseconds(1)); 
    	
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;	
		
			try{			
				//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("registerUser", "newuser"),          		
	              new KeyValuePair<string, string>("fname", firstName), 
	              new KeyValuePair<string, string>("usrpword", password), 
	              new KeyValuePair<string, string>("usrEmail", email), 
	              new KeyValuePair<string, string>("lname", lastName), 
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(registerUserUrl,formContent);
	
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var isregistered = response.GetValue("success").ToString();
				var message = response.GetValue("message").ToString();
				if(isregistered == "true"){
					return true;
				} else {
					var alert = UIAlertController.Create ("User Registration", message, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);					
					return false;
				}		
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception); 

				
				var alert = UIAlertController.Create ("User Registration", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				return false;
			}		
		}
 
    /// <summary>
    /// Downloads the session(s) for a supplied user id that fall between a start and end date
    /// </summary>
    /// <returns>post response</returns>
    public async void DownloadSessions(int accountID, int userID, string startDate, string endDate){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
	
			try{
			//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("downloadSession", "getData"),          		
	              new KeyValuePair<string, string>("userID", userID.ToString()), 
	              new KeyValuePair<string, string>("sessionStart", startDate), 
	              new KeyValuePair<string, string>("sessionEnd", endDate), 
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(downloadSessionUrl,formContent);
	
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				//JObject response = JObject.Parse(textResponse);
				//var isregistered = response.GetValue("success").ToString();
				//var registeredMessage = response.GetValue("message").ToString();
				//if(isregistered == "true"){
				//	Console.WriteLine("Created a new account with id: " + registeredMessage);
				//} else if (isregistered == "false"){
				//	Console.WriteLine("Couldn't create new account because " + registeredMessage);
				//}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Access Code", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}				
	}
	
	/// <summary>
	/// Uploads the devices a user has connected and their relative layout for
	/// the workbench and analyzer
	/// </summary>
	/// <returns>The system layout.</returns>
	public async Task uploadSystemLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var uploadAnalyzer = ion.currentAnalyzer;
		var uploadWorkbench = ion.currentWorkbench;
		var uploadDeviceManager = ion.deviceManager;
		
		var userID = KeychainAccess.ValueForKey("userID");
		
	  ///Start packaging all device manager devices	  
	  var layoutJson = "{\"known\":[";
	  var connectedCount = 0;
	  var totalCount = uploadDeviceManager.knownDevices.Count;
	  foreach(var device in uploadDeviceManager.knownDevices){
	  	if(device is GaugeDevice){
				if(connectedCount < totalCount && connectedCount > 0 ){
					layoutJson += ",";
				}
	  		var gDevice = device as GaugeDevice;
				layoutJson += "{\"sn\":\""+gDevice.serialNumber + "\",\"sa\":[";
				var sensorCounter = 1;
				for(int s = 0; s < gDevice.sensors.Length;s++){ 
					layoutJson += "{\"sv\":\""+gDevice.sensors[s].measurement.amount+"\",\"su\":\""+UnitLookup.GetCode(gDevice.sensors[s].unit)+"\",\"si\":\""+gDevice.sensors[0].index+"\"}";
					if(sensorCounter < gDevice.sensorCount){
						layoutJson += ",";
					}
					sensorCounter++;
				}				
				layoutJson += "], \"on\":\""+Convert.ToInt32(device.isConnected)+"\",\"bat\":\""+gDevice.battery+"\"}";
				connectedCount++;
			}
		}
		layoutJson += "],";  
	  ///Package the analyzer layout
		layoutJson += "\"alyzer\":[";
		string lowSecondary = "null";
		string highSecondary = "null";
		if(uploadAnalyzer != null && uploadAnalyzer.sensorList != null){			
			var count = 1;                                                                    
			foreach(var sensor in uploadAnalyzer.sensorList){
				var position = sensor.analyzerSlot;
			 	layoutJson += "{\"sn\":\""+sensor.name+"\",\"sl\":\""+position+"\",\"sv\":\""+sensor.measurement.amount+"\",\"su\":\""+UnitLookup.GetCode(sensor.unit)+"\",\"sa\":\""+sensor.analyzerArea+"\"}";
				if(count < uploadAnalyzer.sensorList.Count){
					layoutJson += ",";
				}
			
				count++;
			}		
		}
		if(uploadAnalyzer.lowSideManifold != null){
			lowSecondary = uploadAnalyzer.lowSideManifold.primarySensor.name;
		}
		if(uploadAnalyzer.highSideManifold != null){
			highSecondary = uploadAnalyzer.highSideManifold.primarySensor.name;
		}		
		
		layoutJson += "],\"setup\":{\"positions\":["+string.Join(",",uploadAnalyzer.sensorPositions)+"],\"fluid\":\""+ion.fluidManager.lastUsedFluid.name+"\"},\"LH\":{";
		if(uploadAnalyzer.lowAccessibility != "low"){
			layoutJson += "\"low\":\""+uploadAnalyzer.lowAccessibility+"\",\"las\":\"" + lowSecondary + "\", \"lsub\":[";
		} else {
			layoutJson += "\"low\":\""+uploadAnalyzer.lowAccessibility+"\",\"las\":\"null\", \"lsub\":[";
		}
		for(int s = 0; s < uploadAnalyzer.lowSubviews.Count;s++){
			layoutJson += "\""+uploadAnalyzer.lowSubviews[s]+"\"";
			if(s < uploadAnalyzer.lowSubviews.Count - 1){
				layoutJson += ",";
			}
		}
		if(uploadAnalyzer.highAccessibility != "high"){
			layoutJson += "],\"high\":\""+uploadAnalyzer.highAccessibility+"\", \"has\":\"" + highSecondary + "\", \"hsub\":[";
		} else {
			layoutJson += "],\"high\":\""+uploadAnalyzer.highAccessibility+"\", \"has\":\"null\", \"hsub\":[";
		}
		for(int s = 0; s < uploadAnalyzer.highSubviews.Count;s++){
			layoutJson += "\""+uploadAnalyzer.highSubviews[s]+"\"";
			if(s < uploadAnalyzer.highSubviews.Count - 1){
				layoutJson += ",";
			}
		}
		layoutJson += "]},";
		
		///Package the workbench layout
		layoutJson += "\"workB\" : [";
		if(uploadWorkbench != null && uploadWorkbench.manifolds != null){
			var count = 1;  
			foreach(var manifold in uploadWorkbench.manifolds){
				layoutJson += "{\"sn\":\""+manifold.primarySensor.name+"\",";
				layoutJson += "\"amount\":\""+manifold.primarySensor.measurement.amount+"\",\"unit\":\""+UnitLookup.GetCode(manifold.primarySensor.unit)+"\",";

				layoutJson += "\"sub\":[";
				var subcount = 1;
				foreach(var sub in manifold.sensorProperties){
					var sensorEnum = GetSensorPropertyEnum(sub);
					layoutJson += sensorEnum;
					if(subcount < manifold.sensorProperties.Count){
						layoutJson += ",";
					}
					subcount++;
				}
				layoutJson += "]}";
				if(count < uploadWorkbench.manifolds.Count){
					layoutJson += ",";
				}
				count++;
			}
		}
		layoutJson += "]}";

		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("uploadLayouts", "manager"),
              new KeyValuePair<string, string>("layoutJson", layoutJson),
              new KeyValuePair<string, string>("userID", userID),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(uploadLayoutsUrl,formContent);

			var textReponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textReponse);
		} catch (Exception exception){
			Console.WriteLine(exception);
		}
	}
	/// <summary>
	/// Pulls the layout for a selected user and is parsed
	/// to update the workbench,analyzer, and device manager
	/// </summary>
	/// <returns>The layouts.</returns>
	public async Task DownloadLayouts(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var workbench = ion.currentWorkbench.storedWorkbench;
		var remoteAnalyzer = ion.currentAnalyzer;
		var activeManifolds = new List<string>();
		var activeAnalyzerSensors = new List<string>();
		var userID = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");
		
		try{
			var remoteDManager = ion.deviceManager as RemoteBaseDeviceManager;
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("downloadLayouts", "manager"),          		
              new KeyValuePair<string, string>("userID", userID),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(downloadLayoutsUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var retrieved = response.GetValue("success"); 
			if(retrieved == null){
			
				var dManager = response.GetValue("known");
				///create and update the device manager devices
				foreach (var con in dManager) {
					var deserializedToken = JsonConvert.DeserializeObject<connectedData>(con.ToString());
					var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var manualDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, Convert.ToBoolean(deserializedToken.connected));

					if(remoteDManager.knownDevices.Contains(manualDevice)){
							var updateConnection = remoteDManager[iserial].connection as RemoteConnection;
							var gDevice = remoteDManager[iserial] as GaugeDevice;
							gDevice.SetBatteryRemoteDevice(deserializedToken.battery);
						if(Convert.ToBoolean(deserializedToken.connected)){							
							updateConnection.Connect();
						} else {
							updateConnection.Disconnect();
						}
					} else {
						var newDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, Convert.ToBoolean(deserializedToken.connected)) as GaugeDevice;
						newDevice.SetBatteryRemoteDevice(deserializedToken.battery);
						for(int i = 0; i < deserializedToken.sensors.Length;i++){
							newDevice.sensors[i].RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.sensors[i].unit),deserializedToken.sensors[i].measurement));
						}
						remoteDManager.Register(newDevice);  
					} 
				}

				var aManager = response.GetValue("alyzer");
				var sensorOrder = response.GetValue("setup");
				var aLowHigh = response.GetValue("LH");
				var deserializedPositions = JsonConvert.DeserializeObject<analyzerPositions>(sensorOrder.ToString());
				var deserializedLowHigh = JsonConvert.DeserializeObject<analyzerLowHigh>(aLowHigh.ToString());
				foreach (var con in aManager) {
					var deserializedToken = JsonConvert.DeserializeObject<analyzerSetup>(con.ToString());

					foreach(var device in remoteDManager.knownDevices){
						var gDevice = device as GaugeDevice;
						if(gDevice.serialNumber.rawSerial == deserializedToken.serialNumber){
							activeAnalyzerSensors.Add(deserializedToken.serialNumber+UnitLookup.GetSensorTypeFromCode(deserializedToken.unit));
							if(remoteAnalyzer.sensorList.Contains(gDevice.sensors[0]) && gDevice.sensors[0].type == UnitLookup.GetSensorTypeFromCode(deserializedToken.unit)){
								gDevice.sensors[0].RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
							} else if (gDevice.sensors.Length > 1 && remoteAnalyzer.sensorList.Contains(gDevice.sensors[1]) && gDevice.sensors[1].type == UnitLookup.GetSensorTypeFromCode(deserializedToken.unit)){
								gDevice.sensors[1].RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
							} else {
								if(gDevice.serialNumber.rawSerial == deserializedToken.serialNumber){
									if(UnitLookup.GetSensorTypeFromCode(deserializedToken.unit) == ESensorType.Pressure || UnitLookup.GetSensorTypeFromCode(deserializedToken.unit) == ESensorType.Vacuum){
										gDevice.sensors[0].analyzerSlot = deserializedToken.position;
										gDevice.sensors[0].analyzerArea = deserializedToken.area;
										remoteAnalyzer.sensorList.Add(gDevice.sensors[0]);
									}
									else {
										gDevice.sensors[1].analyzerSlot = deserializedToken.position;
										gDevice.sensors[1].analyzerArea = deserializedToken.area;
										remoteAnalyzer.sensorList.Add(gDevice.sensors[1]);   
									}
								}
							}
							break;
 						}
						////SET THE SECONDARY SENSOR FOR LOW AND HIGH AREAS
						if(deserializedLowHigh.lowAttached != "null"){
	 						if(device.serialNumber.rawSerial == deserializedLowHigh.lowAttached){
								ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,gDevice.sensors[1]);
							}
						} else {
							ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,null);
						} 						
						if(deserializedLowHigh.highAttached != "null"){
	 						if(device.serialNumber.rawSerial == deserializedLowHigh.highAttached){
								ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,gDevice.sensors[1]);
							}
						} else {
							ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,null);
						}

					}
				}
				if(ion.fluidManager.lastUsedFluid.name != deserializedPositions.fluid){
					await ion.fluidManager.GetFluidAsync(deserializedPositions.fluid);
				}
				remoteAnalyzer.sensorPositions = new List<int>(deserializedPositions.sensorPositions);
				remoteAnalyzer.revertPositions = new List<int>(deserializedPositions.sensorPositions);
				remoteAnalyzer.lowAccessibility = deserializedLowHigh.lowAccessibility;
				remoteAnalyzer.lowSubviews = new List<string>(deserializedLowHigh.lowSubviews);
				remoteAnalyzer.highAccessibility = deserializedLowHigh.highAccessibility;			
				remoteAnalyzer.highSubviews = new List<string>(deserializedLowHigh.highSubviews);
				foreach(var aSensor in remoteAnalyzer.sensorList.ToArray()){
					if(!activeAnalyzerSensors.Contains(aSensor.name+aSensor.type)){
						remoteAnalyzer.sensorList.Remove(aSensor);
						Console.WriteLine("Removed a sensor from analyzer list");
					}
				}

			
				var wManager = response.GetValue("workB");				
				foreach(var con in wManager){
					var deserializedToken = JsonConvert.DeserializeObject<workbenchSetup>(con.ToString());
					var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var wbconnected = false;
					activeManifolds.Add(deserializedToken.serialNumber+UnitLookup.GetSensorTypeFromCode(deserializedToken.unit));

					var existing = workbench.GetDeviceIndex(iserial,deserializedToken.unit);

					if(existing != 99){
						var updateManifold = workbench.manifolds[existing];
						updateManifold.primarySensor.RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
						/// add any sensor properties that a remote user has added only if they don't exist already
						foreach(var sub in deserializedToken.subviews){
							switch(sub){
								case 1:
									if(!updateManifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))){									
										updateManifold.AddSensorProperty(new PTChartSensorProperty(updateManifold));
									}
									break;
								case 2:
									if(!updateManifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))){
										updateManifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(updateManifold));
									}
									break;
								case 3:
									if (!updateManifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
										updateManifold.AddSensorProperty(new MinSensorProperty(updateManifold.primarySensor));
									}
									break;
								case 4:
									if (!updateManifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
										updateManifold.AddSensorProperty(new MaxSensorProperty(updateManifold.primarySensor));
									}
									break;
								case 5:
									if (!updateManifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
										updateManifold.AddSensorProperty(new HoldSensorProperty(updateManifold.primarySensor));
									}
									break;
								case 6:
									if (!updateManifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
										updateManifold.AddSensorProperty(new RateOfChangeSensorProperty(updateManifold.primarySensor));
									}
									break;
								case 7:
									if (!updateManifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
										updateManifold.AddSensorProperty(new TimerSensorProperty(updateManifold.primarySensor));
									}
									break;
								case 8:
						      if (updateManifold.secondarySensor != null) {
						        if (!updateManifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
						            updateManifold.AddSensorProperty(new SecondarySensorProperty(updateManifold));
						        }
						      }
									break;
								default:
									Console.WriteLine("Unknown....");
									break;
							}
						}
						///Remove any subviews a remote user has removed
						foreach(var sp in updateManifold.sensorProperties.ToArray()){
							var propertyIndex = GetSensorPropertyEnum(sp);
							var exists = Array.IndexOf(deserializedToken.subviews,propertyIndex); 
							if(exists == -1){
								updateManifold.RemoveSensorProperty(sp);
							}
						}
					} else {
						foreach(var idevice in remoteDManager.knownDevices){
							if(idevice.serialNumber.rawSerial == iserial.rawSerial){
								if(idevice.isConnected){
									wbconnected = true;
								}
								break;
							}
						}
						///manifold has been added by the remote user but doesn't exist for viewing user so create it and add it's sensor properties
						var manualDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, wbconnected) as GaugeDevice;

						if(ESensorType.Pressure == UnitLookup.GetSensorTypeFromCode(deserializedToken.unit) || ESensorType.Vacuum == UnitLookup.GetSensorTypeFromCode(deserializedToken.unit)){
							var manualManifold = new ION.Core.Content.Manifold(manualDevice.sensors[0]);
							manualManifold.ptChart = PTChart.New(AppState.context,Fluid.EState.Dew);
							workbench.Add(manualManifold);
							SetupNewManifoldProperties(manualManifold,deserializedToken);							
						} else {   
							var manualManifold = new ION.Core.Content.Manifold(manualDevice.sensors[1]);							
							manualManifold.ptChart = PTChart.New(AppState.context,Fluid.EState.Dew);
							workbench.Add(manualManifold);
							SetupNewManifoldProperties(manualManifold,deserializedToken);
						}
					}					
				}

				foreach(var manifold in workbench.manifolds.ToArray()){
					if(!activeManifolds.Contains(manifold.primarySensor.name+manifold.primarySensor.type)){
						workbench.Remove(manifold);
					}
				}
			}
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
		}
	}

	public async void SetupNewManifoldProperties(Manifold manualManifold, workbenchSetup deserializedToken){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		foreach(var sub in deserializedToken.subviews){
			switch(sub){
				case 1:
					manualManifold.AddSensorProperty(new PTChartSensorProperty(manualManifold));
					break;
				case 2:				
					manualManifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manualManifold));
					break;
				case 3:
					manualManifold.AddSensorProperty(new MinSensorProperty(manualManifold.primarySensor));
					break;
				case 4:						
					manualManifold.AddSensorProperty(new MaxSensorProperty(manualManifold.primarySensor));
					break;
				case 5:							
					manualManifold.AddSensorProperty(new HoldSensorProperty(manualManifold.primarySensor));
					break;
				case 6:								
					manualManifold.AddSensorProperty(new RateOfChangeSensorProperty(manualManifold.primarySensor));
					break;
				case 7:								
					manualManifold.AddSensorProperty(new TimerSensorProperty(manualManifold.primarySensor));
					break;
				case 8:							
					manualManifold.AddSensorProperty(new SecondarySensorProperty(manualManifold));
					break;
			}
		}
	}	
	/// <summary>
	/// Queries for everyone a user has viewing access for
	/// </summary>
	/// <returns>A list of ids associated the user</returns>
	/// <param name="accessList">Access list.</param>
	public async Task GetAccessList(List<accessData> onlineList,List<accessData> offlineList){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

       var userID = KeychainAccess.ValueForKey("userID");
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("retrieveAccess","users");
			data.Add("userID",userID);
 			try{
				//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("retrieveAccess", "manager"),          		
	              new KeyValuePair<string, string>("userID", userID), 
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(retrieveAccessUrl,formContent);
	
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var retrieved = response.GetValue("success");
				if(retrieved.ToString() == "true"){
					var users = response.GetValue("users");
					foreach(var user in users){
						var deserializedToken = JsonConvert.DeserializeObject<accessData>(user.ToString());
						if(deserializedToken.online == 1){
							onlineList.Add(deserializedToken);
						} else {
							offlineList.Add(deserializedToken);
						}
					}
				} else {
					Console.WriteLine("No users found");
				}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Access List", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}		
	}
	/// <summary>
	/// Generates a random access code that will associate users to one another for viewing
	/// </summary>
	/// <returns>The access code.</returns>
	/// <param name="pendingUsers">Pending users.</param>
	public async Task GenerateAccessCode(List<requestData> pendingUsers){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = KeychainAccess.ValueForKey("userID");
		
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("createAccess", "true"),          		
              new KeyValuePair<string, string>("userID", userID),
              new KeyValuePair<string, string>("permanent", "1"), 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(createAccessUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var success = response.GetValue("success");
			
			if(success.ToString() == "true"){
				var newCode = response.GetValue("message").ToString();
				pendingUsers.Add(new requestData(){displayName = "Pending",id = 0, accessCode = newCode});
			} else {
					var window = UIApplication.SharedApplication.KeyWindow;
      		var rootVC = window.RootViewController as IONPrimaryScreenController;
					var errorMessage = response.GetValue("message");
					
					var alert = UIAlertController.Create ("Access Code", errorMessage.ToString(), UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);				
			}
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Access Code", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}
	}	
	/// <summary>
	/// Submits the access code a user enters to update viewing access associated with the code
	/// </summary>
	/// <param name="codeText">Code text.</param>
	public async Task submitAccessCode(string codeText){
		await Task.Delay(TimeSpan.FromMilliseconds(1));			

		var window = UIApplication.SharedApplication.KeyWindow;
		var rootVC = window.RootViewController as IONPrimaryScreenController;
		
		var userID = KeychainAccess.ValueForKey("userID");
		
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("confirmCode", "true"),          		
              new KeyValuePair<string, string>("userID", userID),
              new KeyValuePair<string, string>("accessCode", codeText), 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(confirmAccessUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var responseMessage = response.GetValue("message").ToString();
			
			var alert = UIAlertController.Create ("Confirm Code", responseMessage, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var alert = UIAlertController.Create ("Confirm Code", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}
	}
	/// <summary>
	/// Queries for all the open requests a user has
	/// </summary>
	/// <returns>A list of request codes and their claimed status</returns>
	/// <param name="pendingUsers">Pending users.</param>
	public async Task getAllRequests(List<requestData> pendingUsers){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = KeychainAccess.ValueForKey("userID");

		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("getRequests", "true"),          		
              new KeyValuePair<string, string>("userID", userID), 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(getRequestsUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var success = response.GetValue("success");

			if(success.ToString() == "true"){
				var users = response.GetValue("users");
				
				foreach(var user in users){
					var deserializedToken = JsonConvert.DeserializeObject<requestData>(user.ToString());
					pendingUsers.Add(deserializedToken);
				}		
			} else {
				var errorMessage = response.GetValue("message").ToString();
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Access Requests", errorMessage, UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);	
			}
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Access Requests", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}				
	}
	/// <summary>
	/// Sets a user to be online or offline for uploading/retrieving their data
	/// </summary>
	/// <returns>The online status.</returns>
	/// <param name="status">Status.</param>
	/// <param name="rootVC">Root vc.</param>
	public async Task<bool> updateOnlineStatus(string status){

		return await Task.Factory.StartNew(() => {

			var userID = KeychainAccess.ValueForKey("userID");
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("changeStatus","true");
			data.Add("userID", userID);
			data.Add("status",status); 			
	
			try{
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(changeOnlineUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
	
				if(success.ToString() == "true"){
					return true;
				} else {
					return false;
				}			
	
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				return false;
			}			
		});			
	}
	
	/// <summary>
	/// User enters an email to recieve their username and a new
	/// password at the entered email address
	/// </summary>
	/// <param name="recoverEmail">Recover email.</param>
	public async void resetPassword(string recoverEmail){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("forgotten", "true"),          		
              new KeyValuePair<string, string>("email", recoverEmail), 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(forgotAccountUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var success = response.GetValue("success");

			var errorMessage = response.GetValue("message").ToString();
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Account Recovery", errorMessage, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);

		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Access Requests", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}		
	}
	/// <summary>
	/// Updates the password.
	/// </summary>
	/// <param name="newPword">New pword.</param>
	public async void updatePassword(string newPword){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = KeychainAccess.ValueForKey("userID");
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("updatePassword", "new"),          		
              new KeyValuePair<string, string>("newPassword", newPword), 
              new KeyValuePair<string, string>("userID", userID) 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(updateAccountUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);

			var errorMessage = response.GetValue("message").ToString();
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Account Update", errorMessage, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);

		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Account Update", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}			
	}
	
	/// <summary>
	/// Updates the email.
	/// </summary>
	/// <param name="newEmail">New email.</param>
	public async void updateEmail(string newEmail, UITextField eField){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = KeychainAccess.ValueForKey("userID");
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("updateEmail", "new"),          		
              new KeyValuePair<string, string>("newEmail", newEmail), 
              new KeyValuePair<string, string>("userID", userID) 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(updateAccountUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var success = response.GetValue("success").ToString();
			if(success == "true"){
				eField.Text = newEmail;
			}
			var errorMessage = response.GetValue("message").ToString();
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Account Update", errorMessage, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);

		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Account Update", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}			
	}
	
	/// <summary>
	/// Kicks off the download process for remote viewing and manages it based on if a user quits or not
	/// </summary>
	public async void StartLayoutDownload(){
		startedViewing = DateTime.Now;
		while(downloading){
		  var timeDifference = DateTime.Now.Subtract(startedViewing).Minutes;

			if(timeDifference < 30){
				var loggedUser = KeychainAccess.ValueForKey("userID");
				if(!string.IsNullOrEmpty(loggedUser)){
					await DownloadLayouts();
					await Task.Delay(TimeSpan.FromSeconds(1));
				} else {
					downloading = false;
				}
			} else {
				downloading = false;
				if(timedOut != null){
					timedOut(true);
				}
			}
		}
	}
	
	public int GetSensorPropertyEnum(ISensorProperty type){
		var property = type.GetType();
		
		if(typeof(PTChartSensorProperty).Equals(property)){
			return 1;
		} else if (typeof(SuperheatSubcoolSensorProperty).Equals(property)){
			return 2;
		} else if (typeof(MinSensorProperty).Equals(property)){
			return 3;
		} else if (typeof(MaxSensorProperty).Equals(property)){
			return 4;
		} else if (typeof(HoldSensorProperty).Equals(property)){
			return 5;
		} else if (typeof(RateOfChangeSensorProperty).Equals(property)){
			return 6;
		} else if (typeof(TimerSensorProperty).Equals(property)){
			return 7;
		} else {
			return 8;
		}	
	}
	
	public int GetSensorSubviewCode(string property){
		if(property == "Pressure"){
			return 1;
		} else if (property == "Superheat"){
			return 2;
		} else if (property == "Minimum"){
			return 3;
		} else if (property == "Maximum"){
			return 4;
		} else if (property == "Hold"){
			return 5;
		} else if (property == "Rate"){
			return 6;
		} else if (property == "Alternate"){
			return 7;
		} else {
			return 8;
		}
	}
	public string GetSensorCodeSubview(int subviewCode){
		if(subviewCode == 1){
			return "Pressure";
		} else if (subviewCode == 2){
			return "Superheat";
		} else if (subviewCode == 3){
			return "Minimum";
		} else if (subviewCode == 4){
			return "Maximum";
		} else if (subviewCode == 5){
			return "Hold";
		} else if (subviewCode == 6){
			return "Rate";
		} else if (subviewCode == 7){
			return "Alternate";
		} else {
			return "Linked";
		}
	}	
}


	[Preserve(AllMembers = true)]
	public class sessionData{
		public sessionData(){}
		//public int SID;
		[JsonProperty("start")]
		public string start { get; set; }
		[JsonProperty("end")]
		public string end { get; set; }
		[JsonProperty("measurements")]
		public measurementData[] measurements;
	}
	
	[Preserve(AllMembers = true)]
	public class measurementData {
		public measurementData(){}
		[JsonProperty("measurement")]
		public double measurement { get; set; }
		[JsonProperty("recorded")]
		public string recorded { get; set; }
		[JsonProperty("sensorIndex")]
		public int sensorIndex { get; set; }
		[JsonProperty("serialNumber")]
		public string serialNumber { get; set; }
	}
	
	[Preserve(AllMembers = true)]
	public class connectedData {
		[JsonProperty("sn")]
		public string serialNumber { get; set; }
		[JsonProperty("sa")]
		public sensorData[] sensors { get; set; }
		[JsonProperty("on")]
		public int connected { get; set; }
		[JsonProperty("bat")]
		public int battery { get; set; }		
	}	
	
	[Preserve(AllMembers = true)]
	public class sensorData {
		public sensorData(){}
		[JsonProperty("sv")]
		public double measurement { get; set; }
		[JsonProperty("su")]
		public int unit { get; set; }
		[JsonProperty("si")]
		public int sensorIndex { get; set; }
	}	
	
	[Preserve(AllMembers = true)]
	public class analyzerSetup {
		public analyzerSetup(){}
		[JsonProperty("sn")]
		public string serialNumber { get; set; }
		[JsonProperty("sl")]
		public int position { get; set; }
		[JsonProperty("sv")]
		public double measurement { get; set; }
		[JsonProperty("su")]
		public int unit { get; set; }
		[JsonProperty("sa")]
		public int area { get; set; }		
	}
	
	[Preserve(AllMembers = true)]
	public class workbenchSetup {
		public workbenchSetup(){}
		[JsonProperty("sn")]
		public string serialNumber { get; set; }
		[JsonProperty("amount")]
		public double measurement { get; set; }
		[JsonProperty("unit")]
		public int unit { get; set; }
		[JsonProperty("sub")]
		public int [] subviews {get;set;}
	}
	
	[Preserve(AllMembers = true)]
	public class analyzerPositions {
		public analyzerPositions(){}
		[JsonProperty("positions")]
		public int [] sensorPositions {get;set;}
		[JsonProperty("fluid")]
		public string fluid {get;set;}
	}
	[Preserve(AllMembers = true)]
	public class analyzerLowHigh {
		public analyzerLowHigh(){}
		[JsonProperty("low")]
		public string lowAccessibility {get;set;}
		[JsonProperty("high")]
		public string highAccessibility {get;set;}
		[JsonProperty("lsub")]
		public string[] lowSubviews {get;set;}
		[JsonProperty("hsub")]
		public string[] highSubviews {get;set;}
		[JsonProperty("las")]
		public string lowAttached {get;set;}
		[JsonProperty("has")]
		public string highAttached {get;set;}		
	}
}

