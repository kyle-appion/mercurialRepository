namespace ION.Core.Location {

  using System;

  /// <summary>
  /// A simple object that representing an US address.
  /// </summary>
  public class Address {
    public string address1 { get; set; }
		public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
		public string country { get; set; }
    public string zip { get; set; }
    public ILocation location { get; set; }

    public Address() {
      this.address1 = "";
			this.address2 = "";
      this.city = "";
      this.state = "";
			this.country = "";
      this.zip = "";
    }

		public Address(string address1, string address2, string city, string state, string country, string zip) {
      this.address1 = address1;
			this.address2 = address2;
      this.city = city;
      this.state = state;
			this.country = country;
      this.zip = zip;
    }
  }
}

