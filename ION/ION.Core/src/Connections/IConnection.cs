﻿using System;
using System.Threading.Tasks;


namespace ION.Core.Connections {
  /// <summary>
  /// IConnection is the contract that will wrap the physical connection between the
  /// application and a remote terminus.
  /// </summary>
  public interface IConnection {
    /// <summary>
    /// The event registry that will be notified when the connection's state changes.
    /// </summary>
    event EventHandler<EConnectionState> onStateChanged;
    /// <summary>
    /// The event registrt that will be notified when the connection receives a new
    /// data packet.
    /// </summary>
    event EventHandler<byte[]> onDataReceived;
    /// <summary>
    /// Queries the current state of the connection.
    /// </summary>
    EConnectionState connectionState { get; }
    /// <summary>
    /// Queries the name of the connection. For almost every device, this will be the
    /// device's serial number.
    /// </summary>
    string name { get; }
    /// <summary>
    /// Queries the current received signal strength of the connection. Some connections
    /// will have more reliable RSSI updates than others.
    /// </summary>
    int rssi { get; }
    /// <summary>
    /// Queries whether or not the RSSI readings of the connection are reliable. If they
    /// are, then one can use the RSSI as a pseudo range test.
    /// </summary>
    bool isRssiReliable { get; }
    /// <summary>
    /// Queries the last packet that the connection received.
    /// </summary>
    byte[] lastPacket { get; }
    /// <summary>
    /// The time that the last packet was received by the connection.
    /// </summary>
    DateTime timeLastPacketRecieved { get; }
    /// <summary>
    /// Queries the native connection object that this connection is wrapping.
    /// </summary>
    object nativeDevice { get; }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    Task<bool> Connect();
    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    void Disconnect();
    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    Task<bool> Write(byte[] packet);
  }

  /// <summary>
  /// Enumerates the possible states that a connection can be in.
  /// </summary>
  public enum EConnectionState {
    /// <summary>
    /// The connection is not connected to the remote terminus. No communication is
    /// occurring in this state.
    /// </summary>
    Disconnected,
    /// <summary>
    /// The connection is not connected to the remote terminus. However, it is still
    /// receiving new data packets from the remote.
    /// </summary>
    Broadcasting,
    /// <summary>
    /// The connection is attempting to establish a connection with the remote
    /// terminus.
    /// </summary>
    Connecting,
    /// <summary>
    /// The connection has a connection with the remote terminus, but it is not stable
    /// yet.
    /// </summary>
    Resolving,
    /// <summary>
    /// The connection is stable and communication may occur.
    /// </summary>
    Connected,
  }
}
