using System;
namespace ION.CoreExtensions.Net.Portal {

  /// <summary>
  /// A simple structure that will wrap the parameters retrieved from logging a user into the ION Portal service.
  /// </summary>
  public class UserParameters {
    /// <summary>
    /// The user id for the remote database.
    /// </summary>
    /// <value>The login identifier.</value>
    public string id { get; internal set; }
    /// <summary>
    /// The email for the user.
    /// </summary>
    /// <value>The email.</value>
    public string email { get; internal set; }
    /// <summary>
    /// The display name for the user.
    /// </summary>
    /// <value>The display name.</value>
    public string displayName { get; internal set; }
    /// <summary>
    /// The first name of the user.
    /// </summary>
    /// <value>The display name of the first.</value>
    public string username { get; internal set; }
    /// <summary>
    /// The encrypted password for the user.
    /// </summary>
    /// <value>The password.</value>
    public string password { get; internal set; }
  }
}
