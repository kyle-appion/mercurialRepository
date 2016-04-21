namespace ION.Core.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;

  /// <summary>
  /// The connection helper that will scan both for le devices and classic devices.
  /// </summary>
  public class ConnectionHelperCollection : BaseConnectionHelper {
    /// <summary>
    /// Whether or not the connection helper's backend is enabled.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isEnabled {
      get {
        foreach (var h in helpers) {
          if (!h.isEnabled) {
            return false;
          }
        }

        return true;
      }
    }

    /// <summary>
    /// Whether or not the connection helper requires its own timer to handle scanning.
    /// </summary>
    /// <value><c>true</c> if requires custom time; otherwise, <c>false</c>.</value>
    protected override bool requiresCustomTime {
      get {
        return true;
      }
    }

    private List<IConnectionHelper> helpers;

    public ConnectionHelperCollection(params IConnectionHelper[] helpers) {
      this.helpers = new List<IConnectionHelper>(helpers);
      if (helpers.Length <= 0) {
        throw new Exception("Cannot create ConnectionHelperCollection with no child helpers");
      }
    }

    /// <summary>
    /// Platform code for starting a scan.
    /// </summary>
    /// <returns>The scan async.</returns>
    protected override async Task OnScan(TimeSpan scanTime, CancellationToken token) {
      var start = DateTime.Now;
      foreach (var h in helpers) {
        h.onDeviceFound += OnDeviceFound;

        var time = scanTime - (DateTime.Now - start);

        var task = h.Scan(time);
        while (!(task.IsCompleted || task.IsCanceled || task.IsFaulted || token.IsCancellationRequested)) {
          await Task.Delay(TimeSpan.FromMilliseconds(50));
        }

        h.onDeviceFound -= OnDeviceFound;
      }
    }

    /// <summary>
    /// Platform code for stopping a scan.
    /// </summary>
    protected override void OnStop() {
      ION.Core.Util.Log.D(this, "Stopping scan.");
      foreach (var h in helpers) {
        h.Stop();
      }
    }

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    /// <param name="address">Address.</param>
/*
    public override IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion) {
      foreach (var h in helpers) {
        if (h.CanResolveProtocol(protocolVersion)) {
          return h.CreateConnectionFor(address, protocolVersion);
        }
      }

      throw new Exception("Cannot create connection for: " + address);
    }
*/

    /// <summary>
    /// Queries whether or not the connection helper can resolve the given protocol.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="protocol">Protocol.</param>
    public override bool CanResolveProtocol(EProtocolVersion protocol) {
      foreach (var h in helpers) {
        if (h.CanResolveProtocol(protocol)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Enables the connection helper's backend.
    /// </summary>
    public override async Task<bool> Enable() {
      if (!isEnabled) {
        foreach (var h in helpers) {
          if (!h.isEnabled && !await h.Enable()) {
            return false;
          }
        }
      }

      return isEnabled;
    }

    /// <summary>
    /// Raises the device found event.
    /// </summary>
    private void OnDeviceFound(IConnectionHelper helper, ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocolVersion) {
      this.NotifyDeviceFound(serialNumber, address, packet, protocolVersion);
    }
  }
}
