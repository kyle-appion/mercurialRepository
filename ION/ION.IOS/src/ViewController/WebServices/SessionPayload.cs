using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace ION.IOS.ViewController.WebServices {
	public sealed class PreserveAttribute : System.Attribute 
	{
	    public bool AllMembers;
	    public bool Conditional;
	}

	public class SessionPayload {
		public IION ion;
		public WebClient webClient;
		public const string uploadSessionUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/uploadSession.php";
		public const string downloadSessionUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/downloadSession.php";
		public const string registerUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/registerUser.php";
		public const string registerAccountUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/registerAccount.php";
		public const string uploadAnalyzerUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/uploadAnalyzer.php";
		public const string uploadWorkbenchUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/uploadWorkbench.php";
		public const string downloadAnalyzerUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/downloadAnalyzer.php";
		public const string downloadWorkbenchUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/downloadWorkbench.php";
		public const string submitCodeUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/submitAccessCode.php";
		public const string confirmAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/confirmAccess.php";
		public const string retrieveAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/retrieveAccess.php";
		public const string createAccessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/requestAccess.php";
		public const string getRequestsUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/getRequests.php";
		public const string changeOnlineUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/changeOnlineStatus.php";
	
		public SessionPayload() {
			ion = AppState.context;
			webClient = new WebClient();
			webClient.Proxy = null;
			/************create a new session payload
			var package = new ION.IOS.ViewController.WebServices.SessionPayload();
			*********************/
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
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("uploadSession","true");
			data.Add("sessionData",json);
			data.Add("userID",userID);
			try{		
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(uploadSessionUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
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
    public async void RegisterUser(string userName, string password, string displayName, string email){
    	await Task.Delay(TimeSpan.FromMilliseconds(1)); 
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
	
			data.Add("registerUser","newuser");
			data.Add("uname",userName);
			data.Add("usrpword",password);
			data.Add("usrEmail",email);
			data.Add("displayName",displayName);
			try{			
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(registerUserUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var isregistered = response.GetValue("registered").ToString();
				var registeredValue = response.GetValue("userID").ToString();
				if(isregistered == "true"){
					Console.WriteLine("Created a new user with id: " + registeredValue);
				} else if (isregistered == "false"){
					Console.WriteLine("Couldn't create new user because " + registeredValue);
				}			
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("User Registration", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}		
		}
    /// <summary>
    /// Allows the user to register an account(NOT USED CURRENTLY-EVERY USER IS AN "ACCOUNT")
    /// but hey why not have it ready just in case
    /// </summary>
    /// <returns>post response</returns>
    public async void RegisterAccount(string accountName){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
	
			data.Add("registerAccount","newaccount");
			data.Add("accountName",accountName);
			try{		
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(registerAccountUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var isregistered = response.GetValue("registered").ToString();
				var registeredValue = response.GetValue("accountID").ToString();
				if(isregistered == "true"){
					Console.WriteLine("Created a new account with id: " + registeredValue);
				} else if (isregistered == "false"){
					Console.WriteLine("Couldn't create new account because " + registeredValue);
				}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
	  		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Account Registration", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
    /// <summary>
    /// Downloads the session(s) for a supplied user id that fall between a start and end date
    /// </summary>
    /// <returns>post response</returns>
    public async void DownloadSessions(int accountID, int userID, string startDate, string endDate){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
	
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
	
			data.Add("downloadSession","getData");
			data.Add("userID", userID.ToString());
			data.Add("sessionStart", startDate);
			data.Add("sessionEnd", endDate);
			try{
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(downloadSessionUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				//JObject response = JObject.Parse(textResponse);
				//var isregistered = response.GetValue("registered").ToString();
				//var registeredValue = response.GetValue("accountID").ToString();
				//if(isregistered == "true"){
				//	Console.WriteLine("Created a new account with id: " + registeredValue);
				//} else if (isregistered == "false"){
				//	Console.WriteLine("Couldn't create new account because " + registeredValue);
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
	/// Uploads the analyzer setup for the user
	/// </summary>	
	public async void UploadAnalyzerLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var uploadAnalyzer = ion.currentAnalyzer;

		var userID = KeychainAccess.ValueForKey("userID");
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();
		
		data.Add("uploadSession","sendData");
		data.Add("userID", userID);
		
		if(uploadAnalyzer != null && uploadAnalyzer.sensorList != null){
			var layoutJson = "{";
			var count = 1;                                                                    
			foreach(var sensor in uploadAnalyzer.sensorList){			 	
				var position = Array.IndexOf(uploadAnalyzer.sensorPositions,sensor.analyzerSlot);
			 	layoutJson += "\""+sensor.name+"\":{\"position\":\""+position+"\",\"measurement\":\""+sensor.measurement.amount+"\",\"unit\":\""+sensor.unit.ToString()+"\"}";
				if(count < uploadAnalyzer.sensorList.Count){
					layoutJson += ",";
				}
				count++;
			}
			layoutJson += "}";
			data.Add("devices",layoutJson);
			
			try{
				//initiate the post request and get the request result in a byte array
				//byte[] result = webClient.UploadValues(uploadAnalyzerUrl,data);
				
				////get the string conversion for the byte array
				//var textResponse = Encoding.UTF8.GetString(result);
				//Console.WriteLine(textResponse);
				
				
				Console.WriteLine(layoutJson);
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Layout Upload", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}			
		} else {
			Console.WriteLine("Analyzer List isn't available yet");
		}
	}
	/// <summary>
	/// Uploads the workbench setup for the user
	/// </summary>	
	public async void UploadWorkbenchLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		var userID = KeychainAccess.ValueForKey("userID");
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("uploadWorkbench","data");
		data.Add("userID",userID);
		
		var uploadWorkbench = ion.currentWorkbench;
		if(uploadWorkbench != null && uploadWorkbench.manifolds != null){
			var layoutJson = "{";
			var count = 1;  
			foreach(var manifold in uploadWorkbench.manifolds){
				layoutJson += "\""+manifold.primarySensor.name+"\":{\"measurement\":\""+manifold.primarySensor.measurement.amount+"\",\"unit\":\""+manifold.primarySensor.unit.ToString()+"\"}";
				if(count < uploadWorkbench.manifolds.Count){
					layoutJson += ",";
				}
				count++;			
			}
			layoutJson += "}";
			data.Add("devices",layoutJson);
			
			Console.WriteLine(layoutJson);
			try{
				////initiate the post request and get the request result in a byte array 
				//byte[] result = webClient.UploadValues(uploadWorkbenchUrl,data);
				
				////get the string conversion for the byte array
				//var textResponse = Encoding.UTF8.GetString(result);
				//Console.WriteLine(textResponse);
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Access Code", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		} else {
			Console.WriteLine("Workbench List isn't available yet");
		}
	}
	/// <summary>
	/// Downloads the analyzer setup for a supplied user id
	/// </summary>	
	public async void DownloadAnalyzerLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = KeychainAccess.ValueForKey("userID");
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("downloadAnalyzer","manager");
		data.Add("userID",userID);
		
		try{
			byte[] result = webClient.UploadValues(downloadAnalyzerUrl,data);
			
			var textResponse = Encoding.UTF8.GetString(result);
			
			Console.WriteLine(textResponse);
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Analyzer Layout", "Couldn't pull latest layout. Trying again.", UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		}			
	}
	/// <summary>
	/// Downloads the workbench setup for a supplied user id
	/// </summary>
	public async void DownloadWorkbenchLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));

		var userID = KeychainAccess.ValueForKey("userID");
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("downloadWorkbench","manager");
		data.Add("userID",userID);
		try{
			byte[] result = webClient.UploadValues(downloadWorkbenchUrl,data);
			
			var textResponse = Encoding.UTF8.GetString(result);
			
			Console.WriteLine(textResponse);
		} catch (Exception exception){
			Console.WriteLine("Exception: " + exception);
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			var alert = UIAlertController.Create ("Workbench Layou", "Couldn't pull latest layout. Trying again.", UIAlertControllerStyle.Alert);
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
				//initiate the post request and get the request result in a byte array 
				byte[] result = webClient.UploadValues(retrieveAccessUrl,data);
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
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
					var window = UIApplication.SharedApplication.KeyWindow;
      		var rootVC = window.RootViewController as IONPrimaryScreenController;
					var errorMessage = response.GetValue("message");
					
					var alert = UIAlertController.Create ("User List", errorMessage.ToString(), UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
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
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();
		data.Add("createAccess","true");
		data.Add("userID", userID);
		data.Add("permanent","1");
		
		try{
			//initiate the post request and get the request result in a byte array 
			byte[] result = webClient.UploadValues(createAccessUrl,data);
			
			//get the string conversion for the byte array
			var textResponse = Encoding.UTF8.GetString(result);
			Console.WriteLine(textResponse);
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
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();
		data.Add("confirmCode","true");
		data.Add("userID", userID);
		data.Add("accessCode", codeText);
		
		try{
			//initiate the post request and get the request result in a byte array 
			byte[] result = webClient.UploadValues(confirmAccessUrl,data);

			//get the string conversion for the byte array
			var textResponse = Encoding.UTF8.GetString(result);
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
		Console.WriteLine("Getting all request for id " + userID);
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();
		data.Add("getRequests","true");
		data.Add("userID", userID);

		try{
			//initiate the post request and get the request result in a byte array 
			byte[] result = webClient.UploadValues(getRequestsUrl,data);
			
			//get the string conversion for the byte array
			var textResponse = Encoding.UTF8.GetString(result);
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
				if(rootVC != null){				
					var alert = UIAlertController.Create ("Online Status", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
				return false;
			}			
		});			
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
}

