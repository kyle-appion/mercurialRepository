﻿using System;
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

namespace ION.Core.Net {
	public sealed class PreserveAttribute : System.Attribute 
	{
	    public bool AllMembers;
	    public bool Conditional;
	}

	public class WebPayload {
		public IION ion;
		public WebClient webClient;
		HttpClient client;
		public const string uploadSessionUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/uploadSession.php";
		public const string downloadSessionUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/downloadSession.php";
		public const string registerUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/registerUser.php";
		public const string submitCodeUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/submitAccessCode.php";
		public const string confirmAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/confirmAccess.php";
		public const string retrieveAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/retrieveAccess.php";
		public const string createAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/requestAccess.php";
		public const string getRequestsUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/getRequests.php";
		public const string changeOnlineUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/changeOnlineStatus.php";
		public const string uploadLayoutsUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/uploadLayouts.php";
		public const string downloadLayoutsUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/downloadLayouts.php";
		public const string forgotAccountUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/forgotUserPass.php";
		public const string updateAccountUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/updateAccount.php";
	
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
		public void getSession(List<int> sessionList){
			
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
				jsonPayload += "\"start\":\""+session.sessionStart.ToLocalTime()+"\",";
				jsonPayload += "\"end\":\""+session.sessionEnd.ToLocalTime()+"\",";
				jsonPayload += "\"measurements\":[";
				var count2 = 1;
				if(measurementResult.Count > 0){					
					foreach(var entry in measurementResult){
						jsonPayload += "{\"measurement\":\""+entry.measurement + "\",";
						jsonPayload += "\"recorded\":\"" +entry.recordedDate.ToLocalTime().ToString() + "\",";
						jsonPayload += "\"sensorIndex\":\"" +entry.sensorIndex + "\",";
						jsonPayload += "\"serialNumber\":\"" +entry.serialNumber + "\"}";
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
    public async Task<bool> RegisterUser(string userName, string password, string displayName, string email){
    	await Task.Delay(TimeSpan.FromMilliseconds(1)); 
    	
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;	
		
			try{			
				//Create the data package to send for the post request
				//Key value pair for post variable check
	      var formContent = new FormUrlEncodedContent(new[]
	          {
	              new KeyValuePair<string, string>("registerUser", "newuser"),          		
	              new KeyValuePair<string, string>("uname", userName), 
	              new KeyValuePair<string, string>("usrpword", password), 
	              new KeyValuePair<string, string>("usrEmail", email), 
	              new KeyValuePair<string, string>("displayName", displayName), 
	          });
	          
				//////initiate the post request and get the request result
				var feedback = await client.PostAsync(registerUserUrl,formContent);
	
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var isregistered = response.GetValue("success").ToString();

				if(isregistered == "true"){
					return true;
				} else {
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
				layoutJson += "{\"serial\":\""+gDevice.serialNumber + "\",\"amount\":\"" + gDevice.sensors[0].measurement.amount + "\",\"unit\":\""+UnitLookup.GetCode(gDevice.sensors[0].unit)+"\", \"on\":\""+Convert.ToInt32(device.isConnected)+"\"}";
				connectedCount++;
			}
		}
		layoutJson += "],";
	  ///Package the analyzer layout
		layoutJson += "\"alyzer\":[";
		if(uploadAnalyzer != null && uploadAnalyzer.sensorList != null){			
			var count = 1;                                                                    
			foreach(var sensor in uploadAnalyzer.sensorList){			 	
				var position = sensor.analyzerSlot;
			 	layoutJson += "{\"serial\":\""+sensor.name+"\",\"slot\":\""+position+"\",\"amount\":\""+sensor.measurement.amount+"\",\"unit\":\""+UnitLookup.GetCode(sensor.unit)+"\"}";
				if(count < uploadAnalyzer.sensorList.Count){
					layoutJson += ",";
				}
				count++; 
			}		
		}		
		layoutJson += "],";
		///Package the workbench layout
		layoutJson += "\"workB\" : [";
		if(uploadWorkbench != null && uploadWorkbench.manifolds != null){

			var count = 1;  
			foreach(var manifold in uploadWorkbench.manifolds){
				layoutJson += "{\"serial\":\""+manifold.primarySensor.name+"\",\"amount\":\""+manifold.primarySensor.measurement.amount+"\",\"unit\":\""+UnitLookup.GetCode(manifold.primarySensor.unit)+"\",}";
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
			//Console.WriteLine("uploaded layout for userID " + userID + " as " + layoutJson);		
		} catch (Exception exception){
			Console.WriteLine(exception);
		}
	}
	/// <summary>
	/// Pulls the layout for a selected user and is parsed
	/// to update the workbench,analyzer, and device manager
	/// </summary>
	/// <returns>The layouts.</returns>
	public async Task DownloadLayouts(Workbench workbench){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		ion = AppState.context;
		
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
				Console.WriteLine("CONNECTED DEVICES");

				foreach (var con in dManager) {

					var deserializedToken = JsonConvert.DeserializeObject<connectedData>(con.ToString());
					var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var manualDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, Convert.ToBoolean(deserializedToken.connected));
					if(remoteDManager.knownDevices.Contains(manualDevice)){
							var updateConnection = remoteDManager[iserial].connection as RemoteConnection;						
						if(Convert.ToBoolean(deserializedToken.connected)){							
							updateConnection.Connect();
						} else {
							updateConnection.Disconnect();
						}				
					} else {
						var newDevice = remoteDManager.CreateDevice(iserial, ION.Core.Devices.Protocols.EProtocolVersion.V4, Convert.ToBoolean(deserializedToken.connected)) as GaugeDevice;
						newDevice.sensors[0].RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
	
						remoteDManager.Register(newDevice);
					}

					Console.WriteLine(deserializedToken.serialNumber + " - " + deserializedToken.measurement + " - " + deserializedToken.unit); 
				} 

				var aManager = response.GetValue("alyzer");
				Console.WriteLine("ANALYZER");
				foreach (var con in aManager) { 
					var deserializedToken = JsonConvert.DeserializeObject<analyzerSetup>(con.ToString());
					Console.WriteLine(deserializedToken.serialNumber + " - " + deserializedToken.position + " - " + deserializedToken.measurement + " - " + deserializedToken.unit);
				}
				var wManager = response.GetValue("workB");
				Console.WriteLine("WORKBENCH");
				
				foreach(var con in wManager){
						var deserializedToken = JsonConvert.DeserializeObject<workbenchSetup>(con.ToString());
						var iserial = SerialNumberExtensions.ParseSerialNumber(deserializedToken.serialNumber);
					var wbconnected = false;
					foreach(var idevice in remoteDManager.knownDevices){
						if(idevice.serialNumber.rawSerial == iserial.rawSerial){
							if(idevice.isConnected){
								wbconnected = true;
							}
							break;
						}
					}					
					var manualDevice = remoteDManager.CreateDevice(iserial,ION.Core.Devices.Protocols.EProtocolVersion.V4, wbconnected) as GaugeDevice;
					
					var existing = workbench.GetDeviceIndex(manualDevice.serialNumber);

					if(existing != 99){
							var updateManifold = workbench.manifolds[existing];							
							updateManifold.primarySensor.RemoteForceSetMeasurement(new Measure.Scalar(UnitLookup.GetUnit(deserializedToken.unit),deserializedToken.measurement));
					} else {
						var manualManifold = new ION.Core.Content.Manifold(manualDevice.sensors[0]);	
						workbench.Add(manualManifold);
					}
					Console.WriteLine(deserializedToken.serialNumber + " - " + deserializedToken.measurement + " - " + deserializedToken.unit);
				}
			}
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
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
	public async Task<bool> updateOnlineStatus(string status, IONPrimaryScreenController rootVC){

		return await Task.Factory.StartNew(() => {

			var userID = KeychainAccess.ValueForKey("userID");
			Console.WriteLine("Getting all request for id " + userID);
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
					if(rootVC != null){
						var errorMessage = response.GetValue("message").ToString();
						
						var alert = UIAlertController.Create ("Online Status", errorMessage, UIAlertControllerStyle.Alert);
						alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
						rootVC.PresentViewController (alert, animated: true, completionHandler: null);
					}
					return false;
				}			
	
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				//if(rootVC != null){				
				//	var alert = UIAlertController.Create ("Online Status", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				//	alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				//	rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				//}
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
	/// Updates the display name.
	/// </summary>
	/// <param name="newDName">New DN ame.</param>
	public async void updateDisplayName(string newDName, UITextField dField){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var userID = KeychainAccess.ValueForKey("userID");
		try{
			//Create the data package to send for the post request
			//Key value pair for post variable check
      var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("updateDisplay", "new"),          		
              new KeyValuePair<string, string>("newDisplay", newDName), 
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
				dField.Text = newDName;
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
	public async void DeleteAnalyzerLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
	}

	public async void DeleteWorkbenchLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));		
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
		[JsonProperty("serial")]
		public string serialNumber { get; set; }
		[JsonProperty("amount")]
		public double measurement { get; set; }
		[JsonProperty("unit")]
		public int unit { get; set; }
		[JsonProperty("on")]
		public int connected { get; set; }	
	}	
		
	[Preserve(AllMembers = true)]
	public class analyzerSetup {
		public analyzerSetup(){}
		[JsonProperty("serial")]
		public string serialNumber { get; set; }
		[JsonProperty("slot")]
		public int position { get; set; }
		[JsonProperty("amount")]
		public double measurement { get; set; }
		[JsonProperty("unit")]
		public int unit { get; set; }
	}
	
	[Preserve(AllMembers = true)]
	public class workbenchSetup {
		public workbenchSetup(){}
		[JsonProperty("serial")]
		public string serialNumber { get; set; }
		[JsonProperty("amount")]
		public double measurement { get; set; }
		[JsonProperty("unit")]
		public int unit { get; set; }
	}
}

