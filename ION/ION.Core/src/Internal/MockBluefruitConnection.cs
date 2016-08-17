namespace ION.Core.Internal {

	using System;
	using System.IO;
	using System.Threading;
	using System.Threading.Tasks;

	using ION.Core.Connections;
	using ION.Core.Util;

	public class MockBluefruitConnection : MockConnection {

		private MockTestStation ts;
		private Task loopTask;
		private CancellationTokenSource token;

		public MockBluefruitConnection() : base("Bluefruit") {
		}

		public override Task<bool> ConnectAsync() {
			if (connectionState != EConnectionState.Disconnected) {
				return Task.FromResult(false);
			}
			connectionState = EConnectionState.Connecting;
			token = new CancellationTokenSource();
			ts = new MockTestStation(this, token.Token);
			loopTask = Task.Factory.StartNew(ts.DoLoop, token.Token);
			connectionState = EConnectionState.Connected;
			return Task.FromResult(true);
		}

		public override void Disconnect() {
			connectionState = EConnectionState.Disconnected;
			if (loopTask != null) {
				token.Cancel();
				token = null;
				loopTask = null;
			}
		}

		public override bool Write(byte[] packet) {
			ts.SetInputPacket(packet);
			return true;
		}

		internal void SetPacket(byte[] packet) {
			this.lastPacket = packet;
		}
	}

	internal class MockTestStation {
		private MockBluefruitConnection connection;
		private CancellationToken token;

		private Vrc vrc;
		private Stepper input;
		private Stepper exhaust;
		private byte[] inputPacket;

		public MockTestStation(MockBluefruitConnection connection, CancellationToken token) {
			this.connection = connection;
			this.token = token;
			vrc = new Vrc();
			input = new Stepper();
			exhaust = new Stepper();
		}

		public void SetInputPacket(byte[] inputPacket) {
			this.inputPacket = inputPacket;
		}

		public async Task DoLoop() {
			while (!token.IsCancellationRequested) {
				try {
					Loop();
				} catch (Exception e) {
					Log.D(this, "Failed to loop", e);
				}

				await Task.Delay(25);
			}
		}

		private void Loop() {
			if (connection.isConnected) {
				if (inputPacket != null) {
					ParseInputPacket(inputPacket);
					inputPacket = null;
				}
			}

			vrc.Step();
			input.Step();
			exhaust.Step();

			connection.SetPacket(UpdateBluetooth());
		}

		private byte[] UpdateBluetooth() {
			var buffer = new byte[20];

			using (var s = new BinaryWriter(new MemoryStream(buffer))) {
				// Input
				s.Write(input.targetAngle);
				s.Write(input.currentAngle);
				byte inputRps = (byte)input.speed;
				if (input.IsAtStart()) {
					inputRps |= 0x40;
				}
				if (input.IsAtEnd()) {
					inputRps |= 0x80;
				}
				s.Write(inputRps);
				// Exhaust
				s.Write(exhaust.targetAngle);
				s.Write(input.currentAngle);
				byte exhaustRps = (byte)exhaust.speed;
				if (exhaust.IsAtStart()) {
					exhaustRps |= 0x40;
				}
				if (exhaust.IsAtEnd()) {
					exhaustRps |= 0x80;
				}
				s.Write(exhaustRps);

				// Vrc
				s.Write(vrc.GetMeasurement());
			}

			return buffer;
		}

		private void Reset() {
			vrc.Reset();
			input.Reset();
			exhaust.Reset();
		}

		private void ParseInputPacket(byte[] packet) {
			switch (packet[0]) {
				case 1:
					Reset();
					return;
				case 2:
					byte[] buffer = new byte[20];
					Array.Copy(packet, 1, buffer, 0, 19);
				ParseCommandPacket(buffer);
					return;
			}
		}

		private void ParseCommandPacket(byte[] buffer) {
			using (var s = new BinaryReader(new MemoryStream(buffer))) {
				input.targetAngle = s.ReadSingle();
				input.speed = (ESpeed)s.ReadByte();
				exhaust.targetAngle = s.ReadSingle();
				exhaust.speed = (ESpeed)s.ReadByte();
			}
		}

		private class Vrc {
			private ushort vrc = 1000;
			private ushort e = 3;

			public void Reset() {
				vrc = 1000;
				e = 3;
			}

			public void Step() {
				ushort ret = vrc--;

				if (vrc > 1000) {
					vrc = 1000;
					e -= 3;

					if (e > 3) {
						e = 2;
					}
				}
			}

			public ushort GetMeasurement() {
				return (ushort)(vrc | ((e << 11) & 0xf800));
			}
		}

		private class Stepper {
			private const float DEGREE_AS_RADIANS = (float)(Math.PI / 180 / 1000);
			private const float HPI = (float)(Math.PI / 2);
			public float currentAngle;
			public float targetAngle;
			public ESpeed speed {
				get {
					return __speed;
				}
				set {
					__speed = value;
					switch (__speed) {
						case ESpeed.Fastest:
						speedFactor = 2;
						break;
						case ESpeed.Fast:
						speedFactor = 1;
						break;
						case ESpeed.Normal:
						speedFactor = 1 / 5.0f;
						break;
						case ESpeed.Slow:
						speedFactor = 1 / 20.0f;
						break;
						case ESpeed.Slowest:
						speedFactor = 1 / 40.0f;
						break;
						case ESpeed.Turtle:
						speedFactor = 1 / 80.0f;
						break;
					}
				}
			} ESpeed __speed;
			public float speedFactor;

			public void Reset() {
				currentAngle = targetAngle = 0;
			}

			public bool IsAtStart() {
				return targetAngle <= 0;
			}

			public bool IsAtEnd() {
				return targetAngle >= HPI;
			}

			public void Step() {
				if (targetAngle < currentAngle) {
					if (targetAngle < HPI) {
						targetAngle += DEGREE_AS_RADIANS;
					}
				} else if (targetAngle > currentAngle) {
					if (targetAngle > 0) {
						targetAngle -= DEGREE_AS_RADIANS;
					}
				}
			}
		}
	}
}

