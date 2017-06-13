namespace ION.Core.App {
  
  using System;

  public class AppionConstants {

    /// <summary>
    /// The e-mail address to contact appion support.
    /// </summary>
    public const string EMAIL_SUPPORT = "appsupport@appiontools.com";

		/// <summary>
		/// The proper bluetooth sig manufacturer id for Appion. This id is used to identify modern Appion bluetooth products.
		/// </summary>
		public const int MANFAC_ID = 0x038c;

		/// <summary>
		/// A content-type (mime) that indicates that the body contains an encapsulated message with the syntax of an
		/// RFC 822 message.
		/// </summary>
		/// <remarks>
		///   http://www.w3.org/Protocols/rfc1341/7_3_Message.html
		/// </remarks>
		public const string MIME_RFC822 = "message/rfc822";
  }
}

