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
	public delegate void webOfflineEvent(string offlineMessage);
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
		public const string accountStatusUrl = "http://portal.appioninc.com/App/setAccountStatus.php";
		public webOfflineEvent timedOut;
		public webPauseEvent paused;
		public sessionStateInfo stateInfo; 
		
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
	
	public async Task<HttpResponseMessage> createSystemLayout(string ID, string uniqueID, string deviceName){
		Console.WriteLine("Going to grab a layout id or create a new one for the unique device id " + uniqueID + " belonging to account " + ID + " and named " + deviceName);
			try{
      ////UPDATE THE DEVICE INFORMATION BEFORE SENDING UP REMOTE STATE INFORMATION
      var platformInfo = ion.GetPlatformInformation();
      
      stateInfo = new sessionStateInfo(){
        batteryLevel = platformInfo.batteryPercentage,
        wifiStatus = platformInfo.wifiConnected,
        isRecording = ion.dataLogManager.isRecording,
        remainingMemory = platformInfo.freeMemory,
      };
    
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("createLayout", "true"),
              new KeyValuePair<string, string>("userID", ID),
              new KeyValuePair<string, string>("deviceID", uniqueID),
              new KeyValuePair<string, string>("deviceName", deviceName),
              new KeyValuePair<string, string>("status", JsonConvert.SerializeObject(stateInfo)),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(uploadLayoutsUrl,formContent);
			
			//var textReponse = await feedback.Content.ReadAsStringAsync();
			//Console.WriteLine(textReponse);
			return feedback;
		}  catch (Exception exception){
			Console.WriteLine(exception);
			return null;
		}		
	}
	
	/// <summary>
	/// Uploads the devices a user has connected and their relative layout for
	/// the workbench and analyzer
	/// </summary>
	/// <returns>The system layout.</returns>
	public async Task<HttpResponseMessage> uploadSystemLayout(string ID, string layoutID){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var uploadAnalyzer = ion.currentAnalyzer;
		var uploadWorkbench = ion.currentWorkbench;
		var uploadDeviceManager = ion.deviceManager;
		
		var userID = ID;
		
		//////KNOWN DEVICES PACKAGE
		connectedData[] knownArray = new connectedData[uploadDeviceManager.knownDevices.Count];
		
		for(int k = 0; k < uploadDeviceManager.knownDevices.Count; k++){
			var g2Device = uploadDeviceManager.knownDevices[k] as GaugeDevice;
			
			knownArray[k] = new connectedData(){
				battery = uploadDeviceManager.knownDevices[k].battery,
				serialNumber = uploadDeviceManager.knownDevices[k].serialNumber.rawSerial,
				connected = Convert.ToInt32(uploadDeviceManager.knownDevices[k].isConnected),
			};
			
			knownArray[k].sensors = new sensorData[g2Device.sensorCount];
			for(int s = 0; s < g2Device.sensorCount; s++){
				knownArray[k].sensors[s] = new sensorData(){
					measurement = g2Device.sensors[s].measurement.amount,
					sensorIndex = g2Device.sensors[s].index,
					unit = UnitLookup.GetCode(g2Device.sensors[s].unit),
				};
			}			
		}
		  
	  ///Package the analyzer layout
		analyzerSetup[] analyzerArray = new analyzerSetup[uploadAnalyzer.sensorList.Count];

		for(int a = 0; a < uploadAnalyzer.sensorList.Count; a++){
			var gaugeSensor2 = uploadAnalyzer.sensorList[a] as GaugeDeviceSensor;
			analyzerArray[a] = new analyzerSetup(){
				serialNumber = uploadAnalyzer.sensorList[a].name,
				sensorArea = uploadAnalyzer.sensorList[a].analyzerArea,
				position = uploadAnalyzer.sensorList[a].analyzerSlot,
				measurement = uploadAnalyzer.sensorList[a].measurement.amount,
				unit = UnitLookup.GetCode(uploadAnalyzer.sensorList[a].unit),
				sensorIndex = gaugeSensor2.index,
			};
		}

		string lowAttachedSN= "null";
		int lowAttachedIndex = 0;
		string highAttachedSN = "null";
		int highAttachedIndex = 0;

		/////PACKAGE THE ANALYZER SETUP
		analyzerPositions analyzerSetup = new analyzerPositions(){lfluid = "null", hfluid = "null"};
		analyzerSetup.sensorPositions = uploadAnalyzer.sensorPositions.ToArray();
		if(uploadAnalyzer.lowFluid != null){
			analyzerSetup.lfluid = uploadAnalyzer.lowFluid.name;
		}
		if(uploadAnalyzer.highFluid != null){
			analyzerSetup.hfluid = uploadAnalyzer.highFluid.name;
		}
				
		////GET LOW SIDE ATTACHED SENSOR
		if(uploadAnalyzer.lowSideManifold != null && uploadAnalyzer.lowSideManifold.secondarySensor is GaugeDeviceSensor){
			var attachedGaugeSensor = uploadAnalyzer.lowSideManifold.secondarySensor as GaugeDeviceSensor;
			lowAttachedSN = attachedGaugeSensor.device.serialNumber.rawSerial;
			lowAttachedIndex = attachedGaugeSensor.index;
		}
		////GET HIGH SIDE ATTACHED SENSOR
		if(uploadAnalyzer.highSideManifold != null && uploadAnalyzer.highSideManifold.secondarySensor is GaugeDeviceSensor){
			var attachedGaugeSensor = uploadAnalyzer.highSideManifold.secondarySensor as GaugeDeviceSensor;
			highAttachedSN = attachedGaugeSensor.device.serialNumber.rawSerial;
			highAttachedIndex = attachedGaugeSensor.index;
		}		

		//////PACKAGE LOW AND HIGH SIDE INFORMATION
		analyzerLowHigh lowhighArea = new analyzerLowHigh(){
			lowAttached = lowAttachedSN,
			lowAttachedIndex = lowAttachedIndex,
			lowSerialNumber = "null",
			lowSerialIndex = 0,
			highAttached = highAttachedSN,
			highAttachedIndex = highAttachedIndex,
			highSerialNumber = "null",
			highSerialIndex = 0,
			lowAccessibility = uploadAnalyzer.lowAccessibility,
			highAccessibility = uploadAnalyzer.highAccessibility,
			lowSubviews = uploadAnalyzer.lowSubviews.ToArray(),		
			highSubviews = uploadAnalyzer.highSubviews.ToArray(),		
		};
		//////SET LOW SIDE ATTACHED SENSOR		
		if(uploadAnalyzer.lowAccessibility != "low"){
			GaugeDeviceSensor lssensor = null;
			foreach(var area in uploadAnalyzer.sensorList){
				//Console.WriteLine("Looking for a match in the active sensor of the analyzer. area location: " + area.analyzerSlot);
				if(area.analyzerSlot == Convert.ToInt32(uploadAnalyzer.lowAccessibility)){
					lssensor = area as GaugeDeviceSensor;
					break;
				}
			}			
			if(lssensor != null){
				lowhighArea.lowSerialNumber = lssensor.device.serialNumber.rawSerial;
				lowhighArea.lowSerialIndex = lssensor.index;
			}
		}		

		//////SET HIGH SIDE ATTACHED SENSOR		
		if(uploadAnalyzer.highAccessibility != "high"){
			GaugeDeviceSensor lssensor = null;
			foreach(var area in uploadAnalyzer.sensorList){
				//Console.WriteLine("Looking for a match in the active sensor of the analyzer. area location: " + area.analyzerSlot);
				if(area.analyzerSlot == Convert.ToInt32(uploadAnalyzer.highAccessibility)){
					lssensor = area as GaugeDeviceSensor;
					break;
				}
			}			
			if(lssensor != null){
				lowhighArea.highSerialNumber = lssensor.device.serialNumber.rawSerial;
				lowhighArea.highSerialIndex = lssensor.index;
			}
		}		
		
		///PACKAGE THE WORKBENCH LAYOUT
		workbenchSetup[] workbenchArray = new workbenchSetup[uploadWorkbench.manifolds.Count];
		
		for(int w = 0; w < uploadWorkbench.manifolds.Count; w++){
			var primarySensor = uploadWorkbench.manifolds[w].primarySensor as GaugeDeviceSensor;
	
			workbenchArray[w] = new workbenchSetup(){
				serialNumber = primarySensor.name,
				sensorIndex = primarySensor.index,
				fluidname = uploadWorkbench.manifolds[w].ptChart.fluid.name,
				fluidState = (int)uploadWorkbench.manifolds[w].ptChart.state,
				linkedSerial = "null",
				linkedIndex = -1,
				subviews = new int[uploadWorkbench.manifolds[w].sensorProperties.Count],
			};
			
			if(uploadWorkbench.manifolds[w].secondarySensor != null && uploadWorkbench.manifolds[w].secondarySensor is GaugeDeviceSensor){
				var deviceSensor = uploadWorkbench.manifolds[w].secondarySensor as GaugeDeviceSensor;
				workbenchArray[w].linkedSerial = deviceSensor.device.serialNumber.rawSerial;
				workbenchArray[w].linkedIndex = deviceSensor.index;
			}
			
			for(int s = 0; s < uploadWorkbench.manifolds[w].sensorProperties.Count; s++){
				var sensorEnum = GetSensorPropertyEnum(uploadWorkbench.manifolds[w].sensorProperties[s]);
				workbenchArray[w].subviews[s] = sensorEnum;
			}			
		}
		/////BIND EACH SECTION TOGETHER FOR SERIALIZING
		boundLayout completedLayout = new boundLayout(){
			known = knownArray,
			analyzer = analyzerArray,
			Setup = analyzerSetup,
			lowhigh = lowhighArea,
			workbench = workbenchArray,
			altitude = ion.locationManager.lastKnownLocation.altitude.amount
		};
		//Console.WriteLine(JsonConvert.SerializeObject(completedLayout));
		////UPDATE THE DEVICE INFORMATION BEFORE SENDING UP REMOTE STATE INFORMATION
		var platformInfo = ion.GetPlatformInformation();
		
		stateInfo = new sessionStateInfo(){
			batteryLevel = platformInfo.batteryPercentage,
			wifiStatus = platformInfo.wifiConnected,
			isRecording = ion.dataLogManager.isRecording,
			remainingMemory = platformInfo.freeMemory,
		};
		
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("uploadLayouts", "manager"),
              new KeyValuePair<string, string>("layoutJson", JsonConvert.SerializeObject(completedLayout)),
              new KeyValuePair<string, string>("userID", userID),
              new KeyValuePair<string, string>("layoutID", layoutID),
              new KeyValuePair<string, string>("status", JsonConvert.SerializeObject(stateInfo)),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(uploadLayoutsUrl,formContent);

			//var textReponse = await feedback.Content.ReadAsStringAsync();
			//Console.WriteLine(textReponse);
			return feedback;
		}  catch (Exception exception){
			Console.WriteLine(exception);
			return null;
		}
	}
	/// <summary>
	/// Pulls the layout for a selected user and is parsed
	/// to update the workbench,analyzer, and device manager
	/// </summary>
	/// <returns>The layouts.</returns>
	public async Task DownloadLayouts(string ID, int loggingInterval, string viewingLayout){
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
              new KeyValuePair<string, string>("layoutid", viewingLayout),
          });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(downloadLayoutsUrl,formContent);

			var textResponse = await feedback.Content.ReadAsStringAsync();
			//Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var retrieved = response.GetValue("success").ToString();
						
			if(retrieved == "true"){
				///GET THE COMMAND STATE INFORMATION ABOUT A REMOTE DEVICE I.E. DATA LOGGING, WIFI STATUS, DEVICE BATTERY LEVEL				
				var remoteStatus = JsonConvert.DeserializeObject<sessionStateInfo>(response.GetValue("status").ToString());
				ion.SetRemotePlatformInformation(remoteStatus);

				/////GRAB THE LAYOUT JSON
				response = JObject.Parse(response.GetValue("layout").ToString());
			
				var remoteAltitude = System.Math.Round(Convert.ToDouble(response.GetValue("alt").ToString()),2,MidpointRounding.AwayFromZero);
				var localAltitude = System.Math.Round(ion.locationManager.lastKnownLocation.altitude.amount,2,MidpointRounding.AwayFromZero);
				
				if(remoteAltitude != localAltitude){
						Console.WriteLine("Updating last known location altitude");
						ion.locationManager.AttemptSetLocation(Units.Length.METER.OfScalar(remoteAltitude)); 
				}
				
				var dManager = response.GetValue("known");
				var aManager = response.GetValue("alyzer");
				var sensorOrder = response.GetValue("setup");
				var aLowHigh = response.GetValue("LH");  
				var deserializedPositions = JsonConvert.DeserializeObject<analyzerPositions>(sensorOrder.ToString());
				var deserializedLowHigh = JsonConvert.DeserializeObject<analyzerLowHigh>(aLowHigh.ToString());				
				
				////////////////////////////SETTING UP DEVICE MANAGER BASED ON REMOTE DATA
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
				
				//Console.WriteLine("Low attached: " + deserializedLowHigh.lowAttached);
				//Console.WriteLine("High attached: " + deserializedLowHigh.highAttached);
				//////SET THE LOW MANIFOLD BASED ON THE REMOTE DATA
				if(deserializedLowHigh.lowSerialNumber != "null"){
					if(ion.currentAnalyzer.lowSideManifold == null || ion.currentAnalyzer.lowSideManifold.primarySensor.name != deserializedLowHigh.lowSerialNumber){
						var lowISerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.lowSerialNumber);
						var lowDevice = remoteDManager[lowISerial] as GaugeDevice;
						ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,lowDevice.sensors[deserializedLowHigh.lowSerialIndex], ion.fluidManager.LoadFluidAsync(deserializedPositions.lfluid).Result);
					}
					////SET THE SECONDARY SENSOR FOR LOW AREA (CURRENTLY WILL ONLY BE A TEMPERATURE SENSOR)
 
					if(deserializedLowHigh.lowAttached != "null"){
						////LOW SIDE HAS A SECONDARY SENSOR ALREADY
						if(ion.currentAnalyzer.lowSideManifold.secondarySensor != null ){
							////CHECK IF LOW SIDE SECONDARY SENSOR HAS CHANGED
							if(ion.currentAnalyzer.lowSideManifold.secondarySensor.name != deserializedLowHigh.lowAttached){
								var lowASerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.lowAttached);
								var lowADevice = remoteDManager[lowASerial] as GaugeDevice;							
								ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(lowADevice.sensors[deserializedLowHigh.lowAttachedIndex]);
								Console.WriteLine("Set low secondary sensor to " + lowADevice.serialNumber.rawSerial);
							}
						}
						/////LOW SIDE DOESN'T HAVE A SECONDARY SENSOR YET. AUTOMATICALLY ADD IT
						else { 
							var lowASerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.lowAttached);
							var lowADevice = remoteDManager[lowASerial] as GaugeDevice;
							ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(lowADevice.sensors[deserializedLowHigh.lowAttachedIndex]);
								Console.WriteLine("Unset Set low secondary sensor to " + lowADevice.serialNumber.rawSerial);
						}
					}
				} else {
					ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.Low,null,null);
				}
				
				//////SET THE HIGH MANIFOLD BASED ON THE REMOTE DATA
				if(deserializedLowHigh.highSerialNumber != "null"){
					////HIGH SIDE MANIFOLD IS EMPTY OR DOESN'T MATCH THE REMOTE SERIAL NUMBER
					if(ion.currentAnalyzer.highSideManifold == null || ion.currentAnalyzer.highSideManifold.primarySensor.name != deserializedLowHigh.highSerialNumber){
						var highISerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.highSerialNumber);
						var highDevice = remoteDManager[highISerial] as GaugeDevice;
						ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,highDevice.sensors[deserializedLowHigh.highSerialIndex], ion.fluidManager.LoadFluidAsync(deserializedPositions.hfluid).Result);
					}
					////SET THE SECONDARY SENSOR FOR HIGH AREA (CURRENTLY WILL ONLY BE A TEMPERATURE SENSOR)
					if(deserializedLowHigh.highAttached != "null"){
						////HIGH SIDE HAS A SECONDARY SENSOR ALREADY
						if(ion.currentAnalyzer.highSideManifold.secondarySensor != null){
							////CHECK IF HIGH SIDE SECONDARY SENSOR HAS CHANGED
							if(ion.currentAnalyzer.highSideManifold.secondarySensor.name != deserializedLowHigh.highAttached){
								var highASerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.highAttached);
								var highADevice = remoteDManager[highASerial] as GaugeDevice;
								ion.currentAnalyzer.highSideManifold.SetSecondarySensor(highADevice.sensors[deserializedLowHigh.highAttachedIndex]);
								Console.WriteLine("Set high secondary sensor to " + highADevice.serialNumber.rawSerial);
							}
						}
						/////HIGH SIDE DOESN'T HAVE A SECONDARY SENSOR YET. AUTOMATICALLY ADD IT
						else {
								var highASerial = SerialNumberExtensions.ParseSerialNumber(deserializedLowHigh.highAttached);
								var highADevice = remoteDManager[highASerial] as GaugeDevice;
								ion.currentAnalyzer.highSideManifold.SetSecondarySensor(highADevice.sensors[deserializedLowHigh.highAttachedIndex]);
								Console.WriteLine("Set high secondary sensor to " + highADevice.serialNumber.rawSerial);
						}
					}
				} else {
					if(ion.currentAnalyzer.highSideManifold != null){
						ion.currentAnalyzer.SetRemoteManifold(Analyzer.ESide.High,null,null);
					}
				}
				
				////////////////////////////SETTING UP THE ANALYZER BASED ON REMOTE DATA
				foreach (var con in aManager) {
					var deserializedToken = JsonConvert.DeserializeObject<analyzerSetup>(con.ToString());
					
					//////GET THE DEVICE(AND SENSORS) FROM THE DEVICE MANAGER BASED ON THE SERIAL NUMBER
					var aISerial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var device = remoteDManager[aISerial] as GaugeDevice;
					
					var gDevice = device as GaugeDevice;
					/////ADD IT TO THE LIST OF SENSORS ON THE ANALYZER
					activeAnalyzerSensors.Add(deserializedToken.serialNumber+UnitLookup.GetSensorTypeFromCode(deserializedToken.unit));
					
					/////ADD ANY SENSORS TO THE MANAGING LIST THAT ARE NOT ALREADY THERE
					if(!remoteAnalyzer.sensorList.Contains(gDevice.sensors[deserializedToken.sensorIndex])){
						gDevice.sensors[deserializedToken.sensorIndex].analyzerSlot = deserializedToken.position;
						remoteAnalyzer.sensorList.Add(gDevice.sensors[deserializedToken.sensorIndex]);
					}
				}
				
				//////SET POSITIONS FOR ANALYZER SETUP AND LOW HIGH AREA
				remoteAnalyzer.sensorPositions = new List<int>(deserializedPositions.sensorPositions);
				remoteAnalyzer.revertPositions = new List<int>(deserializedPositions.sensorPositions);
				remoteAnalyzer.lowAccessibility = deserializedLowHigh.lowAccessibility;
				remoteAnalyzer.lowSubviews = new List<string>(deserializedLowHigh.lowSubviews);
				remoteAnalyzer.highAccessibility = deserializedLowHigh.highAccessibility;
				remoteAnalyzer.highSubviews = new List<string>(deserializedLowHigh.highSubviews);
				
				////REMOVE ANY SENSORS FROM THE ANALYZER THAT THE REMOTE DEVICE HAS REMOVED
				foreach(var aSensor in remoteAnalyzer.sensorList.ToArray()){
					if(!activeAnalyzerSensors.Contains(aSensor.name+aSensor.type)){
						remoteAnalyzer.sensorList.Remove(aSensor);
						Console.WriteLine("Removed a sensor from analyzer list");
					}
				}

				if(deserializedLowHigh.lowAttached == "null"){
					////SECONDARY SENSOR SHOULD BE SET TO NULL IF IT ISN'T
					if(ion.currentAnalyzer.lowSideManifold != null && ion.currentAnalyzer.lowSideManifold.secondarySensor != null){
						Console.WriteLine("Set low secondary sensor to null");
						ion.currentAnalyzer.lowSideManifold.SetSecondarySensor(null);
					}
				}
				if(deserializedLowHigh.highAttached == "null"){
					////SECONDARY SENSOR SHOULD BE SET TO NULL IF IT ISN'T
					if(ion.currentAnalyzer.highSideManifold != null && ion.currentAnalyzer.highSideManifold.secondarySensor != null){
						Console.WriteLine("Set high secondary sensor to null");
						ion.currentAnalyzer.highSideManifold.SetSecondarySensor(null);
					}
				}
				////////////////////////////SETTING UP THE WORKBENCH BASED ON REMOTE DATA			
				var wManager = response.GetValue("workB");  
				if(workbench != null){
					foreach(var con in wManager){
						var deserializedToken = JsonConvert.DeserializeObject<workbenchSetup>(con.ToString());
						var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
						var managerDevice = remoteDManager[iserial] as GaugeDevice;
						var managerSensor = managerDevice.sensors[deserializedToken.sensorIndex];
	
						activeManifolds.Add(deserializedToken.serialNumber+managerSensor.type);
	
						var existing = workbench.GetDeviceIndex(iserial,deserializedToken.sensorIndex);
						if(existing != -1){
							Manifold updateManifold = workbench.manifolds[existing];  
							
							//if(managerSensor.type == updateManifold.primarySensor.type){
							//	updateManifold.primarySensor.ForceSetMeasurement(new Scalar(managerSensor.unit,managerSensor.measurement.amount));
							//} else if (updateManifold.secondarySensor != null && managerSensor.type == updateManifold.secondarySensor.type) {
							//	updateManifold.secondarySensor.ForceSetMeasurement(new Scalar(managerSensor.unit,managerSensor.measurement.amount));
							//}
							
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
					///REMOVE ANY WORKBENCH MANIFOLDS THAT ARE NOT IN THE REMOTE LIST
					foreach(var manifold in workbench.manifolds.ToArray()){
						if(!activeManifolds.Contains(manifold.primarySensor.name+manifold.primarySensor.type)){
							workbench.Remove(manifold);
						}
					}
				}
			} else {
				var message = response.GetValue("message").ToString();
				Console.WriteLine("The respone message is:" + message);
				if(downloading){
					timedOut(message);
					timedOut = null;
				}
			}
			
		}  catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
		}
		//Console.WriteLine("Finished layout download");
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
            manualManifold.AddSensorProperty(new RateOfChangeSensorProperty(manualManifold, ion.preferences.device.rateOfChangeInterval));
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
	
	public async Task<HttpResponseMessage> SetRemoteDataLog(string viewedUser, string viewedLayout, string islogging){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
      {
          new KeyValuePair<string, string>("updateLogging", "true"),          		
          new KeyValuePair<string, string>("logStatus", islogging),          		
          new KeyValuePair<string, string>("viewedUser", viewedUser),    
          new KeyValuePair<string, string>("viewedLayout", viewedLayout),    
      });
          
			//////initiate the post request and get the request result
			var feedback = await client.PostAsync(accountStatusUrl,formContent);

			return feedback;
		}  catch (Exception exception){  
			Console.WriteLine("Exception: " + exception);  
			return null;
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
	public async Task<HttpResponseMessage> updateOnlineStatus(string ID, string layoutID){
			var userID = ID;
	
			try{
				//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("changeStatus", "true"),          		
              new KeyValuePair<string, string>("userID", userID),
              new KeyValuePair<string, string>("layoutID", layoutID), 
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
	public async void StartLayoutDownload(string viewingID, string userID, int loggingInterval, string viewingLayout){
		//startedViewing = DateTime.Now;
		while(downloading){
		  //var timeDifference = DateTime.Now.Subtract(startedViewing).Minutes;

			if(!string.IsNullOrEmpty(userID)){
				await DownloadLayouts(viewingID,loggingInterval,viewingLayout);
				await Task.Delay(TimeSpan.FromSeconds(1));
			}  else {
				downloading = false;
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
		[JsonProperty("lai")]
		public int lowAttachedIndex {get;set;}		
		[JsonProperty("lsn")]
		public string lowSerialNumber {get;set;}
		[JsonProperty("lsi")]
		public int lowSerialIndex {get;set;}		
		[JsonProperty("has")]
		public string highAttached {get;set;}
		[JsonProperty("hai")]
		public int highAttachedIndex {get;set;}		
		[JsonProperty("hsn")]
		public string highSerialNumber {get;set;}
		[JsonProperty("hsi")]
		public int highSerialIndex {get;set;}		
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
	public class boundLayout{
		public boundLayout(){}
		[JsonProperty("known")]
		public connectedData [] known {get;set;}
		[JsonProperty("alyzer")]
		public analyzerSetup[] analyzer {get;set;}
		[JsonProperty("setup")]
		public analyzerPositions Setup {get;set;}
		[JsonProperty("LH")]
		public analyzerLowHigh lowhigh {get;set;}
		[JsonProperty("workB")]
		public workbenchSetup[] workbench {get;set;}
		[JsonProperty("alt")]
		public double altitude {get;set;}
	}
}


