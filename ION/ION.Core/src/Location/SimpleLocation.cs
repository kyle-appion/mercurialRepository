namespace ION.Core.Location {
  
  using ION.Core.Measure;

  /// <summary>
  /// A simple implementation of a location that can be used by any platform.
  /// </summary>
  public class SimpleLocation : ILocation {
    /// <summary>
    /// Wether or not the location is valid.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isValid { get; private set; }
    /// <summary>
    /// The altitude of the location.
    /// </summary>
    /// <value>The altitude.</value>
    public Scalar altitude { get; private set; }
    /// <summary>
    /// The longitude of the location.
    /// </summary>
    /// <value>The longitude.</value>
    public Scalar longitude { get; private set; }
    /// <summary>
    /// The latitude of the location.
    /// </summary>
    /// <value>The latitude.</value>
    public Scalar latitude { get; private set; }

    public SimpleLocation() {
      this.isValid = false;
      Init(0, 0, 0);
    }

    public SimpleLocation(bool valid, double altitude, double longitude, double latitude) {
      this.isValid = valid;
      Init(altitude, longitude, latitude);
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
      this.longitude = Units.Angle.DEGREE.OfScalar(longitude);
      this.latitude = Units.Angle.DEGREE.OfScalar(latitude);
    }
  }
}

