using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ION.Core.App;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ION.Core.Devices;
using System.Net.Http;
using ION.Core.Sensors;
using ION.Core.Content;
using ION.Core.Connections;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using System.Collections.ObjectModel;
using Appion.Commons.Measure;
using System.Text.RegularExpressions;


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
					}  else {
						paused(false);
					}
				}
			}
		}  bool __downloading;       
		
		public DateTime startedViewing;   
		public const string loginUserUrl = "http://portal.appioninc.com/App/applogin.php";
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
		public async Task<HttpResponseMessage> getSession(ObservableCollection<int> sessionList, string ID){
			
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
						jsonPayload += "\"sn\":\"" +entry.serialNumber + "\"} ";
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
			var response = await UploadSession(jsonPayload,ID,true);
			return response;
		}
		/// <summary>
		/// Uploads a session chosen by the user
		/// </summary>
		/// <param name="json">Json.</param>
		/// <param name="isJson">If set to <c>true</c> is json.</param>
		//public async void UploadSession(string json, bool isJson = true){
		public async Task<HttpResponseMessage> UploadSession(string json, string id, bool isJson = true){
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			var ID = id;    

			try{		
				//Create the data package to send for the post request
				//Key value pair for post body variable check
				//requires to be sent as the body instead of as form encoded url variables due to size limitations of 1023 characters

	      var message = new sessionUploadPackage{
						uploadSession = true,
						sessionData = json,
						userID = ID
				};
				// Serialize our concrete class into a JSON String
		    var stringPayload = JsonConvert.SerializeObject(message);
		    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
			      
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(uploadSessionUrl,content);

				return feedback;
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);

				return null;
			}
		}
    /// <summary>
    /// Allows the user to register themselves for remote viewing
    /// </summary>
    /// <returns>post response</returns>
    public async Task<HttpResponseMessage> RegisterUser(string password, string email){
    	await Task.Delay(TimeSpan.FromMilliseconds(1)); 
    			
			try{			
				//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("registerUser", "newuser"),
	              new KeyValuePair<string, string>("usrpword", password),
	              new KeyValuePair<string, string>("usrEmail", email),
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(registerUserUrl,formContent);
				
				return feedback;
			}  catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				return null;
			}		
		}
 
    /// <summary>
    /// Downloads the session(s) for a supplied user id that fall between a start and end date
    /// </summary>
    /// <returns>post response</returns>
    public async Task<HttpResponseMessage> DownloadSessions(int accountID, int userID, string startDate, string endDate){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
	
			try{
			//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("downloadSession", "getData"),          		
	              new KeyValuePair<string, string>("userID", userID.ToString()), 
	              new KeyValuePair<string, string>("sessionStart", startDate),
	              new KeyValuePair<string, string>("sessionEnd", endDate)
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(downloadSessionUrl,formContent);
	
				//var textResponse = await feedback.Content.ReadAsStringAsync();
				//Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				//JObject response = JObject.Parse(textResponse);
				//var isregistered = response.GetValue("success").ToString();
				//var registeredMessage = response.GetValue("message").ToString();
				//if(isregistered == "true"){
				//	Console.WriteLine("Created a new account with id: " + registeredMessage);
				//} else if (isregistered == "false"){
				//	Console.WriteLine("Couldn't create new account because " + registeredMessage);
				//}
				return feedback;
			}  catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				return null;
				//var window = UIApplication.SharedApplication.KeyWindow;
    //		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				//var alert = UIAlertController.Create ("Access Code", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				//alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				//rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}				
	}
	
	/// <summary>
	/// Uploads the devices a user has connected and their relative layout for
	/// the workbench and analyzer
	/// </summary>
	/// <returns>The system layout.</returns>
	public async Task uploadSystemLayout(string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var uploadAnalyzer = ion.currentAnalyzer;
		var uploadWorkbench = ion.currentWorkbench;
		var uploadDeviceManager = ion.deviceManager;
		
		var userID = ID;
		
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
					layoutJson += "{\"sv\":\""+gDevice.sensors[s].measurement.amount+"\",\"su\":\""+UnitLookup.GetCode(gDevice.sensors[s].unit)+"\",\"si\":\""+gDevice.sensors[s].index+"\"} ";
					if(sensorCounter < gDevice.sensorCount){
						layoutJson += ",";
					}
					sensorCounter++;
				}				
				layoutJson += "], \"on\":\""+Convert.ToInt32(device.isConnected)+"\",\"bat\":\""+gDevice.battery+"\"} ";
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
				var gaugeSensor = sensor as GaugeDeviceSensor;
				
			 	layoutJson += "{\"sn\":\""+sensor.name+"\",\"sa\":\"" + sensor.analyzerArea + "\",\"sl\":\""+sensor.analyzerSlot+"\",\"sv\":\""+sensor.measurement.amount+"\",\"su\":\""+UnitLookup.GetCode(sensor.unit)+"\",\"si\":\"" + gaugeSensor.index + "\"} ";
				if(count < uploadAnalyzer.sensorList.Count){
					layoutJson += ",";
				}
			
				count++;
			}		
		}
		////GET LOW SIDE ATTACHED SENSOR
		if(uploadAnalyzer.lowSideManifold != null){
			var attachedGaugeSensor = uploadAnalyzer.lowSideManifold.primarySensor as GaugeDeviceSensor;
			lowSecondary = attachedGaugeSensor.device.serialNumber.rawSerial;
		}
		////GET HIGH SIDE ATTACHED SENSOR
		if(uploadAnalyzer.highSideManifold != null){
			var attachedGaugeSensor = uploadAnalyzer.highSideManifold.primarySensor as GaugeDeviceSensor;
			highSecondary = attachedGaugeSensor.device.serialNumber.rawSerial;
		}		
		layoutJson += "],\"setup\":{\"positions\":["+string.Join(",",uploadAnalyzer.sensorPositions)+"],\"lfluid\":\"";
		///SEND THE LOW SIDE MANIFOLD FLUID
		if(uploadAnalyzer.lowFluid != null){
			layoutJson += uploadAnalyzer.lowFluid.name+"\",\"hfluid\":\"";
		} else {
			layoutJson += "null\",\"hfluid\":\"";
		}
		///SEND THE HIGH SIDE MANIFOLD FLUID
		if(uploadAnalyzer.highFluid != null){
			layoutJson += uploadAnalyzer.highFluid.name + "\"} ,\"LH\":{";
		} else {
			layoutJson += "null\"} ,\"LH\":{";
		}
		///SEND THE LOW SIDE SLOT AREA AND ATTACHED SENSOR
		if(uploadAnalyzer.lowAccessibility != "low"){
			Console.WriteLine("Uploading low area accessibility identifier " + uploadAnalyzer.lowAccessibility);
			GaugeDeviceSensor lssensor = null;
			foreach(var area in uploadAnalyzer.sensorList){
				Console.WriteLine("Looking for a match in the active sensor of the analyzer. area location: " + area.analyzerSlot);
				if(area.analyzerSlot == Convert.ToInt32(uploadAnalyzer.lowAccessibility)){
					lssensor = area as GaugeDeviceSensor;
				}
			}
			layoutJson += "\"low\":\""+uploadAnalyzer.lowAccessibility+"\",\"lsn\":\"" + lssensor.device.serialNumber.rawSerial + "\",\"lsi\":\""  + lssensor.index + "\",\"las\":\"" + lowSecondary + "\", \"lsub\":[";
		}  else {
			layoutJson += "\"low\":\""+uploadAnalyzer.lowAccessibility+"\", \"lsn\":\"null\",\"lsi\":\"0\",\"las\":\"null\", \"lsub\":[";
		}
		///SEND THE LOW SIDE SUBVIEW LIST
		for(int s = 0; s < uploadAnalyzer.lowSubviews.Count;s++){
			layoutJson += "\""+uploadAnalyzer.lowSubviews[s]+"\"";
			if(s < uploadAnalyzer.lowSubviews.Count - 1){
				layoutJson += ",";
			}
		}
		///SEND THE HIGH SIDE SLOT AREA AND ATTACHED SENSOR
		if(uploadAnalyzer.highAccessibility != "high"){
			Console.WriteLine("Uploading high area accessibility identifier " + uploadAnalyzer.highAccessibility);
			GaugeDeviceSensor hssensor = null;
			foreach(var area in uploadAnalyzer.sensorList){
				Console.WriteLine("Looking for a match in the active sensor of the analyzer. area location: " + area.analyzerSlot);
				if(area.analyzerSlot == Convert.ToInt32(uploadAnalyzer.highAccessibility)){
					hssensor = area as GaugeDeviceSensor;
				}
			}
			layoutJson += "],\"high\":\""+uploadAnalyzer.highAccessibility+"\",\"hsn\":\"" + hssensor.device.serialNumber.rawSerial + "\",\"hsi\":\""  + hssensor.index + "\", \"has\":\"" + highSecondary + "\", \"hsub\":[";
		}  else {
			layoutJson += "],\"high\":\""+uploadAnalyzer.highAccessibility+"\", \"hsn\":\"null\",\"hsi\":\"0\",\"has\":\"null\", \"hsub\":[";
		}
		///SEND THE HIGH SIDE SUBVIEW LIST
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
				var primarySensor = manifold.primarySensor as GaugeDeviceSensor;
				layoutJson += "{\"sn\":\""+manifold.primarySensor.name+"\",\"si\":\"" + primarySensor.index + "\",";
				//layoutJson += "\"amount\":\""+manifold.primarySensor.measurement.amount+"\",\"unit\":\""+UnitLookup.GetCode(manifold.primarySensor.unit)+"\",";
				if(manifold.secondarySensor != null && manifold.secondarySensor is GaugeDeviceSensor){
					var deviceSensor = manifold.secondarySensor as GaugeDeviceSensor;
					layoutJson += "\"lsn\":\"" +deviceSensor.device.serialNumber.rawSerial + "\",\"lsi\":\"" + deviceSensor.index + "\",";
				} else {
					layoutJson += "\"lsn\":\"null\",\"lsi\":\"-1\",";
				}
				layoutJson += "\"fl\":\"" + manifold.ptChart.fluid.name + "\",\"fls\":\""; 
	
				layoutJson += (int)manifold.ptChart.state+ "\",";

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
			 
    if (ion.locationManager.lastKnownLocation != null) {			      	
      layoutJson += "],\"alt\":\"" + ion.locationManager.lastKnownLocation.altitude.amount + "\",";
    }
    
    layoutJson += "\"state\":{\"log\":\"" + Convert.ToInt32(ion.dataLogManager.isRecording) + "\"}";
    
		layoutJson += "}";  
		//Console.WriteLine(layoutJson);
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
			//Console.WriteLine(textReponse);
		}  catch (Exception exception){
			Console.WriteLine(exception);
		}
	}
	/// <summary>
	/// Pulls the layout for a selected user and is parsed
	/// to update the workbench,analyzer, and device manager
	/// </summary>
	/// <returns>The layouts.</returns>
	public async Task DownloadLayouts(string ID, int loggingInterval){
		//Console.WriteLine("downloading layout");
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var workbench = ion.currentWorkbench.storedWorkbench;
		var remoteAnalyzer = ion.currentAnalyzer;
		var activeManifolds = new List<string>();
		var activeAnalyzerSensors = new List<string>();
		var viewingID = ID;
		
		try{
				var remoteDManager = ion.deviceManager as RemoteBaseDeviceManager;
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("downloadLayouts", "manager"),          		
              new KeyValuePair<string, string>("userID", viewingID),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(downloadLayoutsUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			//Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var retrieved = response.GetValue("success").ToString();
			
			if(retrieved == "true"){
				/////GRAB THE LAYOUT JSON
				response = response.GetValue("layout") as JObject;
				
				var remoteAltitude = System.Math.Round(Convert.ToDouble(response.GetValue("alt").ToString()),2,MidpointRounding.AwayFromZero);
				var localAltitude = System.Math.Round(ion.locationManager.lastKnownLocation.altitude.amount,2,MidpointRounding.AwayFromZero);
				
				if(remoteAltitude != localAltitude){
						Console.WriteLine("Updating last known location altitude");
						ion.locationManager.AttemptSetLocation(Units.Length.METER.OfScalar(remoteAltitude));
				}
				////////////////////////////SETTING UP DEVICE MANAGER BASED ON REMOTE DATA
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
							for(int i = 0; i < deserializedToken.sensors.Length;i++){
								gDevice.sensors[i].ForceSetMeasurement(new Scalar(UnitLookup.GetUnit(deserializedToken.sensors[i].unit),deserializedToken.sensors[i].measurement));
							}
						if(Convert.ToBoolean(deserializedToken.connected)){							
							updateConnection.Connect();
						}  else {
							updateConnection.Disconnect();
						}
					}  else {
						var newDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, Convert.ToBoolean(deserializedToken.connected)) as GaugeDevice;
						newDevice.SetBatteryRemoteDevice(deserializedToken.battery);
						for(int i = 0; i < deserializedToken.sensors.Length;i++){
							newDevice.sensors[i].ForceSetMeasurement(new Scalar(UnitLookup.GetUnit(deserializedToken.sensors[i].unit),deserializedToken.sensors[i].measurement));
						}
						remoteDManager.Register(newDevice);  
					}  
				}
				////////////////////////////SETTING UP THE ANALYZER BASED ON REMOTE DATA
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
								gDevice.sensors[0].ForceSetMeasurement(new Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
							}  else if (gDevice.sensors.Length > 1 && remoteAnalyzer.sensorList.Contains(gDevice.sensors[1]) && gDevice.sensors[1].type == UnitLookup.GetSensorTypeFromCode(deserializedToken.unit)){
								gDevice.sensors[1].ForceSetMeasurement(new Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
							}  else {
								if(gDevice.serialNumber.rawSerial == deserializedToken.serialNumber){
									gDevice.sensors[deserializedToken.sensorIndex].analyzerSlot = deserializedToken.position;
									gDevice.sensors[deserializedToken.sensorIndex].analyzerArea = deserializedToken.sensorArea;
									remoteAnalyzer.sensorList.Add(gDevice.sensors[deserializedToken.sensorIndex]);
									//if(UnitLookup.GetSensorTypeFromCode(deserializedToken.unit) == ESensorType.Pressure || UnitLookup.GetSensorTypeFromCode(deserializedToken.unit) == ESensorType.Vacuum){
									//	gDevice.sensors[0].analyzerSlot = deserializedToken.position;
									//	//gDevice.sensors[0].analyzerArea = deserializedToken.area;
									//	remoteAnalyzer.sensorList.Add(gDevice.sensors[0]);
									//}
									//else {   
									//	gDevice.sensors[1].analyzerSlot = deserializedToken.position;
									//	//gDevice.sensors[1].analyzerArea = deserializedToken.area;
									//	remoteAnalyzer.sensorList.Add(gDevice.sensors[1]);   
									//}
								}
							}
 						}
						////SET THE SECONDARY SENSOR FOR LOW AND HIGH AREAS (CURRENTLY WILL ONLY BE A TEMPERATURE SENSOR)
						if(deserializedLowHigh.lowAttached != "null"){
	 						if(device.serialNumber.rawSerial == deserializedLowHigh.lowAttached){
								ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,gDevice.sensors[1]);
							}
						}  else {
							ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,null);
						}
						
						if(deserializedLowHigh.highAttached != "null"){
	 						if(device.serialNumber.rawSerial == deserializedLowHigh.highAttached){
								ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,gDevice.sensors[1]);
							}
						}  else {
							ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,null);
						}  
					}  
				}  

				///CHECK LOW SIDE FLUID BEING SENT UP AND CHANGE IF NECESSARY
				if(deserializedPositions.lfluid != "null" && (remoteAnalyzer.lowFluid == null || remoteAnalyzer.lowFluid.name != deserializedPositions.lfluid)){
					//Console.WriteLine("Setting analyzer low fluid to " + deserializedPositions.lfluid);
					//remoteAnalyzer.lowFluid = await ion.fluidManager.LoadFluidAsync(deserializedPositions.lfluid);
					//remoteAnalyzer.lowSideManifold.ptChart = PTChart.New(ion,remoteAnalyzer.lowSideManifold.ptChart.state,remoteAnalyzer.lowFluid);
				}
				///CHECK HIGH SIDE FLUID BEING SENT UP AND CHANGE IF NECESSARY
				if(deserializedPositions.hfluid != "null" && (remoteAnalyzer.highFluid == null || remoteAnalyzer.highFluid.name != deserializedPositions.hfluid)){				
					//Console.WriteLine("Setting analyzer high fluid to " + deserializedPositions.lfluid);
					//remoteAnalyzer.highFluid = await ion.fluidManager.LoadFluidAsync(deserializedPositions.hfluid);
					//remoteAnalyzer.highSideManifold.ptChart = PTChart.New(ion,remoteAnalyzer.highSideManifold.ptChart.state,remoteAnalyzer.highFluid);					
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

				////////////////////////////SETTING UP THE WORKBENCH BASED ON REMOTE DATA			
				var wManager = response.GetValue("workB");

				foreach(var con in wManager){
					var deserializedToken = JsonConvert.DeserializeObject<workbenchSetup>(con.ToString());
					var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var managerDevice = remoteDManager[iserial] as GaugeDevice;
					var managerSensor = managerDevice.sensors[deserializedToken.sensorIndex];

					activeManifolds.Add(deserializedToken.serialNumber+managerSensor.type);

					var existing = workbench.GetDeviceIndex(iserial,deserializedToken.sensorIndex);
					if(existing != -1){
						Manifold updateManifold = workbench.manifolds[existing];  
						
						if(managerSensor.type == updateManifold.primarySensor.type){
							updateManifold.primarySensor.ForceSetMeasurement(new Scalar(managerSensor.unit,managerSensor.measurement.amount));
						} else if (updateManifold.secondarySensor != null && managerSensor.type == updateManifold.secondarySensor.type) {
							updateManifold.secondarySensor.ForceSetMeasurement(new Scalar(managerSensor.unit,managerSensor.measurement.amount));
						}
						
						if(updateManifold.ptChart.fluid.name != deserializedToken.fluidname){

							var	ptstate = (Fluid.EState)deserializedToken.fluidState;

							var manifoldFluid = await ion.fluidManager.LoadFluidAsync(deserializedToken.fluidname);
							updateManifold.ptChart = PTChart.New(ion,ptstate,manifoldFluid);
						}
						
						if(deserializedToken.linkedSerial != "null" ){
							if(updateManifold.secondarySensor != null){
								///get current secondary sensor
								var checkSensor = updateManifold.secondarySensor as GaugeDeviceSensor;
								///check if current secondary is the same linked sensor;
								if(checkSensor.device.serialNumber.rawSerial != deserializedToken.linkedSerial){
									///find the secondary sensor in known devices that matches the linked serial and is the opposite unit type of the parent
									///create an iserial
									var linkedSerial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.linkedSerial);
									var linkedDevice = remoteDManager[linkedSerial] as GaugeDevice;
									var linkedSensor = linkedDevice.sensors[deserializedToken.linkedIndex];
									updateManifold.SetSecondarySensor(linkedSensor);
								}
							} else {
							
									var linkedSerial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.linkedSerial);
									var linkedDevice = remoteDManager[linkedSerial] as GaugeDevice;
									var linkedSensor = linkedDevice.sensors[deserializedToken.linkedIndex];
									updateManifold.SetSecondarySensor(linkedSensor);
							} 
						} else {
							if(updateManifold.secondarySensor != null){
								updateManifold.SetSecondarySensor(null);
							}
						}				
						////////
						/// add any sensor properties that a remote user has added only if they don't exist already
						SetupNewManifoldProperties(updateManifold,deserializedToken);			

						///Remove any subviews a remote user has removed
						foreach(var sp in updateManifold.sensorProperties.ToArray()){
							var propertyIndex = GetSensorPropertyEnum(sp);
							var exists = Array.IndexOf(deserializedToken.subviews,propertyIndex); 
							if(exists == -1){
								updateManifold.RemoveSensorProperty(sp);
							}
						}
					}  else {

						///manifold has been added by the remote user but doesn't exist for viewing user so create it and add it's sensor properties
						var newIdevice = remoteDManager[iserial];
						var manualDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, newIdevice.isConnected) as GaugeDevice;

						var manualManifold = new ION.Core.Content.Manifold(manualDevice.sensors[deserializedToken.sensorIndex]);
						
						var	ptstate = (Fluid.EState)deserializedToken.fluidState;  

						var manifoldFluid = await ion.fluidManager.LoadFluidAsync(deserializedToken.fluidname);
						manualManifold.ptChart = PTChart.New(AppState.context,ptstate,manifoldFluid);
						workbench.Add(manualManifold);
						SetupNewManifoldProperties(manualManifold,deserializedToken);
					}					
				}
				
				foreach(var manifold in workbench.manifolds.ToArray()){
					if(!activeManifolds.Contains(manifold.primarySensor.name+manifold.primarySensor.type)){
						workbench.Remove(manifold);
					}
				}
				///GET THE COMMAND STATE INFORMATION ABOUT A REMOTE DEVICE I.E. DATA LOGGING, WIFI STATUS, DEVICE BATTERY LEVEL
				//var stateManager = response.GetValue("state");
				
				//foreach(var state in stateManager){
				//	var deserializedState = JsonConvert.DeserializeObject<sessionStateInfo>(state.ToString());
					
				//	if(deserializedState.isRecording){
				//		if(!ion.dataLogManager.isRecording){
				//			await ion.dataLogManager.BeginRecording(TimeSpan.FromSeconds(loggingInterval));
				//		}
				//	} else {
				//		await ion.dataLogManager.StopRecording();
				//	}
				//}
				
			}
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
		}
	}

	public async void SetupNewManifoldProperties(Manifold manualManifold, workbenchSetup deserializedToken){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		foreach(var sub in deserializedToken.subviews){
			switch(sub){
				case 1:
					if(!manualManifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))){									
						manualManifold.AddSensorProperty(new PTChartSensorProperty(manualManifold));
					}
					break;
				case 2:
					if(!manualManifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))){
						manualManifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manualManifold));
					}
					break;
				case 3:
					if (!manualManifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
						manualManifold.AddSensorProperty(new MinSensorProperty(manualManifold));
					}
					break;
				case 4:
					if (!manualManifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
						manualManifold.AddSensorProperty(new MaxSensorProperty(manualManifold));
					}
					break;
				case 5:
					if (!manualManifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
						manualManifold.AddSensorProperty(new HoldSensorProperty(manualManifold));
					}
					break;
				case 6:
					if (!manualManifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
						manualManifold.AddSensorProperty(new RateOfChangeSensorProperty(manualManifold));
					}
					break;
				case 7:
					if (!manualManifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
						manualManifold.AddSensorProperty(new TimerSensorProperty(manualManifold));
					}
					break;
				case 8:
		      if (manualManifold.secondarySensor != null) {
		        if (!manualManifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) {
		            manualManifold.AddSensorProperty(new SecondarySensorProperty(manualManifold));
		        }
		      }
					break;
				default:
					Console.WriteLine("Unknown....");
					break;
			}
		}		
	}	
	/// <summary>
	/// Queries for everyone a user has viewing access for
	/// </summary>
	/// <returns>A list of ids associated the user</returns>
	/// <param name="accessList">Access list.</param>
	public async Task<HttpResponseMessage> GetAccessList(string userID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
      {
          new KeyValuePair<string, string>("retrieveAccess", "true"),          		
          new KeyValuePair<string, string>("userID", userID),    
      });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(retrieveAccessUrl,formContent);

			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);  
			return null;
		}			
	}
	/// <summary>
	/// Generates a random access code that will associate users to one another for viewing
	/// </summary>
	/// <returns>The access code.</returns>
	/// <param name="pendingUsers">Pending users.</param>
	public async Task<HttpResponseMessage> GenerateAccessCode(string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = ID;
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
			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			return null;
		}
	}	
	/// <summary>
	/// Submits the access code a user enters to update viewing access associated with the code
	/// </summary>
	/// <param name="codeText">Code text.</param>
	public async Task<HttpResponseMessage> submitAccessCode(string codeText, string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));			

	var userID = ID;
		
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

			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			return null;
		}
	}
	/// <summary>
	/// Queries for all the open requests a user has
	/// </summary>
	/// <returns>A list of request codes and their claimed status</returns>
	/// <param name="pendingUsers">Pending users.</param>
	public async Task<HttpResponseMessage> getAllRequests(string ID){   
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = ID;

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
			
			return feedback;

		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			return null;
		}				
	}
	/// <summary>
	/// Sets a user to be online or offline for uploading/retrieving their data
	/// </summary>
	/// <returns>The online status.</returns>
	/// <param name="status">Status.</param>
	/// <param name="rootVC">Root vc.</param>
	public async Task<HttpResponseMessage> updateOnlineStatus(string status, string ID){
			var userID = ID;

	
			try{
				//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("changeStatus", "true"),          		
              new KeyValuePair<string, string>("userID", userID),
              new KeyValuePair<string, string>("status", status), 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(changeOnlineUrl,formContent);			
	
				return feedback;
			}  catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				return null;
			}				
	}
	
	/// <summary>
	/// User enters an email to recieve their username and a new
	/// password at the entered email address
	/// </summary>
	/// <param name="recoverEmail">Recover email.</param>
	public async Task<HttpResponseMessage> resetPassword(string recoverEmail){
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
			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			return null;
		}
	}
	/// <summary>
	/// Updates the password.
	/// </summary>
	/// <param name="newPword">New pword.</param>
	public async Task<HttpResponseMessage> updatePassword(string newPword, string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = ID;
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

			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);

			return null;
		}			
	}
	
		/// <summary>
	/// Updates the display name.
	/// </summary>
	/// <param name="newPword">New pword.</param>
	public async Task<HttpResponseMessage> updateDisplayName(string firstName, string lastName, string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = ID;
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("updateDisplay", "true"),          		
              new KeyValuePair<string, string>("firstName", firstName), 
              new KeyValuePair<string, string>("lastName", lastName), 
              new KeyValuePair<string, string>("userID", userID) 
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(updateAccountUrl,formContent);

			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);

			return null;
		}			
	}
	/// <summary>
	/// Updates the email.
	/// </summary>
	/// <param name="newEmail">New email.</param>
	public async Task<HttpResponseMessage> updateEmail(string newEmail, string ID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = ID;

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
			return feedback;
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			return null;
		}			
	}
	
	public async Task<HttpResponseMessage> userLogin(string userName, string password){
			//Create the data package to send for the post request
			//Key value pair for post variable check
	    var formContent = new FormUrlEncodedContent(new[]
      {
          new KeyValuePair<string, string>("loginUser", "returning"),          		
          new KeyValuePair<string, string>("uname", userName), 
          new KeyValuePair<string, string>("usrPword", password) 
      });
      
			try{
				//initiate the post request and get the request result in a byte array 
				//byte[] result = webClient.UploadValues(loginUserUrl,data);
				var feedback = await client.PostAsync(loginUserUrl,formContent);
				return feedback;
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				return null;			
			}
	}
	
	/// <summary>
	/// Kicks off the download process for remote viewing and manages it based on if a user quits or not
	/// </summary>
	public async void StartLayoutDownload(string viewingID, string userID, int loggingInterval){
		startedViewing = DateTime.Now;
		while(downloading){
		  var timeDifference = DateTime.Now.Subtract(startedViewing).Minutes;

			if(timeDifference < 360){
				if(!string.IsNullOrEmpty(userID)){
					await DownloadLayouts(viewingID,loggingInterval);
					await Task.Delay(TimeSpan.FromSeconds(1));
				}  else {
					downloading = false;
				}
			}  else {
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
		}  else if (typeof(SuperheatSubcoolSensorProperty).Equals(property)){
			return 2;
		}  else if (typeof(MinSensorProperty).Equals(property)){
			return 3;
		}  else if (typeof(MaxSensorProperty).Equals(property)){
			return 4;
		}  else if (typeof(HoldSensorProperty).Equals(property)){
			return 5;
		}  else if (typeof(RateOfChangeSensorProperty).Equals(property)){
			return 6;
		}  else if (typeof(TimerSensorProperty).Equals(property)){
			return 7;
		}  else {
			return 8;
		}	
	}
	
	public int GetSensorSubviewCode(string property){
		if(property == "Pressure"){
			return 1;
		}  else if (property == "Superheat"){
			return 2;
		}  else if (property == "Minimum"){
			return 3;
		}  else if (property == "Maximum"){
			return 4;
		}  else if (property == "Hold"){
			return 5;
		}  else if (property == "Rate"){
			return 6;
		}  else if (property == "Alternate"){
			return 7;
		}  else {
			return 8;
		}
	}
	public string GetSensorCodeSubview(int subviewCode){
		if(subviewCode == 1){
			return "Pressure";
		}  else if (subviewCode == 2){
			return "Superheat";
		}  else if (subviewCode == 3){
			return "Minimum";
		}  else if (subviewCode == 4){
			return "Maximum";
		}  else if (subviewCode == 5){
			return "Hold";
		}  else if (subviewCode == 6){
			return "Rate";
		}  else if (subviewCode == 7){
			return "Alternate";
		}  else {
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
		[JsonProperty("sa")]
		public int sensorArea { get; set; }		
		[JsonProperty("sl")]
		public int position { get; set; }
		[JsonProperty("sv")]
		public double measurement { get; set; }
		[JsonProperty("su")]
		public int unit { get; set; }
		[JsonProperty("si")]
		public int sensorIndex { get; set; }		
	}
	
	[Preserve(AllMembers = true)]
	public class workbenchSetup {
		public workbenchSetup(){}
		[JsonProperty("sn")]
		public string serialNumber { get; set; }
		[JsonProperty("si")]
		public int sensorIndex { get; set; }
		[JsonProperty("lsn")]
		public string linkedSerial { get; set; }
		[JsonProperty("lsi")]
		public int linkedIndex { get; set; }
		[JsonProperty("fl")]
		public string fluidname { get; set; }	
		[JsonProperty("fls")]
		public int fluidState { get; set; }
		[JsonProperty("sub")]
		public int [] subviews {get;set;}
	}
	
	[Preserve(AllMembers = true)]
	public class analyzerPositions {
		public analyzerPositions(){}
		[JsonProperty("positions")]
		public int [] sensorPositions {get;set;}
		[JsonProperty("lfluid")]
		public string lfluid {get;set;}
		[JsonProperty("hfluid")]
		public string hfluid {get;set;}		
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
	[Preserve(AllMembers = true)]
	public class sessionUploadPackage {
		public sessionUploadPackage(){}
		[JsonProperty("uploadSession")]
		public bool uploadSession {get;set;}
		[JsonProperty("sessionData")]
		public string sessionData {get;set;}
		[JsonProperty("userID")]
		public string userID {get;set;}		
	}
	[Preserve(AllMembers = true)]
	public class sessionState {
		public sessionState(){}
		public sessionStateInfo info {get;set;}
	
	}
	[Preserve(AllMembers = true)]
	public class sessionStateInfo {
		public sessionStateInfo(){}
		[JsonProperty("log")]
		public bool isRecording {get; set;}		
	}
}


