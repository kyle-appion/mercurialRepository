namespace ION.Droid.App {

	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.Locations;
	using Android.Net;
	using Android.OS;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	/// <summary>
	/// This service is a fire and forget service that appempts to grab the user's current altitude (and location).
	/// </summary>
	[Service]
	public class LocationResolverService : Service {
		public LocationResolverService() {
		}

		public override IBinder OnBind(Intent intent) {
			return new Binder(this);
		}

		public class Binder : Android.OS.Binder {
			public LocationResolverService service { get; internal set; }

			public Binder(LocationResolverService service) {
				this.service = service;
			}
		}
	}

	public class ElevationResult {
		public bool success { get; private set; }
		public Scalar elevation { get; private set; }
		public string errorMessage { get; private set; }

		internal ElevationResult(Scalar elevation) {
			success = true;
			this.elevation = elevation;
		}

		internal ElevationResult(string errorMessage) {
			success = false;
			this.errorMessage = errorMessage;
		}
	}

	public class GoogleMapsAltitudeRetriever {
		public bool IsNetworkAvailable(Context context) {
			var cm = context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
			var ni = cm.ActiveNetworkInfo;
			return ni != null && ni.IsConnected;
		}

		public async Task<ElevationResult> FetchAltitudeFromLatitudeLongitude(double latitude, double longitude) {
			try {
				double result = Double.NaN;

				string url = "https://maps.googleapis.com/maps/api/elevation/json?locations=" + latitude + "," + longitude;

				var request = WebRequest.Create(url) as HttpWebRequest;

				var response = await request.GetResponseAsync();
				var stream = response.GetResponseStream();

				var sb = new StringBuilder();

				using (var bt = new StreamReader(stream)) {
					string line = null;
					while ((line = bt.ReadLine()) != null) {
						sb.Append(line);
					}
				}

				Log.D(this, "JSON response: " + sb);

				return new ElevationResult(Units.Length.METER.OfScalar(result));
			} catch (Exception e) {
				Log.E(this, "Failed to get response", e);
				return new ElevationResult("Unknown");
			}
		}
	}
}


