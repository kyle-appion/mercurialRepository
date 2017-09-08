namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using Android.Bluetooth;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V4.Content;

	using Appion.Commons.Util;

	using ION.Core.Connections;
	using ION.Core.Devices;
	using ION.Core.Devices.Protocols;

	using ION.Droid.App;

  public class AndroidConnectionManager : Java.Lang.Object, IConnectionManager, ISharedPreferencesOnSharedPreferenceChangeListener {

    // Implemented for IConnectionManager
    public event OnScanStateChanged onScanStateChanged;
    // Implemented for IConnectionManager
    public event OnDeviceFound onDeviceFound;

    // Implemented for IConnectionManager
    public bool isEnabled { get { return bm.Adapter.IsEnabled; } }
    // Implemented for IConnectionManager
    public bool isScanning { 
      get {
        lock (locker) {
          return __isScanning;
        }
      }
      private set {
        lock (locker) {
          __isScanning = value;
          if (onScanStateChanged != null) {
            onScanStateChanged(this);
          }
        }
      }
    } bool __isScanning;
    // Implemented for IConnectionManager
    public bool isBroadcastScanning {
      get {
        return bleScanMethod.isScanning;
      }
    }

    /// <summary>
    /// The ion context that the connection manager is working within.
    /// </summary>
    public BaseAndroidION ion { get; private set; }
    /// <summary>
    /// The android context that the connection manager is working within.
    /// </summary>
    public Context context { get; private set; }
    /// <summary>
    /// The bluetooth manager that is perform our bluetooth tasks.
    /// </summary>
    public BluetoothManager bm { get; private set; }

		/// <summary>
		/// The object that we will synchronize.
		/// </summary>
		internal object locker = new object();

    /// <summary>
    /// The scan method that is used for exlussively finding bluetooth le devices and used in broadcast scanning. 
    /// </summary>
    private IScanMethod bleScanMethod;

    public AndroidConnectionManager(BaseAndroidION ion) {
      this.ion = ion;
      this.context = ion.context;
      this.bm = context.GetSystemService(Context.BluetoothService) as BluetoothManager;

			// Resolve the scan methods that we will need to perform scanning.
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				if (Permission.Granted == ContextCompat.CheckSelfPermission(context, Android.Manifest.Permission.AccessFineLocation)) {
					bleScanMethod = new Api21BleScanMethod(this);
				} else {
					bleScanMethod = new Api18BleScanMethod(this);
				}
			} else {
				bleScanMethod = new Api18BleScanMethod(this);
			}
    }

    public void Release() {
      StopScan();
    }

    // Implemented for IConnectionManager
    public bool StartScan() {
      lock (locker) {
        if (!isScanning) {
          var ret = bleScanMethod.StartScan();
          isScanning = ret;
          return ret;
        } else {
          return false;
        }
      }
    }

    // Implemented for IConnectionManager
    public bool StartBroadcastScan() {
      lock (locker) {
        if (!isScanning) {
					return bleScanMethod.StartScan();
				} else {
					return false;
        }
      }
    }

    // Implemented for IConnectionManager
    public void StopScan() {
      lock (locker) {
        if (isScanning) {
          bleScanMethod.StopScan();
          isScanning = false;
        }
      }
    }

    // Implemented for IConnectionManager
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
			var device = bm.Adapter.GetRemoteDevice(address);

			if (device == null) {
				var msg = "Cannot create connection: Failed to find device for address";
				Log.E(this, msg);
				throw new Exception(msg);
			}

			switch (protocolVersion) {
				case EProtocolVersion.Classic: {
						return new ClassicConnection(device);
					} // EProtocolVersion.Classic

				case EProtocolVersion.V1: {
						return new LeConnection(this, device);
					} // EProtocolVersion.V1

				case EProtocolVersion.V4: {
						return new RigadoConnection(this, device);
					} // EProtocolVersion.V4

				default: {
						throw new Exception("Cannot create connection for protocol version: " + protocolVersion);
					}
			}
    }

		// Implemented for OnSharedPreferenceChangeListener
		public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      // todo ahodder@appioninc.com: how does this work with classic scanning in device manager and the switching between contexts?
			if (context.GetString(Resource.String.pkey_device_long_range).Equals(key)) {
				if (ion.preferences.device.allowLongRangeMode) {
					StartBroadcastScan();
				} else {
					StopScan();
				}
			}
		}

		internal void OnDeviceFound(BluetoothDevice device, byte[] scanRecord) {
			ISerialNumber sn = null;

			if (!ResolveSerialNumber(device, scanRecord, out sn)) {
				// We could not resolve the device (or it is not ours)
				return;
			}

			// Note: gauges that have a scan record come with an interesting issue. There are two radios on those devices. As
			// such, the radio that would actually provide a scan record is NOT the radio that accepts connections. So, we
			// must filter out the connections that are providing scan records and only pass empty scan record connections to
			// device manager.

			if (scanRecord == null) {
				// Notify the device manager a newly found device.
				var pv = ResolveProtocolVersion(device, sn, scanRecord);

				if (onDeviceFound != null) {
					onDeviceFound(this, sn, device.Address, scanRecord, pv);
				}
			} else {
				// We need to simply get the ACTUAL connection and pass it the scan record.
				var idevice = ion.deviceManager[sn];
				if (idevice == null) {
					// The device manager does not know about this device. This is actually an issue as we can't create the device
					// with the current bluetooth device that we were given. So we must ignore the device until we can get in
					// enough in range to discover the actual connectable bluetoothdevice.
					Log.E(this, "Received a broadcast packet for an unknown device. Ignoring device");
				} else {
					// The application is aware of the device, this is great. Just simply set the connection's last packet to
					// perform the broadcast update.
					idevice.connection.lastPacket = scanRecord;
				}
			}
		}

		/// <summary>
		/// Attempts to resolve the serial number from the device and its scan record.
		/// </summary>
		/// <returns><c>true</c>, if serial number was resolved, <c>false</c> otherwise.</returns>
		/// <param name="device">Device.</param>
		/// <param name="scanRecord">Scan record.</param>
		/// <param name="sn">Sn.</param>
		private bool ResolveSerialNumber(BluetoothDevice device, byte[] scanRecord, out ISerialNumber sn) {
			if (device.Name.IsValidSerialNumber()) {
				sn = device.Name.ParseSerialNumber();
				return true;
			} else if (scanRecord != null) {
				var ussn = System.Text.Encoding.UTF8.GetString(scanRecord, 0, 8);
				if (ussn.IsValidSerialNumber()) {
					sn = ussn.ParseSerialNumber();
					// We need to format the scan record.
					var buffer = new byte[20];
					Array.Copy(scanRecord, 8, buffer, 0, buffer.Length);
					Array.Clear(scanRecord, 0, scanRecord.Length);
					Array.Copy(buffer, scanRecord, buffer.Length);
					return true;
				} else {
					sn = null;
					return false;
				}
			} else {
				sn = null;
				return false;
			}
		}

		/// <summary>
		/// Attempts to resolve the protocol version for the device.
		/// </summary>
		/// <returns>The protocol version.</returns>
		/// <param name="device">Device.</param>
		/// <param name="sn">Sn.</param>
		/// <param name="scanRecord">Scan record.</param>
		private EProtocolVersion ResolveProtocolVersion(BluetoothDevice device, ISerialNumber sn, byte[] scanRecord) {
			if (scanRecord != null && scanRecord[0] == 4) {
				return EProtocolVersion.V4;
			} else {
				// TODO ahodder@appioninc.com: We need a better way to determine a device's protocol.
				if (sn.rawSerial.Length == 8) {
					return EProtocolVersion.V4;
				} else if (device.Type == BluetoothDeviceType.Classic) {
					return EProtocolVersion.Classic;
				} else {
					return EProtocolVersion.V1;
				}
			}
		}
  }
}
