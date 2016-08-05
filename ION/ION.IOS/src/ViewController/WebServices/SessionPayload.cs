﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ION.Core.App;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ION.IOS.ViewController.WebServices {
	public sealed class PreserveAttribute : System.Attribute 
	{
	    public bool AllMembers;
	    public bool Conditional;
	}

	public class SessionPayload {
		public string message;
		public IION ion;
		
		public const string uploadSessionUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		public const string registerUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/registerUser.php";
		public const string registerAccountUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		public const string uploadAnalyzerUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		public const string uploadWorkbenchUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		public const string downloadAnalyzerUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		public const string downloadWorkbenchUrl = "http://www.buildtechhere.com/DLog/StoreSession.php";
		
		public SessionPayload() {
			ion = AppState.context;

			/************create a new session payload
			var package = new ION.IOS.ViewController.WebServices.SessionPayload();
			*********************/
		}
		
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
		
		public async void UploadSession(string json, bool isJson = true)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			WebClient wc = new WebClient();
			wc.Proxy = null;
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("uploadSession","true");
			data.Add("sessionData",json);
			data.Add("userID","1");
			data.Add("accountID","1");
			
			//initiate the post request and get the request result in a byte array 
			byte[] result = wc.UploadValues(uploadSessionUrl,data);
			
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
		}
    /// <summary>
    /// Working with sending and recieving data between companies and employees
    /// </summary>
    /// <returns>post response</returns>
    public async void RegisterUser(string userName, string password, string displayName, string email){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
			WebClient wc = new WebClient(); 
			wc.Proxy = null;
			
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();

			data.Add("registerUser","newuser");
			data.Add("uname",userName);
			data.Add("usrpword",password);
			data.Add("usrEmail",email);
			data.Add("displayName",displayName);
			
			//initiate the post request and get the request result in a byte array 
			byte[] result = wc.UploadValues(registerUserUrl,data);
			
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
		}
    /// <summary>
    /// Working with sending and recieving data between companies and employees
    /// </summary>
    /// <returns>post response</returns>
    public async void RegisterAccount(string accountName){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
		WebClient wc = new WebClient(); 
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("registerAccount","newaccount");
		data.Add("accountName",accountName);
		
		//initiate the post request and get the request result in a byte array 
		byte[] result = wc.UploadValues(registerAccountUrl,data);
		
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
	}
    /// <summary>
    /// Working with sending and recieving data between companies and employees
    /// </summary>
    /// <returns>post response</returns>
    public async void DownloadSessions(int accountID, int userID, string startDate, string endDate){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
		WebClient wc = new WebClient(); 
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("downloadSession","getData");
		data.Add("accountID", accountID.ToString());
		data.Add("userID", userID.ToString());
		data.Add("sessionStart", startDate);
		data.Add("sessionEnd", endDate);
		
		//initiate the post request and get the request result in a byte array 
		byte[] result = wc.UploadValues("http://www.buildtechhere.com/DLog/StoreSession.php",data);
		
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
	}
	
	public async void UploadAnalyzerLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		var uploadAnalyzer = ion.currentAnalyzer;

		WebClient wc = new WebClient(); 
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();
		
		data.Add("uploadSession","sendData");
		data.Add("accountID", "1");

		
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
			data.Add("layoutJson",layoutJson);
			//initiate the post request and get the request result in a byte array
			//byte[] result = wc.UploadValues(uploadAnalyzerUrl,data);
			
			////get the string conversion for the byte array
			//var textResponse = Encoding.UTF8.GetString(result);
			//Console.WriteLine(textResponse);
			
			
			Console.WriteLine(layoutJson);
		} else {
			Console.WriteLine("Analyzer List isn't available yet");
		}
	}
	
	public async void UploadWorkbenchLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		
		WebClient wc = new WebClient(); 
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("uploadWorkbench","data");
		data.Add("userID","1");
		
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
			data.Add("layoutJson",layoutJson);
			
			Console.WriteLine(layoutJson);
			////initiate the post request and get the request result in a byte array 
			//byte[] result = wc.UploadValues(uploadWorkbenchUrl,data);
			
			////get the string conversion for the byte array
			//var textResponse = Encoding.UTF8.GetString(result);
			//Console.WriteLine(textResponse);
		} else {
			Console.WriteLine("Workbench List isn't available yet");
		}
	}
	
	public async void DownloadAnalyzerLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		WebClient wc = new WebClient(); 
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("downloadAnalyzer","manager");
		data.Add("userID","1");
		
		byte[] result = wc.UploadValues("http://www.buildtechhere.com/DLog/StoreSession.php",data);
		
		var textResponse = Encoding.UTF8.GetString(result);
		
		Console.WriteLine(textResponse);
	}
	
	public async void DownloadWorkbenchLayout(){
		await Task.Delay(TimeSpan.FromMilliseconds(1));
		WebClient wc = new WebClient();
		wc.Proxy = null;
		
		//Create the data package to send for the post request
		//Key value pair for post variable check
		var data = new System.Collections.Specialized.NameValueCollection();

		data.Add("downloadAnalyzerLayout","manager");
		data.Add("accountID","1");
		data.Add("userID","1");
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
}

