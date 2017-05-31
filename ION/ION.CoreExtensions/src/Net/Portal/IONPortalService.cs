﻿namespace ION.CoreExtensions.Net.Portal {

	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
	using ION.Core.Fluids;
  using ION.Core.IO;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.CoreExtensions.Net.Portal.Remote;

  /// <summary>
  /// An interface object that is used to communicate with the ION portal web service.
  /// </summary>
	public class IONPortalService {

		private const string LOGIN_USER = "loginUser";
		private const string RETURNING = "returning";
		private const string USER_NAME = "uname";
		private const string USER_PASS = "usrPword";

		private const string TRUE = "true";
		private const string FALSE = "false";
		private const string NEW = "new";

		//34.198.112.47
		private const string URL_ACCESS_CODE_CONFIRM = "http://portal.appioninc.com/App/confirmAccess.php";
		private const string URL_ACCESS_CODE_CREATE = "http://portal.appioninc.com/App/requestAccess.php";
		private const string URL_ACCESS_CODE_DELETE = "http://portal.appioninc.com/App/deleteAccess.php";
		private const string URL_ACCESS_CODE_PENDING = "http://portal.appioninc.com/App/getRequests.php";
		private const string URL_CHANGE_STATUS = "http://portal.appioninc.com/App/changeOnlineStatus.php";
		private const string URL_CLONE_REMOTE = "http://portal.appioninc.com/App/downloadLayouts.php";
    private const string URL_RSS_FEED = "http://portal.appioninc.com/RSS/feed.xml";
		private const string URL_UPDATE_ACCOUNT = "http://portal.appioninc.com/App/updateAccount.php";
		private const string URL_FORGOT_ACCOUNT = "http://portal.appioninc.com/App/forgotUserPass.php";
		private const string URL_LOGIN_USER = "http://portal.appioninc.com/App/applogin.php";
		private const string URL_RETRIEVE_ACCESS = "http://portal.appioninc.com/App/retrieveAccess.php";
		private const string URL_REGISTER_USER = "http://portal.appioninc.com/App/registerUser.php";
		private const string URL_UPLOAD_SESSION = "http://portal.appioninc.com/App/uploadSession.php";
		private const string URL_LOGIN_USER_2_ARG = "http://portal.appioninc.com/joomla/modules/mod_processing/appWebLogin.php?usrEmail={0}&usrPass={1}";
    private const string URL_UPLOAD_LAYOUTS = "http://portal.appioninc.com/App/uploadLayouts.php";

		private const string JSON_SESSION = "session";
		private const string JSON_UPLOAD_SESSION = "uploadSession";
		private const string JSON_UPLOAD_PACKAGE = "sessionUploadPackage";
		private const string JSON_START = "start";
		private const string JSON_END = "end";
		private const string JSON_MEASUREMENTS = "measurements";
		private const string JSON_MEASUREMENT = "measurement";
		private const string JSON_RECORDED = "recorded";
		private const string JSON_INDEX = "sindex";
		private const string JSON_SERIAL_NUMBER = "sn";
		private const string JSON_SESSION_DATA = "sessionData";
		private const string JSON_USER_ID = "userID";

		private const string JSON_SUCCESS = "success";
		private const string JSON_MESSAGE = "message";
		private const string JSON_FOUND = "found";
		private const string JSON_DISPLAY = "display";
		private const string JSON_EMAIL = "email";
		private const string JSON_ACCESS_CODE = "accessCode";
		private const string JSON_ACCESS_CODE_CREATE = "createAccess";
		private const string JSON_ACCESS_CODE_CONFIRM = "confirmCode";
		private const string JSON_ACCESS_CODE_DELETE = "deleteRequest";
		private const string JSON_ACCESS_CODE_PENDING = "getRequests";
		private const string JSON_USERS = "users";
		private const string JSON_DISPLAY_NAME = "display";
		private const string JSON_ID = "id";
		private const string JSON_ID_CAP = "ID";
		private const string JSON_RETRIEVE_ACCESS = "retrieveAccess";
		private const string JSON_MANAGER = "manager";
		private const string JSON_VIEWING = "viewing";
		private const string JSON_ALLOWING = "allowing";
		private const string JSON_ONLINE = "online";
		private const string JSON_PERMANENT = "permanent";
    private const string JSON_LAYOUT = "layout";
    private const string JSON_UPLOAD_LAYOUTS = "uploadLayouts";
    private const string JSON_LAYOUT_JSON = "layoutJson";
		private const string JSON_SETUP = "setup";
		private const string JSON_POSITIONS = "positions";
		private const string JSON_LOW_FLUID = "lfluid";
		private const string JSON_HIGH_FLUID = "hfluid";

		private const string JSON_REGISTER_USER = "registerUser";
		private const string JSON_NEW_USER = "newuser";
		private const string JSON_FIRST_NAME = "fname";
		private const string JSON_LAST_NAME = "lname";
		private const string JSON_USER_PASSWORD = "usrpword";
		private const string JSON_USER_EMAIL = "usrEmail";
		private const string JSON_FORGOT_PASSWORD = "forgotten";
		private const string JSON_CHANGE_STATUS = "changeStatus";
		private const string JSON_STATUS = "status";
		private const string JSON_UPDATE_PASSWORD = "updatePassword";
		private const string JSON_NEW_PASSWORD = "newPassword";
    private const string JSON_CONFIRM_ACCESS = "confirmAccess";
    private const string JSON_ACCESS_ID = "accessID";
    private const string JSON_ACCESS_CODE_DELETE_USER = "deleteUserAccess";
    private const string JSON_ACCESS_CODE_DELETE_VIEWER = "deleteViewerAccess";

    private const string JSON_CREATE_LAYOUT = "createLayout";
    private const string JSON_DEVICE_ID = "deviceID";
    private const string JSON_DEVICE_NAME = "deviceName";
    private const string JSON_LAYOUT_ID = "layoutID";
    private const string JSON_LAYOUT_ID_LOWER = "layoutid";
    private const string JSON_LOGGING = "logging";

    private const string JSON_ACTION_CLONE_REMOTE = "downloadLayouts";

		private const string PORTAL_DATE_FORMAT = "yy-MM-dd HH:mm:ss";

		private const string MIME_JSON = "application/json";

    /// <summary>
    /// The event that will alert subscribers when the portal's state changes in a meaningful way.
    /// </summary>
    public event Action<IONPortalService, PortalEvent> onPortalEvent;


		/// <summary>
		/// Whether or not a user is logged into the portal service.
		/// </summary>
		/// <value><c>true</c> if is logged in; otherwise, <c>false</c>.</value>
		public bool isLoggedIn { get; private set; }
		/// <summary>
		/// Gets the logged in time.
		/// </summary>
		/// <value>The logged in time.</value>
		public DateTime loggedInTime { get; private set; }
		/// <summary>
		/// Whether or not the ion portal service is currently uploading.
		/// </summary>
		/// <value><c>true</c> if is uploading; otherwise, <c>false</c>.</value>
		public bool isUploading { get { return appStateUploadCancellationToken != null; } }

    /// <summary>
    /// The connections that the currently logged in user is following.
    /// </summary>
    /// <value>The following connections.</value>
    public IEnumerable<ConnectionData> followingConnections {
      get {
        return __followingConnections.AsReadOnly();
      }
      set {
        __followingConnections.Clear();
        __followingConnections.AddRange(value);
      }
    } List<ConnectionData> __followingConnections = new List<ConnectionData>();

    /// <summary>
    /// The connections that are currently following the currently logged in user;
    /// </summary>
    /// <value>The following connections.</value>
    public IEnumerable<ConnectionData> followerConnections {
      get {
        return __followerConnections.AsReadOnly();
      }
      set {
        __followerConnections.Clear();
        __followerConnections.AddRange(value);
      }
    } List<ConnectionData> __followerConnections = new List<ConnectionData>();

    /// <summary>
    /// The connections known by the currently logged in user that are online.
    /// </summary>
    /// <value>The following connections.</value>
    public IEnumerable<ConnectionData> onlineConnections {
      get {
        return __onlineConnections.AsReadOnly();
      }
      set {
        __onlineConnections.Clear();
        __onlineConnections.AddRange(value);
      }
    } List<ConnectionData> __onlineConnections = new List<ConnectionData>();

		public string loginPortalUrl {
			get {
				if (isLoggedIn) {
					return string.Format(URL_LOGIN_USER_2_ARG, userEmail, userPassword);
				} else {
					return "";
				}
			}
		}

		private WebClient web;
		private HttpClient client;

		public string loginId { get; private set; }
		public string displayName { get; private set; }
		public string userEmail { get; private set; }

    /// <summary>
    /// The user's password. 
    /// </summary>
    // TODO ahodder@appioninc.com: think of a way to get rid of this.
    private string userPassword;
    /// <summary>
    /// The current layout if for the device. Used to identify the current layout for the user.
    /// If null, then RequestLayoutIdAsync must be called.
    /// </summary>
    private string layoutId;
		private CancellationTokenSource appStateUploadCancellationToken;
		private Task uploadLayoutTask;

		public IONPortalService() {
			web = new WebClient();
			web.Proxy = null;
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
      client.Timeout = TimeSpan.FromSeconds(10);
		}

    /// <summary>
    /// Downloads the current rss feed from the appion server.
    /// </summary>
    /// <returns>The rss async.</returns>
    public Task<Rss> DownloadRssOrThrowAsync() {
      return Task.Factory.StartNew(() => {
        var request = WebRequest.Create(URL_RSS_FEED);
        var response = request.GetResponse() as HttpWebResponse;
        if (response.StatusCode == HttpStatusCode.OK) {
          var stream = response.GetResponseStream();
          using (stream) {
            try {
              return RssParser.ParseFeedOrThrow(stream);
            } catch (Exception e) {
              Log.E(this, "Failed to parse rss feed", e);
              return null;
            }
          }
        } else {
          Log.E(this, "Failed to resolve rss feed from server");
          return (Rss)null;
        }
      });
    }

		/// <summary>
		/// Logs the current user out.
		/// </summary>
		public async Task Logout() {
			isLoggedIn = false;
			loginId = null;
			displayName = null;
			userEmail = null;

			try {
				var data = new NameValueCollection();
				data.Add(JSON_CHANGE_STATUS, TRUE);
				data.Add(JSON_ID_CAP, loginId);
				data.Add(JSON_STATUS, "0");

				var response = await web.UploadValuesTaskAsync(URL_CHANGE_STATUS, data);
				var content = Encoding.UTF8.GetString(response);
				Log.D(this, "Logout:\n" + content);
				var json = JObject.Parse(content);
				if (!CheckResponseForSuccess(json)) {
					Log.E(this, "Failed to properly logout the user.");
				}
			} catch (Exception e) {
				Log.E(this, "Failed to properly logout the user.", e);
			}

		}

		/// <summary>
		/// Determines whether or not the given password is valid.
		/// </summary>
		/// <returns><c>true</c>, if password valid was ised, <c>false</c> otherwise.</returns>
		/// <param name="password">Password.</param>
		public bool IsPasswordValid(string password) {
			return !string.IsNullOrEmpty(password) && password.Length >= 8 && password.Any(c => char.IsUpper(c));
		}

		/// <summary>
		/// Performs a new user registration attempt.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="password">Password.</param>
		/// <param name="email">Email.</param>
		public async Task<PortalResponse> RegisterUser(string email, string password) {
			try {
				var formContent = new FormUrlEncodedContent(new [] {
					new KeyValuePair<string, string>(JSON_REGISTER_USER, JSON_NEW_USER),
					new KeyValuePair<string, string>(JSON_USER_EMAIL, email),
					new KeyValuePair<string, string>(JSON_USER_PASSWORD, password),
				});

				var response = await client.PostAsync(URL_REGISTER_USER, formContent);
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "RegisterUser:\n" + content);
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}

			} catch (Exception e) {
				Log.E(this, "Failed to register user", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Attempts to contact the appion portal server to log the user in.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public async Task<PortalResponse> RequestLoginAsync(string username, string password) {
			// The form that is used to login to the remote server
			var formContent = new FormUrlEncodedContent(new[] {
				new KeyValuePair<string, string>(LOGIN_USER, RETURNING),
				new KeyValuePair<string, string>(USER_NAME, username),
				new KeyValuePair<string, string>(USER_PASS, password),
			});

			try {
				var response = await client.PostAsync(URL_LOGIN_USER, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);
				var isUserFound = json[JSON_FOUND].ToString();

				if (isUserFound.Equals(TRUE)) {
					loginId  = json[JSON_MESSAGE].ToString();
					displayName = json[JSON_DISPLAY].ToString();
					userEmail = json[JSON_EMAIL].ToString();
					userPassword = password;

					isLoggedIn = true;
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					Log.E(this, "Failed to log user into the portal: " + json[JSON_MESSAGE].ToString());
					// We did not find the user
					isLoggedIn = false;
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to log user in.", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Posts a request to the Portal server that a user wishes to reset their password.
		/// </summary>
		/// <returns>The user password async.</returns>
		/// <param name="email">Email.</param>
		public async Task<PortalResponse> RequestResetUserPasswordAsync(string email) {
			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_FORGOT_PASSWORD, TRUE),
					new KeyValuePair<string, string>(JSON_EMAIL, email),
				});

				var response = await client.PostAsync(URL_FORGOT_ACCOUNT, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}

			} catch (Exception e) {
				Log.E(this, "Failed to reset user password", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Updates the current user's password.
		/// </summary>
		/// <returns>The password.</returns>
		/// <param name="newPassword">New password.</param>
		public async Task<PortalResponse> RequestUpdatePassword(string newPassword) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_UPDATE_PASSWORD, NEW),
					new KeyValuePair<string, string>(JSON_NEW_PASSWORD, newPassword),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
				});

				var response = await client.PostAsync(URL_UPDATE_ACCOUNT, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to update password", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Uploads the given sessions to the ION server. If there is an internal (client packaging) error, then the portal
		/// response will have a status of HttpStatusCode.NoContent.
		/// </summary>
		/// <returns>The sessions.</returns>
		/// <param name="sessions">Sessions.</param>
		public async Task<PortalResponse> RequestUploadSessionsAsync(IION ion, IEnumerable<SessionRow> sessions) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			// TODO ahodder@appioninc.com: The webservice currently only supports one upload at a time.
			try {
				// Build the JSON object representation of the sessions.
				var root = new JObject();

				// Add sessions
				var i = 1;
				foreach (var s in sessions) {
					var sessionContent = new JObject();
					sessionContent[JSON_START] = s.sessionStart.ToLocalTime().ToString(PORTAL_DATE_FORMAT);
					sessionContent[JSON_END] = s.sessionEnd.ToLocalTime().ToString(PORTAL_DATE_FORMAT);

					var table = ion.database.Table<SensorMeasurementRow>();
					var rawMeasurements = table.Where(smr => smr.frn_SID == s._id).OrderBy(smr => smr.recordedDate);
					var sessionMeasurements = new JArray();
					foreach (var m in rawMeasurements) {
						var measurement = new JObject();
						measurement[JSON_MEASUREMENT] = m.measurement;
						measurement[JSON_RECORDED] = m.recordedDate.ToLocalTime().ToString(PORTAL_DATE_FORMAT);
						measurement[JSON_INDEX] = m.sensorIndex;
						measurement[JSON_SERIAL_NUMBER] = m.serialNumber;
						sessionMeasurements.Add(measurement);
					}

					sessionContent[JSON_MEASUREMENTS] = sessionMeasurements;
					// This is a little awkward. Kyle didn't know about JArray at the time and should probably be switched.
					// So as of time of writing, this isn't supported.
					root[JSON_SESSION + i++] = sessionContent;
				}

				// Package everything up for the final post.
				var package = new JObject();

				package[JSON_UPLOAD_SESSION] = true;
				package[JSON_SESSION_DATA] = root.ToString();
				package[JSON_USER_ID] = loginId;

				var p = new JObject();
				p[JSON_UPLOAD_PACKAGE] = package;

				var postContent = new StringContent(p.ToString(), Encoding.UTF8, MIME_JSON);

				// Post the sessions JSON to the server
				var response = await client.PostAsync(URL_UPLOAD_SESSION, postContent);

				if (response == null) {
					return new PortalResponse(null, "", EError.ServerError);
				}

				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
			} catch (Exception e) {
				Log.E(this, "Failed to upload sessions", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Generates an access code.
		/// </summary>
		/// <returns>The access code async.</returns>
		// TODO ahodder@appioninc.com: This query could use error codes.
		// Ie. currently, if the user mashes generate new access codes, then we will fail with no relavent [localized] message.
		public async Task<PortalResponse> RequestAccessCodeAsync() {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new [] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_CREATE, TRUE),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_PERMANENT, "1"),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_CREATE, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to generate access code.", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Queries all of the access codes that are pending access from the user.
		/// </summary>
		/// <returns>The pending access codes async.</returns>
		public async Task<PortalResponse<List<AccessCode>>> RequestPendingAccessCodesAsync() {
			if (!isLoggedIn) {
				return new PortalResponse<List<AccessCode>>(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new [] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_PENDING, JSON_MANAGER),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_PENDING, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					var codes = new List<AccessCode>();

					var users = json.GetValue(JSON_USERS) as JArray;
					foreach (var user in users) {
						var obj = user as JObject;

						var usid = obj.GetValue(JSON_ID).ToString();
						var dn = obj.GetValue(JSON_DISPLAY_NAME).ToString();
						var ac = obj.GetValue(JSON_ACCESS_CODE).ToString();

						var id = 0;
						if (!int.TryParse(usid, out id)) {
							Log.E(this, "Failed to validate user id {id: " + usid + "}");
							continue;
						}
						codes.Add(new AccessCode(id, ac, dn));
					}
					return new PortalResponse<List<AccessCode>>(response, json.GetValue(JSON_MESSAGE)?.ToString(), codes, EError.Success);
				} else {
					return new PortalResponse<List<AccessCode>>(response, json.GetValue(JSON_MESSAGE)?.ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to query pending access codes", e);
				return new PortalResponse<List<AccessCode>>(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Requests that the given access code be deleted.
		/// </summary>
		/// <returns>The access code async.</returns>
		/// <param name="accessCode">Access code.</param>
		public async Task<PortalResponse> RequestDeleteAccessCodeAsync(string accessCode) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_DELETE, TRUE),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_ACCESS_CODE, accessCode),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_DELETE, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);
				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to delete access code", e);
				return new PortalResponse(null, "", EError.ServerError);
			}
		}

		/// <summary>
		/// Submits an access code to the portal in an attempt to allow the current user to access the user linked to the
		/// given access code.
		/// </summary>
		/// <returns>The access code.</returns>
		/// <param name="accessCode">Access code.</param>
		public async Task<PortalResponse> SubmitAccessCodeAsync(string accessCode) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_CONFIRM, "true"),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_ACCESS_CODE, accessCode),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_CONFIRM, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);
				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to submit access code: " + accessCode, e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Confirms the access code.
		/// </summary>
		/// <returns>The access code async.</returns>
		/// <param name="code">Code.</param>
		public async Task<PortalResponse> RequestConfirmAccessCodeAsync(AccessCode code) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_CONFIRM_ACCESS, TRUE),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_ACCESS_CODE, code.code),
					new KeyValuePair<string, string>(JSON_ACCESS_ID, code.acceptId.ToString()),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_CONFIRM, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json) || "duplicate".Equals(json.GetValue(JSON_SUCCESS).ToString())) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to confirm access code", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Removes a following connection data.
		/// </summary>
		/// <returns>The following async.</returns>
		/// <param name="data">Data.</param>
		public async Task<PortalResponse> RequestRemoveFollowingAsync(ConnectionData data) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_DELETE_USER, TRUE),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_ACCESS_ID, data.id.ToString()),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_DELETE, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to confirm access code", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Removes a follower connection data.
		/// </summary>
		/// <returns>The following async.</returns>
		/// <param name="data">Data.</param>
		public async Task<PortalResponse> RequestRemoveFollowerAsync(ConnectionData data) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_ACCESS_CODE_DELETE_VIEWER, TRUE),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_ACCESS_ID, data.id.ToString()),
				});

				var response = await client.PostAsync(URL_ACCESS_CODE_DELETE, formContent);
				var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString());
				} else {
					return new PortalResponse(response, json.GetValue(JSON_MESSAGE).ToString(), EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to confirm access code", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

    /// <summary>
    /// Downloads the latest connection data state from the server.
    /// </summary>
    /// <returns>The connection data.</returns>
    public async Task<PortalResponse> RequestConnectionData() {
      if (!isLoggedIn) {
        return new PortalResponse(null, null, EError.NotLoggedIn);
      }

      try {
        var formContent = new FormUrlEncodedContent(new [] {
          new KeyValuePair<string, string>(JSON_RETRIEVE_ACCESS, JSON_MANAGER),
          new KeyValuePair<string, string>(JSON_USER_ID, loginId),
        });

        var response = await client.PostAsync(URL_RETRIEVE_ACCESS, formContent);
        var content = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(content);

        if (CheckResponseForSuccess(json)) {
          JToken viewingToken;
          if (json.TryGetValue(JSON_VIEWING, out viewingToken)) {
            var data = JsonConvert.DeserializeObject<List<ConnectionData>>(viewingToken.ToString());
            followingConnections = data;
          }

          JToken allowingToken;
          if (json.TryGetValue(JSON_ALLOWING, out allowingToken)) {
            var data = JsonConvert.DeserializeObject<List<ConnectionData>>(allowingToken.ToString());
            followerConnections = data;
          }

          JToken onlineToken;
          if (json.TryGetValue(JSON_ONLINE, out onlineToken)) {
            var data = JsonConvert.DeserializeObject<List<ConnectionData>>(onlineToken.ToString());
            onlineConnections = data;
          }

          return new PortalResponse(response, null, EError.Success);
        } else {
          return new PortalResponse(response, null, EError.ServerError);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to RequestConnectionData.", e);
        return new PortalResponse(null, null, EError.InternalError);
      }
    }

    /// <summary>
    /// Requests that the server return or create a layout id for this device for the logged in user. This layout id
    /// is what is used to identify the user's remote session.
    /// </summary>
    /// <returns>The create layout identifier.</returns>
    // TODO Refer to line ION.CoreExtensions.Net.WebPayload#478
    // TODO Refer to line ION.CoreExtensions.Net.WebPayload#234
    // TODO Refer to line ION.IOS.ViewController.RemoteViewingViewController#97
    public async Task<PortalResponse<string>> RequestLayoutIdAsync(IION ion) {
      try {
        var formContent = new FormUrlEncodedContent(new[] {
          new KeyValuePair<string, string>(JSON_CREATE_LAYOUT, TRUE),
          new KeyValuePair<string, string>(JSON_USER_ID, loginId),
          new KeyValuePair<string, string>(JSON_DEVICE_ID, ion.preferences.appId.ToString()),
          new KeyValuePair<string, string>(JSON_DEVICE_NAME, ion.GetPlatformInformation().GetDeviceName()),
          new KeyValuePair<string, string>(JSON_STATUS, JsonConvert.SerializeObject(new RemoteStatus(ion))),
        });

        var response = await client.PostAsync(URL_UPLOAD_LAYOUTS, formContent);
        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        if (CheckResponseForSuccess(json)) {
          JToken errorMessage;
          if (!json.TryGetValue(JSON_MESSAGE, out errorMessage)) {
            Log.E(this, "Did not receive an error message from RequestLayoutIdAsync");
            return new PortalResponse<string>(response, null, EError.ServerError);
          }

          JToken layoutIdToken;
          if (!json.TryGetValue(JSON_LAYOUT_ID_LOWER, out layoutIdToken)) {
            Log.E(this, "Did not receive a layout id from RequestLayoutIdAsync");
            return new PortalResponse<string>(response, null, EError.ServerError);
          } else {
            layoutId = layoutIdToken.ToString();
          }

          return new PortalResponse<string>(response, layoutId, EError.Success);
        } else {
          // We did not get a valid response from the server.
          return new PortalResponse<string>(response, null, EError.ServerError);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to request layout id.", e);
        return new PortalResponse<string>(null, null, EError.InternalError);
      }
    }

		/// <summary>
		/// Downloads and inflates the given remote user's ION instance into this instance.
		/// </summary>
		/// <returns>The from remote.</returns>
		/// <param name="userId">User identifier.</param>
		public async Task<PortalResponse> RequestCloneFromRemote(IION ion, ConnectionData connection) {
			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_ACTION_CLONE_REMOTE, JSON_MANAGER),
          new KeyValuePair<string, string>(JSON_USER_ID, connection.id + ""),
          new KeyValuePair<string, string>(JSON_LAYOUT_ID_LOWER, connection.layoutId),
				});

				var response = await client.PostAsync(URL_CLONE_REMOTE, formContent);
				var content = await response.Content.ReadAsStringAsync();

				var json = JObject.Parse(content);
				string layout;
				if (CheckResponseForSuccess(json)) {
					layout = json.GetValue(JSON_LAYOUT).ToString();

					var appState = JsonConvert.DeserializeObject<RemoteAppState>(layout);
					await SyncDeviceManagerAsync(ion, appState);

					ion.PostToMain(() => {
						try {
							SyncWorkbench(ion, appState);
							SyncAnalyzer(ion, appState);
						} catch (Exception e) {
							Log.E(this, "Failed to sync", e);
						}
					});
				}

				return new PortalResponse(null, "", EError.InternalError);
			} catch (Exception e) {
				Log.E(this, "Failed to clone ion instance from remote", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

    /// <summary>
    /// Uploads the current user's applications state for remote viewing.
    /// </summary>
    /// <returns>The app state.</returns>
    /// <param name="ion">Ion.</param>
    public bool BeginAppStateUpload(IION ion) {
      lock (web) {
        if (appStateUploadCancellationToken == null) {
          appStateUploadCancellationToken = new CancellationTokenSource();
          uploadLayoutTask = Task.Factory.StartNew(async () => {
            while (!appStateUploadCancellationToken.Token.IsCancellationRequested) {
              try {
                var appState = RemoteAppState.CreateOrThrow(ion);
                var response = await PerformAppStateUpload(ion, appState);
                if (!response.success) {
                  Log.E(this, "Failed to upload state. Message: {" + response.message + "}");
                }
              } catch (Exception e) {
                Log.E(this, "Failed to upload app state", e);
                EndAppStateUpload();
              }
              await Task.Delay(TimeSpan.FromSeconds(1));
            }
            appStateUploadCancellationToken = null;
          }, appStateUploadCancellationToken.Token);
          return true;
        } else {
          return false;
        }
      }
    }

    /// <summary>
    /// Safely ends the app state upload task.
    /// </summary>
    public void EndAppStateUpload() {
      lock (web) {
        layoutId = null;
        if (appStateUploadCancellationToken != null) {
          appStateUploadCancellationToken.Cancel();
        }
				appStateUploadCancellationToken = null;
			}
    }

		/// <summary>
		/// Uploads the given app state to the appion server.
		/// </summary>
		/// <returns>The app state upload.</returns>
		/// <param name="ion">Ion.</param>
		private async Task<PortalResponse> PerformAppStateUpload(IION ion, RemoteAppState appState) {
			try {
				var layout = JsonConvert.SerializeObject(appState);

        if (layoutId == null) {
          // Attempt to get out layout id.
          var layoutResponse = await RequestLayoutIdAsync(ion);
          if (!layoutResponse.success) {
            Log.E(this, "PerformAppStateUpload: failed to get layout id: cannot upload layout.");
            return new PortalResponse(layoutResponse.response, null, EError.InternalError);
          }
        }

        var status = new RemoteStatus(ion);

				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_UPLOAD_LAYOUTS, JSON_MANAGER),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
					new KeyValuePair<string, string>(JSON_LAYOUT_JSON, layout.ToString()),
          new KeyValuePair<string, string>(JSON_LAYOUT_ID, layoutId),
          new KeyValuePair<string, string>(JSON_STATUS, JsonConvert.SerializeObject(new RemoteStatus(ion))),
				});

				var response = await client.PostAsync(URL_UPLOAD_LAYOUTS, formContent);
				var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        var loggingStatus = json.GetValue(JSON_LOGGING);
        var shouldBeLogging = loggingStatus.Value<int>() != 0 ? true : false;

        if (shouldBeLogging) {
          if (!ion.dataLogManager.isRecording) {
            var result = await ion.dataLogManager.BeginRecording(ion.preferences.report.dataLoggingInterval);
            if (!result) {
              Log.E(this, "Failed to save data logging session.");
            }
          }
        } else {
          if (ion.dataLogManager.isRecording) {
            var result = await ion.dataLogManager.StopRecording();
            if (!result) {
              Log.E(this, "Failed to end datalogging by remote command.");
            }
          }
        }

				return new PortalResponse(response, "Ok", EError.Success);
			} catch (Exception e) {
				Log.E(this, "Failed to perform app state upload", e);
				return new PortalResponse(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Synchronizes the current
		/// </summary>
		/// <returns>The device manager.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="state">State.</param>
		private async Task SyncDeviceManagerAsync(IION ion, RemoteAppState state) {
			// This set is the set containing all the of the devices that are pending removal from the device manager.
			// If a user forgets a device, we need to reflect that in the cloned device manager. So, this set will contain
			// all of the "historical" devices from the last state. As devices are iterated over, we will remove them from
			// the set. Once all new devices have been added, we will remove all remaining devices in the set from the
			// device manager.
			var oldDevices = new HashSet<IDevice>(ion.deviceManager.devices);
			foreach (var remoteDevice in state.knownDevices) {
				var serialNumber = remoteDevice.serialNumber.ParseSerialNumber();
				var device = ion.deviceManager[serialNumber];

				if (device == null) {
					// The device does not exist. We need to create and register it.
					device = ion.deviceManager.CreateDevice(serialNumber, "", EProtocolVersion.V4);
					ion.deviceManager.Register(device);
				}

				var gd = device as GaugeDevice;
				oldDevices.Remove(device); // Remove the device from the pending device removal.

				foreach (var remoteSensor in remoteDevice.sensors) {
					var sensor = gd[remoteSensor.sensorIndex];
					sensor.ForceSetMeasurement(UnitLookup.GetUnit(remoteSensor.unit).OfScalar(remoteSensor.measurement));
				}
			}

			// Remove all remaining historical devices.
			foreach (var device in oldDevices) {
				await ion.deviceManager.DeleteDevice(device.serialNumber);
			}
		}

		private void SyncAnalyzer(IION ion, RemoteAppState appState) {
			// Sync the sensor mounts as this is the more important part
			var analyzer = ion.currentAnalyzer;
			analyzer.isEditable = false;

      // The set will contain sensor mounts that were present in the analyzer before the sync started that were removed
      // by the app state. Simply, anything left in here after the following loop is removed from the analyzer.
			var pendingSensorMountRemovals = new HashSet<Sensor>(analyzer.sensorList);

			foreach (var sm in appState.analyzer) {
				var sn = sm.serialNumber.ParseSerialNumber();
				var device = ion.deviceManager[sn] as GaugeDevice;

				if (device != null) {
					var sensor = device[int.Parse(sm.sensorIndex)];
					var si = int.Parse(sm.analyzerIndex);

					if (analyzer.HasSensor(sensor)) {
						pendingSensorMountRemovals.Remove(sensor);

						var csi = analyzer.IndexOfSensor(sensor);
						if (csi != si) {
							// Swap where the sensor mount is
							analyzer.SwapSensors(si, csi, true);
						} else {
							// Don't need to do anything
						}
					} else {
						analyzer.PutSensor(si, sensor);
					}
				}
			}

      // Remove all pending sensor mounts
			foreach (var sensor in pendingSensorMountRemovals) {
				analyzer.RemoveSensor(sensor);
			}

			// Sync the analyzer manifolds
			int index = 0;

			// Sync the low manifold
      var uslpsn = appState.lh.lowSerialNumber;
      if (uslpsn.IsValidSerialNumber()) {
        var m = analyzer.lowSideManifold;

        if (!int.TryParse(appState.lh.lowAnalyzerIndex, out index)) {
          m = null;
        }
				// We have a low side manifold
				var sensor = analyzer[index];
				if (m == null || !sensor.Equals(m.primarySensor)) {
					// We need to update the manifold
					analyzer.SetManifold(Analyzer.ESide.Low, sensor);
					m = analyzer.lowSideManifold;
				}

        // The analyzer has a low side manifold, so lets set its subviews and secondary sensor.
				if (m != null) {
          // Start by setting the manifold's secondary sensor.
          var uslsn = appState.lh.lowLinkedSerialNumber;
          if (!string.IsNullOrEmpty(uslsn) && !uslsn.Equals("null", StringComparison.OrdinalIgnoreCase)) {
            var lsn = uslsn.ParseSerialNumber();
            var sd = ion.deviceManager[lsn] as GaugeDevice;
            if (sd != null) {
              var sds = sd[appState.lh.lowLinkedSensorIndex];
              if (m.secondarySensor != sds) {
                m.SetSecondarySensor(sds);
              }
            }
          }

          // Setup the subviews for the manifold.
					var pendingSubviewRemovals = new HashSet<ISensorProperty>(m.sensorProperties);
					foreach (var subCode in appState.lh.lowSubviews) {
						var newSp = RemoteAnalyzerLH.ParseSensorPropertyFromCode(m, subCode);
            if (newSp != null) {
              var type = newSp.GetType();
              if (!m.HasSensorPropertyOfType(type) && !type.Equals(typeof(RateOfChangeSensorProperty))) {
                m.AddSensorProperty(newSp);
              } else {
                var sp = m.GetSensorPropertyOfType(type);
                pendingSubviewRemovals.Remove(sp);
              }
            }
					}

					foreach (var sp in pendingSubviewRemovals) {
						m.RemoveSensorProperty(sp);
					}
				}
      } else {
        if (analyzer.lowSideManifold != null) {
          analyzer.SetManifold(Analyzer.ESide.Low, (Manifold)null);
        }
      }

			// Sync the high manifold
      var ushpsn = appState.lh.highSerialNumber;
      if (ushpsn.IsValidSerialNumber()) {
        var m = analyzer.highSideManifold;

        if (!int.TryParse(appState.lh.highAnalyzerIndex, out index)) {
          m = null;
        }
				// We have a low side manifold
				var sensor = analyzer[index];
				if (m == null || !sensor.Equals(m.primarySensor)) {
					// We need to update the manifold
					analyzer.SetManifold(Analyzer.ESide.High, sensor);
					m = analyzer.highSideManifold;
				}

				if (m != null) {
          // Start by setting the manifold's secondary sensor.
          var ushsn = appState.lh.highLinkedSerialNumber;
          if (!string.IsNullOrEmpty(ushsn) && !ushsn.Equals("null", StringComparison.OrdinalIgnoreCase)) {
            var hsn = ushsn.ParseSerialNumber();
            var sd = ion.deviceManager[hsn] as GaugeDevice;
            if (sd != null) {
              var sds = sd[appState.lh.highLinkedSensorIndex];
              if (m.secondarySensor != sds) {
                m.SetSecondarySensor(sds);
              }
            }
          }

          // Setup the subviews for the manifold.
					var pendingSubviewRemovals = new HashSet<ISensorProperty>(m.sensorProperties);
					foreach (var subCode in appState.lh.highSubviews) {
						var newSp = RemoteAnalyzerLH.ParseSensorPropertyFromCode(m, subCode);
            if (newSp != null) {
              var type = newSp.GetType();
              if (!m.HasSensorPropertyOfType(type) && !type.Equals(typeof(RateOfChangeSensorProperty))) {
                m.AddSensorProperty(newSp);
              } else {
                var sp = m.GetSensorPropertyOfType(type);
                pendingSubviewRemovals.Remove(sp);
              }
            }
					}

					foreach (var sp in pendingSubviewRemovals) {
						m.RemoveSensorProperty(sp);
					}
				}
      } else {
        if (analyzer.highSideManifold != null) {
          analyzer.SetManifold(Analyzer.ESide.High, (Manifold)null);
        }
      }
		}
		/// <summary>
		/// Attempts to sync the received uploaded workbench to the local remote workbench.
		/// </summary>
		/// <param name="ion">Ion.</param>
		private void SyncWorkbench(IION ion, RemoteAppState state) {
			var wb = ion.currentWorkbench;
			wb.isEditable = false;

			var pendingRemovals = new HashSet<Sensor>();
			foreach (var m in wb.manifolds) {
				pendingRemovals.Add(m.primarySensor);
			}

			var ci = 0; // Current index
			foreach (var remoteManifold in state.workbench) {
				var sn = remoteManifold.serialNumber.ParseSerialNumber();

				var gd = ion.deviceManager[sn] as GaugeDevice;
				if (gd != null) {
					var sensor = gd[remoteManifold.index];

					var wbi = wb.IndexOf(sensor);
					if (wbi != -1) { // The sensor is present in the workbench
						pendingRemovals.Remove(wb[wbi].primarySensor);

						if (wbi != ci) { // The sensor moved in the workbench
							wb.Remove(sensor);
							var m = remoteManifold.InflateManifoldOrThrowAsync(ion).Result;
							wb.Insert(m, ci);
						} else { // The sensor is exactly where we left it.
							// Do Nothing
							SyncManifold(ion, wb[wbi], remoteManifold);
							SyncManifoldSensorProperties(wb[wbi], remoteManifold);
						}
					} else { // The sensor is not present in the workbench
						// Insert the sensor into the workbench at the current index
						var m = remoteManifold.InflateManifoldOrThrowAsync(ion).Result;
						wb.Insert(m, ci);
					}
				}
				ci++;
			}

			foreach (var sensor in pendingRemovals) {
				wb.Remove(sensor);
			}
		}

		// TODO ahodder@appioninc.com: Using Task.Result synchronously
		private void SyncManifold(IION ion, Manifold manifold, RemoteManifold rm) {
			var fs = (Fluid.EState)rm.fluidState;
			if (!manifold.ptChart.fluid.name.Equals(rm.fluid) || manifold.ptChart.state != fs) {
				manifold.ptChart = PTChart.New(ion, fs, ion.fluidManager.LoadFluidAsync(rm.fluid).Result);
			}
		}

		private void SyncManifoldSensorProperties(Manifold manifold, RemoteManifold rm) {
			var codes = new HashSet<int>(rm.subviewCodes);
			var sps = new List<ISensorProperty>(manifold.sensorProperties);

			foreach (var sp in sps) {
				var code = RemoteManifold.CodeFromSensorProperty(sp);
				if (code != -1) {
					if (!codes.Contains(code)) {
						manifold.RemoveSensorProperty(sp);
					}
				}
			}

			foreach (var code in codes) {
				manifold.AddSensorProperty(RemoteManifold.ParseSensorPropertyFromCode(manifold, code));
			}
		}

		/// <summary>
		/// Attempts to retrieve and return whether or not the responseContent contained a valid success element.
		/// </summary>
		/// <returns><c>true</c>, if response for success was checked, <c>false</c> otherwise.</returns>
		/// <param name="responseContent">Response content.</param>
		private bool CheckResponseForSuccess(JObject responseContent) {
			if (responseContent != null) {
				JToken success;
				if (responseContent.TryGetValue(JSON_SUCCESS, out success)) {
					var s = success.ToString();
					bool ret = false;
					bool.TryParse(s, out ret);
					return ret;
				} else {
					return false;
				}
			}

			return false;
		}

		public class PortalResponse {
			public HttpResponseMessage response { get; private set; }
			public EError error { get; private set; }
			public string message { get; private set; }
			public bool success { get { return EError.Success == error; } }

			internal PortalResponse(HttpResponseMessage response, string message, EError error=EError.Success) {
				this.response = response;
				this.message = message;
				this.error = error;
			}
		}

		public class PortalResponse<RESULT> : PortalResponse {
			public RESULT result { get; private set; }

			internal PortalResponse(HttpResponseMessage response, string message, EError error=EError.Success) : base(response, message, error) {
			}

			internal PortalResponse(HttpResponseMessage response, string message, RESULT result, EError error=EError.Success) : base(response, message, error) {
				this.result = result;
			}
		}

		/// <summary>
		/// An enumeration of the type of response.
		/// </summary>
		public enum EError {
			Success,
			ServerError,
			InternalError,
			NotLoggedIn,
		}
	}
}
