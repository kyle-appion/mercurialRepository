namespace TestBench.Droid {

	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using DSoft.UI.Grid;

	using ION.Core.Devices;
	using ION.Core.IO;

	[Activity(Label="Perform Test")]
	public class TestActivity : BaseActivity {
		public const string EXTRA_SERIALS = "TestBench.Droid.extra.SERIALS";
		public const string EXTRA_TEST_TYPE = "TestBench.Droid.extra.TYPE";
		public const string EXTRA_RIG_ADDRESS = "TestBench.Droid.extra.RIG_ADDRESS";

		private DSGridView grid;
		private TextView state;
		private Button startTest;
		/// <summary>
		/// The test that we are working through.
		/// </summary>
		private ITest test;
		/// <summary>
		/// The data source that we will display.
		/// </summary>
		private TestDataSource dataSource;
		private Handler handler;

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			Window.AddFlags(WindowManagerFlags.KeepScreenOn);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

			SetContentView(Resource.Layout.activity_test);
			handler = new Handler();

			state = FindViewById<TextView>(Resource.Id.state);
			startTest = FindViewById<Button>(Resource.Id.startTest);
			startTest.Visibility = ViewStates.Gone;
			startTest.Click += async (sender, e) => {
				if (test.isTesting) {
					StopTest();
				} else {
					await StartTest();
				}
			};


			var rawSerials = Intent.GetStringArrayExtra(EXTRA_SERIALS);
			var type = (EDeviceModel)Intent.GetIntExtra(EXTRA_TEST_TYPE, -1);

			if (rawSerials == null) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Initialization Error");
				adb.SetMessage("Failed to start TestActivity: no serial numbers were passed to the activity");
				adb.SetNegativeButton("Cancel", (sender, e) => {
					Finish();
				});
				adb.Show();
			}

			var sns = new List<ISerialNumber>();
			foreach (var raw in rawSerials) {
				var sn = raw.ParseSerialNumber();
				if (sn.deviceModel != type) {
					var adb = new AlertDialog.Builder(this);
					adb.SetTitle("Initialization Error");
					adb.SetMessage("Failed to start TestActivity: serial number type (" + sn.deviceModel + ") is not the expected: " + type);
					adb.SetNegativeButton("Well, that sucks", (sender, e) => {
						Finish();
					});
					adb.Show();
					return;
				} else {
					sns.Add(sn);
				}
			}

			grid = FindViewById<DSGridView>(Resource.Id.grid);
			grid.Theme = DSoft.Themes.Grid.DSGridTheme.Current;

			Initialize();

			UpdateStartTestButton();
		}

		protected override void OnResume() {
			base.OnResume();
			grid.ReloadData();
		}

		protected override void OnPause() {
			base.OnPause();
			if (service != null) {
				service.scanDelegate.StopScan();
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			return true;
		}

		// Overridden from Activity
		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					if (test != null) {
						test.onTestEvent -= OnTestEvent;
						test.StopTest();
						test = null;
					}
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		private async Task<bool> StartTest() {
			var rig = test.rig;
			var connections = test.connections.Values;

			var progress = new ProgressDialog(this);
			progress.SetTitle("Please Wait...");
			progress.SetMessage("We are connecting to all of the devices. Please wait.");
			progress.Indeterminate = true;
			progress.SetCancelable(false);

			handler.Post(() => {
				progress.Show();
			});

			Action dismiss = new Action(() => {
				handler.Post(() => {
					progress.Dismiss();
				});
			});

			// First we will need to connect to the rig
			var start = DateTime.Now;

			rig.Connect();
			while (DateTime.Now - start <= TimeSpan.FromSeconds(30)) {
				if (rig.isConnected) {
					break;
				} else {
					await Task.Delay(TimeSpan.FromMilliseconds(150));
				}
			}

			if (!rig.isConnected) {
				handler.Post(() => {
					var adb = new AlertDialog.Builder(this);
					adb.SetTitle("Failed to Start Test");
					adb.SetMessage("Failed to connect to the test rig.");
					adb.SetNegativeButton("Well, then connect to the rig?!", (sender, e) => {
					});
					adb.Show();
				});

				dismiss();
				StopTest();
				return false;
			}

			var missingConnection = false;
			for (var attempts = 2; attempts >= 2; attempts--) {
				// Connect to all of the selected connections.
				foreach (var connection in connections) {
					handler.Post(() => {
						if (!connection.isConnected) {
							connection.Connect();
						}
					});
					await Task.Delay(TimeSpan.FromMilliseconds(1000));
				}

				start = DateTime.Now;
				while (DateTime.Now - start <= TimeSpan.FromSeconds(20)) {
					missingConnection = false;
					foreach (var connection in connections) {
						if (!connection.isConnected) {
							missingConnection = true;
							break;
						}
					}

					if (!missingConnection) {
						break;
					} else {
						await Task.Delay(150);
					}
				}

				// Check if we need another try
				foreach (var connection in connections) {
					if (!connection.isConnected) {
						continue;
					}
				}
			}

			dismiss();

			if (!rig.isConnected) {
				missingConnection = true;
			} else {
				foreach (var connection in connections) {
					if (!connection.isConnected) {
						missingConnection = true;
						break;
					}
				}
			}

			if (missingConnection) {
				var sb = new StringBuilder();
				sb.Append("Failed to connect to the following devices:\n");
				if (!rig.isConnected) {
					sb.Append("TestRig, ");
				}
				foreach (var connection in connections) {
					if (!connection.isConnected) {
						sb.Append(connection.serialNumber).Append(", ");
					}
				}

				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Failed to Start Test");
				adb.SetMessage(sb.ToString());
				adb.SetCancelable(false);
				adb.SetNegativeButton("Cancel", (sender, e) => {
				});
				adb.Show();

				StopTest();
				return false;
			} else {
				test.StartTest();
				return true;
			}
		}

		private void StopTest() {
			test.StopTest();
		}

		private void DisconnectFromTheWorld() {
			test.rig.Disconnect();
			foreach (var connection in test.connections.Values) {
				connection.Disconnect();
			}
		}

/*
		public override void OnServiceBound() {
			base.OnServiceBound();
			Initialize();
		}
*/

		private void Initialize() {

			var rawSerials = Intent.GetStringArrayExtra(EXTRA_SERIALS);
			var type = (EDeviceModel)Intent.GetIntExtra(EXTRA_TEST_TYPE, -1);

			if (rawSerials == null) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Initialization Error");
				adb.SetMessage("Failed to start TestActivity: no serial numbers were passed to the activity");
				adb.SetCancelable(false);
				adb.SetNegativeButton("Cancel", (sender, e) => {
					Finish();
				});
				adb.Show();
				return;
			}

			var sns = new List<ISerialNumber>();
			foreach (var raw in rawSerials) {
				var sn = raw.ParseSerialNumber();
				if (sn.deviceModel != type) {
					var adb = new AlertDialog.Builder(this);
					adb.SetTitle("Initialization Error");
					adb.SetMessage("Failed to start TestActivity: serial number type (" + sn.deviceModel + ") is not the expected: " + type);
					adb.SetCancelable(false);
					adb.SetNegativeButton("Well, that sucks", (sender, e) => {
						Finish();
					});
					adb.Show();
					return;
				} else {
					sns.Add(sn);
				}
			}

			if (sns.Count <= 0) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Initialization Error");
				adb.SetMessage("No serial numbers were provided to the TestActivity.");
				adb.SetCancelable(false);
				adb.SetNegativeButton("Ok. Take me to my leaders", (sender, e) => {
					Finish();
				});
				adb.Show();
				return;
			}

			if (BuildTest(sns)) {
				InitGrid();
				UpdateStartTestButton();
			}

			handler.PostDelayed(() => grid.ReloadData(), 1500);
		}

		private void Export() {
			var adb = new AlertDialog.Builder(this);
			adb.SetTitle("Test Complete");
			adb.SetMessage("The test has completed. Would you like to export it?");
			adb.SetNegativeButton("No", (sender, e) => {
				var adb2 = new AlertDialog.Builder(this);
				adb2.SetTitle("Really?");
				adb2.SetMessage("Are you sure you don't want to export?");
				adb2.SetCancelable(false);
				adb2.SetNegativeButton("I'm Sure", (sender2, e2) => {});
				adb2.SetPositiveButton("Export", (sender2, e2) => { StartActivity(new Intent(this, typeof(ExportTestActivity))); });
				adb2.Show();
			});
			adb.SetPositiveButton("Export", (sender, e) => {
				StartActivity(new Intent(this, typeof(ExportTestActivity)));
			});
      adb.SetCancelable(false);
			adb.Show();
		}

		private void OnTestEvent(ITest test, TestEvent te) {
			handler.Post(() => {
				switch (te.type) {
					case TestEvent.EType.TestStarted:
						break;
					case TestEvent.EType.TestStopped:
						DisconnectFromTheWorld();
						break;
					case TestEvent.EType.TestCancelled:
						handler.Post(() => {
							var adb = new AlertDialog.Builder(this);
							adb.SetTitle("Test Canceled");
							adb.SetMessage(te.message);
							adb.SetCancelable(false);
							adb.SetNegativeButton("Well, damn", (sender, e) => {});
							adb.Show();
						});
						DisconnectFromTheWorld();
						break;
					case TestEvent.EType.NewTestData:
						grid.ReloadData();
						break;
					case TestEvent.EType.NewTestState:
						break;
					case TestEvent.EType.TestComplete:
						DisconnectFromTheWorld();
						this.handler.Post(() => Export());
						break;
				}

				state.TextFormatted = Android.Text.Html.FromHtml(test.GetState());
				UpdateStartTestButton();
			});
		}

		private void UpdateStartTestButton() {
			if (test == null) {
				startTest.Visibility = ViewStates.Gone;
			} else {
				startTest.Visibility = ViewStates.Visible;
				if (test.isTesting) {
					startTest.Text = "Stop Test";
					startTest.SetBackgroundColor(Android.Graphics.Color.Red);
				} else {
					startTest.Text = "Start Test";
					startTest.SetBackgroundColor(Android.Graphics.Color.Green);
				}
			}
		}

		private bool BuildTest(List<ISerialNumber> serialNumbers) {
			var address = Intent.GetStringExtra(EXTRA_RIG_ADDRESS);
			var type = (EDeviceModel)Intent.GetIntExtra(EXTRA_TEST_TYPE, -1);

			service = AppService.INSTANCE;
			var rig = service.GetRigByAddress(address);

			if (rig == null) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Initialization Error");
				adb.SetMessage("Failed to find Rig.");
				adb.SetCancelable(false);
				adb.SetNegativeButton("Then how did I get here? Ok, take me back...", (sender, e) => {
					Finish();
				});
				adb.Show();
				return false;
			} else if (rig.rigType != type.AsRigType()) {
				var adb = new AlertDialog.Builder(this);
				adb.SetTitle("Initialization Error");
				adb.SetMessage("Received a Rig of type: " + rig.rigType + ". A rig of this type is incompatible with a test designed for " + type);
				adb.SetCancelable(false);
				adb.SetNegativeButton("This app just refuses to work. Ok, take me back...", (sender, e) => {
					Finish();
				});
				adb.Show();
				return false;
			} else {
				var connections = new List<IConnection>();

				foreach (var serialNumber in serialNumbers) {
					// TODO ahodder@appioninc.com: This is bad
					var connection = service.GetConnection(serialNumber);
					if (connection != null) {
						connections.Add(connection);
					}
				}

				var tp = new XmlTestParser().Parse(EmbeddedResource.Load(typeof(TestActivity).GetTypeInfo().Assembly, "test_av760.xml"));
				switch (rig.rigType) {
					case ERigType.Vacuum:
						test = new AV760Test(tp, rig as VacuumRig, connections);
						break;
					default:
						var adb = new AlertDialog.Builder(this);
						adb.SetTitle("Initialization Error");
						adb.SetMessage("Failed to build test");
						adb.SetCancelable(false);
						adb.SetNegativeButton("Wai?! Ok, take me back...", (sender, e) => {
							Finish();
						});
						adb.Show();
						return false;
				}

				service.currentTest = test;
				test.onTestEvent += OnTestEvent;
				return true;
			}
		}

		/// <summary>
		/// Initializes a test.
		/// </summary>
		private void InitGrid() {
			dataSource = new TestDataSource(test);
			grid.DataSource = dataSource;
		}
	}
}
