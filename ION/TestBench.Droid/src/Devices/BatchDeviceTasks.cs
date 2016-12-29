namespace TestBench.Droid.Devices {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Appion.Commons.Util;

	using ION.Core.Devices;

	public static class BatchDeviceTasks {
		private static readonly string TAG = "BatchDeviceTasks";

		/// <summary>
		/// Attempts to connect all of the given connections.
		/// </summary>
		/// <returns>A list of connections that failed to connect..</returns>
		/// <param name="connections">Connections.</param>
		/// <param name="timeout">Maxmimum time that is allowed for a connection to be established. This is applied to each
		/// connection</param>
		public static async Task<List<IConnection>> ConnectAllAsync(this IEnumerable<IConnection> connections, TimeSpan timeout) {
			var ret = new List<IConnection>();

			foreach (var connection in connections) {
				connection.Connect();
				var start = DateTime.Now;
				while (!connection.isConnected && DateTime.Now - start < timeout) {
					await Task.Delay(250);
				}
				if (!connection.isConnected) {
					ret.Add(connection);
					Log.D(TAG, "Failed to connect to: " + connection.serialNumber);
				}
			}

			return ret;
		}

		/// <summary>
		/// Disconnects all of the given connections.
		/// </summary>
		/// <param name="connections">Connections.</param>
		public static void Disconnect(this IEnumerable<IConnection> connections) {
			foreach (var connection in connections) {
				connection.Disconnect();
			}
		}

		/// <summary>
		/// Attempts to connect al of the given devices.
		/// </summary>
		/// <returns>The all async.</returns>
		/// <param name="devices">Devices.</param>
		/// <param name="timeout">Timeout.</param>
		public static async Task<List<IDevice>> ConnectAllAsync(this IEnumerable<IDevice> devices, TimeSpan timeout) {
			var ret = new List<IDevice>();

			foreach (var device in devices) {
				var start = DateTime.Now;
				await device.connection.ConnectAsync();
				while (!device.isConnected && DateTime.Now - start < timeout) {
					await Task.Delay(250);
				}

				if (!device.isConnected) {
					ret.Add(device);
					Log.D(TAG, "Failed to connect to: " + device.serialNumber);
				}
			}

			return ret;
		}

		/// <summary>
		/// Disconnects all of the given devices.
		/// </summary>
		/// <param name="devices">Devices.</param>
		public static void Disconnect(this IEnumerable<IDevice> devices) {
			foreach (var device in devices) {
				device.connection.Disconnect();
			}
		}
	}
}
