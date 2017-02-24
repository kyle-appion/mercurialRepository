namespace ION.CoreExtensions.Net.Portal {

	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;

	using Newtonsoft.Json.Linq;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Database;

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
		private const string URL_UPDATE_ACCOUNT = "http://portal.appioninc.com/App/updateAccount.php";
		private const string URL_FORGOT_ACCOUNT = "http://portal.appioninc.com/App/forgotUserPass.php";
		private const string URL_LOGIN_USER = "http://portal.appioninc.com/App/applogin.php";
		private const string URL_RETRIEVE_ACCESS = "http://portal.appioninc.com/App/retrieveAccess.php";
		private const string URL_REGISTER_USER = "http://portal.appioninc.com/App/registerUser.php";
		private const string URL_UPLOAD_SESSION = "http://portal.appioninc.com/App/uploadSession.php";
		private const string URL_LOGIN_USER_2_ARG = "http://portal.appioninc.com/joomla/modules/mod_processing/appWebLogin.php?usrEmail={0}&usrPass={1}";

		private const string JSON_SESSION = "session";
		private const string JSON_UPLOAD_SESSION = "uploadSession";
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


		private const string PORTAL_DATE_FORMAT = "yy-MM-dd HH:mm:ss";

		private const string MIME_JSON = "application/json";


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

		private string userPassword { get; set; }

		public IONPortalService() {
			web = new WebClient();
			web.Proxy = null;
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
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
		/// <param name="confirmPassword">Confirm password.</param>
		public bool IsPasswordValid(string password) {
			return !string.IsNullOrEmpty(password) && password.Length >= 8 && password.Any(c => char.IsUpper(c));
		}

		/// <summary>
		/// Performs a new user registration attempt.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="firstName">First name.</param>
		/// <param name="password">Password.</param>
		/// <param name="lastName">Last name.</param>
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
		public async Task<PortalResponse> LoginAsync(string username, string password) {
			Log.D(this, "Attempting to login to the portal server {username: " + username + ", password: " + password + "}");
			// The form that is used to login to the remote server
			var formContent = new FormUrlEncodedContent(new[] {
				new KeyValuePair<string, string>(LOGIN_USER, RETURNING),
				new KeyValuePair<string, string>(USER_NAME, username),
				new KeyValuePair<string, string>(USER_PASS, password),
			});

			try {
				var response = await client.PostAsync(URL_LOGIN_USER, formContent);
				Log.D(this, "now we have a response from the internet");
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "LoginAsync:\n" + content);
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
					Log.D(this, "Failed to log user into the portal: " + json[JSON_MESSAGE].ToString());
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
		public async Task<PortalResponse> ResetUserPasswordAsync(string email) {
			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_FORGOT_PASSWORD, TRUE),
					new KeyValuePair<string, string>(JSON_EMAIL, email),
				});

				var response = await client.PostAsync(URL_FORGOT_ACCOUNT, formContent);
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "ResetUserPasswordAsync:\n" + content);
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
		public async Task<PortalResponse> UpdatePassword(string newPassword) {
			if (!isLoggedIn) {
				return new PortalResponse(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new[] {
					new KeyValuePair<string, string>(JSON_UPDATE_PASSWORD, NEW),
					new KeyValuePair<string, string>(JSON_NEW_PASSWORD, newPassword),
					new KeyValuePair<string, string>(JSON_USER_ID, this.loginId),
				});

				var response = await client.PostAsync(URL_UPDATE_ACCOUNT, formContent);
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "UpdatePassword:\n" + content);
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
		public async Task<PortalResponse> UploadSessionsAsync(IION ion, IEnumerable<SessionRow> sessions) {
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

				var postContent = new StringContent(package.ToString(), Encoding.UTF8, MIME_JSON);

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
		public async Task<PortalResponse> GenerateAccessCodeAsync() {
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
				Log.D(this, "GenerateAccessCodeAsync:\n" + content);
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
		public async Task<PortalResponse<List<AccessCode>>> QueryPendingAccessCodesAsync() {
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
				Log.D(this, "QueryPendingAccessCodeAsync:\n" + content);
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
		public async Task<PortalResponse> DeleteAccessCodeAsync(string accessCode) {
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
				Log.D(this, "DeleteAccessCodeAsync:\n" + content);
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
				Log.D(this, "SubmitAccessCodeAsync:\n" + content);
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
		public async Task<PortalResponse> ConfirmAccessCodeAsync(AccessCode code) {
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
				Log.D(this, "ConfirmAccessCodeAsync:\n");
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
		/// Queries the list of users of whom the current user is following.
		/// </summary>
		/// <returns>The following async.</returns>
		public async Task<PortalResponse<List<ConnectionData>>> QueryFollowingAsync() {
			if (!isLoggedIn) {
				return new PortalResponse<List<ConnectionData>>(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new [] {
					new KeyValuePair<string, string>(JSON_RETRIEVE_ACCESS, JSON_MANAGER),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
				});

				var response = await client.PostAsync(URL_RETRIEVE_ACCESS, formContent);
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "QueryFollowingAsync:\n" + content);
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					var ret = new List<ConnectionData>();

					foreach (var userTok in json.GetValue(JSON_VIEWING)) {
						try {
							var user = userTok as JObject;

							var usid = user.GetValue(JSON_ID_CAP).ToString();

							int id = int.Parse(usid);

							ret.Add(new ConnectionData() {
								id = id,
								displayName = user.GetValue(JSON_DISPLAY).ToString(),
								email = user.GetValue(JSON_EMAIL).ToString(),
							});
						} catch (Exception e) {
							Log.E(this, "Failed to parse user data:\n" + userTok, e);
						}
					}

					return new PortalResponse<List<ConnectionData>>(response, "", ret, EError.Success);
				} else {
					return new PortalResponse<List<ConnectionData>>(response, "", EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to query user's connections", e);
				return new PortalResponse<List<ConnectionData>>(null, "", EError.InternalError);
			}
		}

		/// <summary>
		/// Removes a following connection data.
		/// </summary>
		/// <returns>The following async.</returns>
		/// <param name="data">Data.</param>
		public async Task<PortalResponse> RemoveFollowingAsync(ConnectionData data) {
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
				Log.D(this, "RemoveFollowingAsync:\n");
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
		public async Task<PortalResponse> RemoveFollowerAsync(ConnectionData data) {
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
				Log.D(this, "RemoveFollowerAsync:\n");
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
		/// Queries the list of users of whom are following the current user.
		/// </summary>
		/// <returns>The followers async.</returns>
		public async Task<PortalResponse<List<ConnectionData>>> QueryFollowersAsync() {
			if (!isLoggedIn) {
				return new PortalResponse<List<ConnectionData>>(null, "", EError.NotLoggedIn);
			}

			try {
				var formContent = new FormUrlEncodedContent(new [] {
					new KeyValuePair<string, string>(JSON_RETRIEVE_ACCESS, JSON_MANAGER),
					new KeyValuePair<string, string>(JSON_USER_ID, loginId),
				});

				var response = await client.PostAsync(URL_RETRIEVE_ACCESS, formContent);
				var content = await response.Content.ReadAsStringAsync();
				Log.D(this, "QueryFollowersAsync:\n" + content);
				var json = JObject.Parse(content);

				if (CheckResponseForSuccess(json)) {
					var ret = new List<ConnectionData>();

					foreach (var userTok in json.GetValue(JSON_ALLOWING)) {
						try {
							var user = userTok as JObject;

							var usid = user.GetValue(JSON_ID_CAP).ToString();

							int id = int.Parse(usid);

							ret.Add(new ConnectionData() {
								id = id,
								displayName = user.GetValue(JSON_DISPLAY).ToString(),
								email = user.GetValue(JSON_EMAIL).ToString(),
							});
						} catch (Exception e) {
							Log.E(this, "Failed to parse user data:\n" + userTok, e);
						}
					}

					return new PortalResponse<List<ConnectionData>>(response, "", ret, EError.Success);
				} else {
					return new PortalResponse<List<ConnectionData>>(response, "", EError.ServerError);
				}
			} catch (Exception e) {
				Log.E(this, "Failed to query user's connections", e);
				return new PortalResponse<List<ConnectionData>>(null, "", EError.InternalError);
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
