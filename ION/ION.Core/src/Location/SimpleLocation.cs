﻿namespace ION.Core.Location {
  
	using Appion.Commons.Measure;

  /// <summary>
  /// A simple implementation of a location that can be used by any platform.
  /// </summary>
  public class SimpleLocation : ILocation {
    /// <summary>
    /// Wether or not the location is valid, meaning the location is up to date by a primary location provider.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isValid { get; set; }
    /// <summary>
    /// The altitude of the location.
    /// </summary>
    /// <value>The altitude.</value>
    public Scalar altitude { get; set; }
    /// <summary>
    /// The longitude of the location.
    /// </summary>
    /// <value>The longitude.</value>
    public double longitude { get; set; }
    /// <summary>
    /// The latitude of the location.
    /// </summary>
    /// <value>The latitude.</value>
    public double latitude { get; set; }
    /// <summary>
    /// The last known address the device was located in.
    /// </summary>
    /// <value>The address.</value>
    public string address { get; set; }
    /// <summary>
    /// The last known city the device was located in.
    /// </summary>
    /// <value>The city.</value>
    public string city { get; set; }
    /// <summary>
    /// The last known state the device was located in.
    /// </summary>
    /// <value>The state.</value>
    public string state { get; set; }
    /// <summary>
    /// The last known zip the device was located in.
    /// </summary>
    /// <value>The zip.</value>
    public string zip { get; set; }

    public SimpleLocation() {
      this.isValid = false;
      Init(0, 0, 0);
    }

		public SimpleLocation(bool valid, double altitudeMeters, double longitude, double latitude) {
      this.isValid = valid;
      Init(altitudeMeters, longitude, latitude);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Location.SimpleLocation"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Location.SimpleLocation"/>.</returns>
    public override string ToString() {
      return string.Format("[SimpleLocation: isValid={0}, altitude={1}, longitude={2}, latitude={3}]", isValid, altitude, longitude, latitude);
    }

    /// <summary>
    /// Initializes the state of the location.
    /// </summary>
    /// <param name="altitude">Altitude.</param>
    /// <param name="longitude">Longitude.</param>
    /// <param name="latitude">Latitude.</param>
    protected void Init(double altitude, double longitude, double latitude) {
      this.altitude = Units.Length.METER.OfScalar(altitude);
      this.longitude = longitude;
      this.latitude = latitude;
    }
  }
}

