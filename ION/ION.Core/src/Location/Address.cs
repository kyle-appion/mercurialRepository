namespace ION.Core.Location {

  using System;

  /// <summary>
  /// A simple object that representing an US address.
  /// </summary>
  public class Address {
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

    public Address() {
      this.address = "";
      this.city = "";
      this.state = "";
      this.zip = "";
    }

    public Address(string address, string city, string state, string zip) {
      this.address = address;
      this.city = city;
      this.state = state;
      this.zip = zip;
    }
  }
}

